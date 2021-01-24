using System.Collections.Generic;
using System.IO;
using Domain;
using Domain.Validations;
using Microsoft.AspNetCore.Http;

namespace Model.In
{
    public class LodgingModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public bool IsFull { get; set; }
        public ICollection<IFormFile> Images { get; set; }
        public double PricePerNight { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ConfirmationMessage { get; set; }
        public int? TouristSpot { get; set; }

        public Lodging ToEntity()
        {
            var touristSpot = TouristSpot == null ? null : new TouristSpot { Id = (int)TouristSpot };
            
            return new Lodging
            {
                Name = this.Name,
                Description = this.Description,
                Rating = this.Rating,
                IsFull = this.IsFull,
                Images = CreateImageList(),
                PricePerNight = this.PricePerNight,
                Address = this.Address,
                Phone = this.Phone,
                ConfirmationMessage = this.ConfirmationMessage,
                TouristSpot = touristSpot
            };
        }

        private List<LodgingImage> CreateImageList()
        {
            if (Images == null)
            {
                return null;
            }

            var images = new List<LodgingImage>();
            foreach (var image in Images)
            {
                var memoryStream = new MemoryStream();
                image.CopyTo(memoryStream);
                images.Add(new LodgingImage { ImageData = memoryStream.ToArray() });
            }

            return images;
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
