using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using DataAccessInterface;

namespace DataAccess
{
    public class TouristSpotRepository : GenericRepository<TouristSpot>, ITouristSpotRepository
    {
        public TouristSpotRepository(DbContext context) : base(context)
        {
            Context = context;
        }
        
        public ICollection<TouristSpot> Search(int region, List<int> categories, int page = 1, int numItemsPerPage = 10)
        {
            IQueryable<TouristSpot> query = Context.Set<TouristSpot>();

            query = query.Where(x => x.Region.Id == region);

            if(categories != null)
            {
                query = query.Where(ts => 
                    ts.TouristSpotCategories.Count(tsc => categories.Contains(tsc.CategoryId)) == categories.Count);
            }

            query = query.Skip((page - 1) * numItemsPerPage).Take(numItemsPerPage);
            query = query.Include("TouristSpotCategories.Category");
            query = query.Include("Region");
            return query.ToList();
        }

        public bool HasAnyBooking(int touristSpotId)
        {
            return Context.Set<TouristSpot>()
                .Include(ts => ts.Lodgings)
                .ThenInclude(l => l.Bookings)
                .Any(ts => ts.Id == touristSpotId && ts.Lodgings.Any(l => !l.IsDeleted && l.Bookings.Any()));
        }
    }
}
