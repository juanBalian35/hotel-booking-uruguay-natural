using System.Collections.Generic;
using Domain;
using Model.In;
using Model.Out;

namespace DataAccessInterface
{
    public interface ILodgingRepository : IRepository<Lodging>
    {
        ICollection<ReportBasicInfoModel> Search(ReportModel reportModel);
    }
}