using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;

namespace BusinessLogic
{
    public class RegionLogic : IRegionLogic
    {
        private IRepository<Region> RegionRepository;

        public RegionLogic(IUnitOfWork unitOfWork)
        {
            RegionRepository = unitOfWork.GetRegionRepository();
        }
        
        public ICollection<Region> GetAll()
        {
            return RegionRepository.GetAll();
        }
    }
}
