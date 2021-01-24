using System.Collections.Generic;
using BusinessLogicInterface.Exceptions;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using Domain.Validations;
using Model.In;
using Model.Out;

namespace BusinessLogic
{
    public class ReportLogic : IReportLogic
    {
        private readonly ILodgingRepository LodgingRepository;
        private readonly ITouristSpotRepository TouristSpotRepository;
        
        public ReportLogic(IUnitOfWork unitOfWork)
        {
            LodgingRepository = unitOfWork.GetLodgingRepository();
            TouristSpotRepository = unitOfWork.GetTouristSpotRepository();
        }
        
        public ICollection<ReportBasicInfoModel> Search(ReportModel reportModel)
        {
            var touristSpot = TouristSpotRepository.GetFirst(spot => spot.Id == reportModel.TouristSpot);
            if (touristSpot == null)
            {
                throw new NotFoundException("TouristSpot");
            }

            if (!TouristSpotRepository.HasAnyBooking(touristSpot.Id))
            {
                var error = new Notification();
                error.AddError("TouristSpot", "Has no bookings");
                throw new EntityNotValidException(error);
            }

            return LodgingRepository.Search(reportModel);
        }
    }
}
