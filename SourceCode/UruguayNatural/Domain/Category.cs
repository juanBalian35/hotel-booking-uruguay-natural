using System.Collections.Generic;

namespace Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FaIconName { get; set; }
        
        public ICollection<TouristSpotCategory> TouristSpotCateogries { get; set; }
    }
}
