using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BusinessLogicInterface;
using BusinessLogicInterface.Exceptions;
using DataAccessInterface;
using Domain;
using Import;
using Model.In;

namespace BusinessLogic
{
    public class ImportLogic : IImportLogic
    {
        private readonly ILodgingLogic LodgingLogic;
        private readonly ITouristSpotRepository TouristSpotRepository;
        
        public ImportLogic(ILodgingLogic lodgingLogic, IUnitOfWork unitOfWork)
        {
            LodgingLogic = lodgingLogic;
            TouristSpotRepository = unitOfWork.GetTouristSpotRepository();
        }
        
        public ICollection<string> GetFormatNames()
        {
            IEnumerable<Type> implementations = new List<Type>();
            foreach (var file in FilesOnAssembliesDirectory("*.dll"))
            {
                var assemblyLoaded = Assembly.LoadFile(file.FullName);
                var loadedImplementations = assemblyLoaded.GetTypes().Where(IsValidClassType);

                if (loadedImplementations.Any())
                {
                    implementations = implementations.Union(loadedImplementations);
                }
            }

            return implementations
                .Select(impl => (Activator.CreateInstance(impl) as IImportLodging).GetFormatName())
                .ToList();
        }

        public void Import(ImportLodgingModel importLodgingModel)
        {
            var files = FilesOnAssembliesDirectory(importLodgingModel.Format.ToUpper() + "Import.dll");
            if (!files.Any())
            {
                throw new NotFoundException(importLodgingModel.Format);
            }
            
            ImportFromFile(importLodgingModel, files.First());
        }

        private void ImportFromFile(ImportLodgingModel importLodgingModel, FileInfo file)
        {   
            var assemblyLoaded = Assembly.LoadFile(file.FullName);
            var classType = assemblyLoaded.GetTypes().First();

            if (!IsValidClassType(classType))
            {
                throw new NotFoundException(importLodgingModel.Format);
            }
            
            CreateParsed(importLodgingModel, classType);
        }

        private static bool IsValidClassType(Type classType)
        {
            return typeof(IImportLodging).IsAssignableFrom(classType) && classType.IsClass;
        }

        private void CreateParsed(ImportLodgingModel importLodgingModel, Type classType)
        {
            var importer = (IImportLodging)Activator.CreateInstance(classType);
            var fileContent = importLodgingModel.GetFileContent();

            importer.Parse(fileContent).ToList().ForEach(CreateFromParsedModel);
        }
        
        private void CreateFromParsedModel(LodgingParsed lodgingParsed)
        {
            var lodging = new Lodging
            {
                Name = lodgingParsed.Name,
                Description = lodgingParsed.Description,
                Address = lodgingParsed.Address,
                Rating = lodgingParsed.Rating,
                PricePerNight = lodgingParsed.PricePerNight,
                ConfirmationMessage = lodgingParsed.ConfirmationMessage,
                Phone = lodgingParsed.Phone,
                TouristSpot = TouristSpotFromParsedModel(lodgingParsed)
            };
                    
            if (!lodging.IsValid("Images"))
            {
                throw new EntityNotValidException(lodging.Validate("Images"));
            }
            
            LodgingLogic.Create(lodging);
        }

        private TouristSpot TouristSpotFromParsedModel(LodgingParsed lodgingParsed)
        {
            var touristSpot = TouristSpotRepository.GetFirst(x => x.Name == lodgingParsed.TouristSpot.Name);
            
            if (touristSpot == null)
            {
                touristSpot = new TouristSpot
                {
                    Name = lodgingParsed.TouristSpot.Name,
                    Description = lodgingParsed.Description,
                    RegionId = lodgingParsed.TouristSpot.RegionId
                };

                if (!touristSpot.IsValid("Image"))
                {
                    throw new EntityNotValidException(touristSpot.Validate("Image"));
                }
                
                TouristSpotRepository.Add(touristSpot);
                TouristSpotRepository.Save();
            }

            return touristSpot;
        }

        private static FileInfo[] FilesOnAssembliesDirectory(string pattern)
        {
            var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "ImportersAssemblies");
            return directory.GetFiles(pattern);
        }
    }
}
