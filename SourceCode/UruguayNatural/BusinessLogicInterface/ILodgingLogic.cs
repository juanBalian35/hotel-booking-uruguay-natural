using System.Collections.Generic;
using Domain;
using Model.In;

namespace BusinessLogicInterface
{
    public interface ILodgingLogic
    {
        Lodging Create(Lodging lodging);

        Lodging Update(int id);
        
        ICollection<Lodging> Search(SearchLodgingModel searchLodgingModel);

        void Delete(int id);
    }
}
