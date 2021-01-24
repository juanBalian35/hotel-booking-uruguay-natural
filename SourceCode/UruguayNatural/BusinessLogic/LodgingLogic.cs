using System.Collections.Generic;
using BusinessLogicInterface.Exceptions;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using Model.In;

namespace BusinessLogic
{
    public class LodgingLogic : ILodgingLogic
    {
        private readonly ILodgingRepository LodgingRepository;
        private readonly ITouristSpotRepository TouristSpotRepository;
        public LodgingLogic(IUnitOfWork unitOfWork)
        {
            LodgingRepository = unitOfWork.GetLodgingRepository();
            TouristSpotRepository = unitOfWork.GetTouristSpotRepository();
        }

        public Lodging Create(Lodging lodging)
        {
            var touristSpot = TouristSpotRepository.Get(lodging.TouristSpot.Id);

            if (touristSpot == null)
            {
                throw new NotFoundException("TouristSpot");
            }

            if (LodgingRepository.Exists(x => x.Address == lodging.Address && x.IsDeleted == false))
            {
                throw new NotUniqueException("Address");
            }

            lodging.TouristSpot = touristSpot;
            LodgingRepository.Add(lodging);
            LodgingRepository.Save();
            
            return LodgingRepository.GetFirst(x => x.Id == lodging.Id, "TouristSpot,Images");
        }

        public Lodging Update(int id)
        {
            var actualLodging = LodgingRepository.GetFirst(x => x.Id == id && x.IsDeleted == false);

            if (actualLodging == null)
            {
                throw new NotFoundException("Id");
            }
            
            actualLodging.IsFull = !actualLodging.IsFull;
            
            LodgingRepository.Update(actualLodging);
            LodgingRepository.Save();

            return LodgingRepository.GetFirst(x => x.Id == actualLodging.Id, "TouristSpot,Images");
        }
        
        public ICollection<Lodging> Search(SearchLodgingModel searchLodgingModel)
        {
            if (TouristSpotRepository.Get(searchLodgingModel.TouristSpot) == null)
            {
                throw new NotFoundException("Id");
            }

            var lodgings = LodgingRepository.GetAllWithPagination(
                x => x.TouristSpot.Id == searchLodgingModel.TouristSpot && !x.IsDeleted 
                     && (searchLodgingModel.IsFull == null ||
                         (searchLodgingModel.IsFull != null && x.IsFull == searchLodgingModel.IsFull)),
                "Images", searchLodgingModel.Page, searchLodgingModel.ResultsPerPage);

            foreach (var lodging in lodgings)
            {
                lodging.TotalPrice = lodging.CalculatePrice(searchLodgingModel.CheckIn.Value,
                    searchLodgingModel.CheckOut.Value,
                    searchLodgingModel.GetGuests());
            }
            return lodgings;
        }
        
        public void Delete(int id)
        {
            var actualLodging = LodgingRepository.GetFirst(x => x.Id == id && !x.IsDeleted);
            if (actualLodging == null)
            {
                throw new NotFoundException("Id");
            }
            
            actualLodging.IsDeleted = !actualLodging.IsDeleted;
            
            LodgingRepository.Update(actualLodging);
            LodgingRepository.Save();
        }
    }
}
