using System.Collections.Generic;
using Model.In;

namespace BusinessLogicInterface
{
    public interface IImportLogic
    {
        void Import(ImportLodgingModel importLodgingModel);
        ICollection<string> GetFormatNames();
    }
}