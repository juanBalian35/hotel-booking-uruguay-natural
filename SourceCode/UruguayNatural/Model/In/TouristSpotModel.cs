using Domain;
using Domain.Validations;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Model.In
{
    public class TouristSpotModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile Image { get; set; }

        public int? RegionId { get; set; }

        public List<int> Categories { get; set; }

        public TouristSpot ToEntity()
        {
            var touristSpotCategories = new List<TouristSpotCategory>();
            
            if (Categories != null)
            {
                Categories = Categories.Distinct().ToList();
                Categories.Sort();
                Categories.ForEach(x => touristSpotCategories.Add(new TouristSpotCategory() { CategoryId = x }));
            }
            
            return new TouristSpot
            {
                Name = this.Name,
                Description = this.Description,
                Image = GetImage(),
                RegionId = RegionId,
                TouristSpotCategories = touristSpotCategories
            };
        }

        private byte[] GetImage()
        {
            if (Image == null)
            {
                return null;
            }
            
            var memoryStream = new MemoryStream();
            this.Image.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        public bool HasErrors()
        {
            return !ToEntity().IsValid();
        }
        
        public INotification Errors()
        {
            return ToEntity().Validate();
        }
    }
}
