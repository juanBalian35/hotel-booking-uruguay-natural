using System.Collections.Generic;
using Domain;

namespace BusinessLogicInterface
{
    public interface ILodgingReviewLogic
    {
        LodgingReview Create(LodgingReview lodgingReview);
        
        ICollection<LodgingReview> GetAllReviews(int id, int page,int resultsPerPage);
    }
}