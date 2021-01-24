using System;
using System.Collections.Generic;
using Domain;
using BusinessLogicInterface;
using DataAccessInterface;
using BusinessLogicInterface.Exceptions;
using System.Linq;
using Model.In;

namespace BusinessLogic
{
    public class TouristSpotLogic : ITouristSpotLogic
    {
        private readonly ITouristSpotRepository TouristSpotRepository;
        private readonly IRepository<Region> RegionRepository;
        private readonly IRepository<Category> CategoryRepository;

        public TouristSpotLogic(IUnitOfWork unitOfWork)
        {
            TouristSpotRepository = unitOfWork.GetTouristSpotRepository();
            RegionRepository = unitOfWork.GetRegionRepository();
            CategoryRepository = unitOfWork.GetCategoryRepository();
        }

        public IEnumerable<TouristSpot> Search(TouristSpotSearchModel touristSpotSearchModel)
        {
            var storedRegion = RegionRepository.Get(touristSpotSearchModel.Region);

            if (storedRegion == null)
            {
                throw new NotFoundException("Region");
            }

            return TouristSpotRepository.Search((int)touristSpotSearchModel.Region,
                touristSpotSearchModel.Categories.ToList(), 
                touristSpotSearchModel.Page, touristSpotSearchModel.ResultsPerPage);
        }

        
        public TouristSpot Create(TouristSpot touristSpot)
        {
            var region = RegionRepository.Get(touristSpot.RegionId);

            if (region == null)
            {
                throw new NotFoundException("Region");
            }

            if (touristSpot.TouristSpotCategories
                .Any(touristSpotCategory => CategoryRepository.Get(touristSpotCategory.CategoryId) == null))
            {
                throw new NotFoundException("Category");
            }

            TouristSpotRepository.Add(touristSpot);
            TouristSpotRepository.Save();

            return TouristSpotRepository.GetFirst(x => x.Id == touristSpot.Id, "TouristSpotCategories.Category");
        }
        
        public TouristSpot Get(int id)
        {
           var touristSpot = TouristSpotRepository.GetFirst(x=> x.Id == id, "TouristSpotCategories.Category");
           if (touristSpot == null)
           {
               throw new NotFoundException("TouristSpot.Id");
           }

           return touristSpot;
        }
    }
}
