using System.Collections.Generic;
using Domain;
using Model.In;

namespace BusinessLogicInterface
{
    public interface ITouristSpotLogic
    {
        TouristSpot Create(TouristSpot touristSpot);

        IEnumerable<TouristSpot> Search(TouristSpotSearchModel touristSpotSearchModel);
       
        TouristSpot Get(int id);
    }
}
