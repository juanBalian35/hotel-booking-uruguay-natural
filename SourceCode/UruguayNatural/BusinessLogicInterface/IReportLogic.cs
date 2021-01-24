using System.Collections.Generic;
using Domain;
using Model.In;
using Model.Out;

namespace BusinessLogicInterface
{
    public interface IReportLogic
    {
        ICollection<ReportBasicInfoModel> Search(ReportModel reportModel);
    }
}