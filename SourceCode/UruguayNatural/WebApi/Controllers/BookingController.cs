using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using Model.In;
using BusinessLogicInterface;
using Model.Out;

namespace WebApi.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingLogic BookingLogic;
        private readonly IBookingStateLogic BookingStateLogic;
        private readonly ILodgingReviewLogic LodgingReviewLogic;

        public BookingController(IBookingLogic bookingLogic, IBookingStateLogic bookingStateLogic,
            ILodgingReviewLogic lodgingReviewLogic) 
        {
            BookingLogic = bookingLogic;
            BookingStateLogic = bookingStateLogic;
            LodgingReviewLogic = lodgingReviewLogic;
        }

        [HttpPost]
        public IActionResult Post(BookingModel bookingModel)
        {
            IActionResult result;

            if (bookingModel.HasErrors())
            {
                result = BadRequest(new ErrorModel(bookingModel.Errors()));
            }
            else
            {
                var booking = BookingLogic.Create(bookingModel.ToEntity(), bookingModel.GetGuests());
                result = Created("GetBooking", new BookingBasicInfoModel(booking));
            }

            return result;
        }
        
        [HttpGet("{id}/States")]
        public IActionResult GetStates(int id)
        {
            var states = BookingLogic.GetAllStates(id);
            return Ok(states.Select(x => new BookingStateBasicInfoModel(x)).ToList());
        }

        [HttpPost("{id}/States")]
        [AuthorizationFilter]
        public IActionResult PostState(int id, BookingStateCreateModel bookingStateCreateModel)
        {
            bookingStateCreateModel.Id = id;
            
            IActionResult result;
            if (bookingStateCreateModel.HasErrors())
            {
                result = BadRequest(new ErrorModel(bookingStateCreateModel.Errors()));
            }
            else
            {
                var bookingState = BookingStateLogic.Create(bookingStateCreateModel.ToEntity());
                result = Created("GetBookingState", new BookingStateBasicInfoModel(bookingState));
            }

            return result;
        }
        
        [HttpPost("{id}/Reviews")]
        public IActionResult PostReview(int id, LodgingReviewModel lodgingReviewModel)
        {
            lodgingReviewModel.BookingId = id;
            
            IActionResult result;
            if (lodgingReviewModel.HasErrors())
            {
                result = BadRequest(new ErrorModel(lodgingReviewModel.Errors()));
            }
            else
            {
                var lodgingReview = LodgingReviewLogic.Create(lodgingReviewModel.ToEntity());
                result = Created("GetLodgingReview", new LodgingReviewBasicInfoModel(lodgingReview));
            }
            
            return result;
        }
    }
}
