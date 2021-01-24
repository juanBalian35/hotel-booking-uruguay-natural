using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using DataAccessInterface;
using Model.In;
using Model.Out;

namespace DataAccess
{
    public class LodgingRepository : GenericRepository<Lodging>, ILodgingRepository
    {
        public LodgingRepository(DbContext context) : base(context)
        {
            Context = context;
        }

        public ICollection<ReportBasicInfoModel> Search(ReportModel reportModel)
        {
            var invalidStates = new List<string> { "Rechazada", "Expirada" };

            return Context.Set<Lodging>()
                .Include(l => l.Bookings)
                .ThenInclude(b => b.States)
                .Where(l => !l.IsDeleted && l.TouristSpot.Id == reportModel.TouristSpot)
                .Select(lodging => new
                {
                    Lodging = lodging,
                    BookingCount = lodging
                        .Bookings
                        .Count(b => b.CheckIn < reportModel.CheckOut && reportModel.CheckIn < b.CheckOut &&
                                    !invalidStates.Contains(b.States.OrderByDescending(s => s.Id).First().State))
                })
                .Where(r => r.BookingCount != 0)
                .OrderByDescending(r => r.BookingCount)
                .ThenBy(r => r.Lodging.Id)
                .Skip((reportModel.Page - 1) * reportModel.ResultsPerPage)
                .Take(reportModel.ResultsPerPage)
                .Select(r => new ReportBasicInfoModel(r.Lodging) { BookingCount = r.BookingCount })
                .ToList();
        }
    }
}
