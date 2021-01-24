using System.Collections.Generic;
using Domain;

namespace DataAccessInterface
{
    public interface ITouristSpotRepository : IRepository<TouristSpot>
    {
        ICollection<TouristSpot> Search(int region, List<int> categories, int page,
            int numItemsPerPage);

        bool HasAnyBooking(int touristSpotId);
    }
}