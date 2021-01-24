using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Domain;

namespace DomainTests
{
    [TestClass]
    public class LodgingTest
    {
        private Lodging Lodging;

        [TestInitialize]
        public void TestInitialize()
        {
            var touristSpot = new TouristSpot()
            {
                Id = 1,
                Name = "name",
                Description = "description",
                Image = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20},
                Region = null,
                TouristSpotCategories = new List<TouristSpotCategory>()
            };
            Lodging = new Lodging()
            {
                Id = 1,
                Name = "Name",
                Description = "Description",
                Rating = 3,
                IsFull = true,
                Images = new List<LodgingImage>() { new LodgingImage() },
                PricePerNight = 100,
                Address = "Valid Address 123",
                Phone = "+598 98 303 040",
                ConfirmationMessage = "Your reservation has been confirmed!",
                TouristSpot = touristSpot,
                IsDeleted = false
            };
        }

        [TestMethod]
        public void ValidLodgingIsValid()
        {
            Assert.IsFalse(Lodging.Validate().HasErrors());
        }

        [TestMethod]
        public void NameCannotBeNull()
        {
            Lodging.Name = null;
            Assert.IsTrue(Lodging.Validate().HasErrors());
        }

        [TestMethod]
        public void NameCannotBeEmpty()
        {
            Lodging.Name = "    ";
            Assert.IsTrue(Lodging.Validate().HasErrors());
        }

        [TestMethod]
        public void DescriptionCannotBeNull()
        {
            Lodging.Description = null;
            Assert.IsTrue(Lodging.Validate().HasErrors());
        }

        [TestMethod]
        public void DescriptionCannotBeEmpty()
        {
            Lodging.Description = "    ";
            Assert.IsTrue(Lodging.Validate().HasErrors());
        }

        [TestMethod]
        public void RatingCannotBeLessThan1()
        {
            Lodging.Rating = 0;
            Assert.IsTrue(Lodging.Validate().HasErrors());
        }

        [TestMethod]
        public void RatingCannotBeMoreThan5()
        {
            Lodging.Rating = 6;
            Assert.IsTrue(Lodging.Validate().HasErrors());
        }

        [TestMethod]
        public void ImagesCannotBeNull()
        {
            Lodging.Images = null;
            Assert.IsTrue(Lodging.Validate().HasErrors());
        }

        [TestMethod]
        public void ImagesShouldHaveAtleastOneElement()
        {
            Lodging.Images.Clear();
            Assert.IsTrue(Lodging.Validate().HasErrors());
        }

        [TestMethod]
        public void PricePerNightShouldBePositive()
        {
            Lodging.PricePerNight = -10;
            Assert.IsTrue(Lodging.Validate().HasErrors());
        }

        [TestMethod]
        public void AddressShouldNotBeNull()
        {
            Lodging.Address = "";
            Assert.IsTrue(Lodging.Validate().HasErrors());
        }

        [TestMethod]
        public void AddressShouldNotBeEmpty()
        {
            Lodging.Address = "";
            Assert.IsTrue(Lodging.Validate().HasErrors());
        }

        [TestMethod]
        public void PhoneShouldNotBeNull()
        {
            Lodging.Phone = null;
            Assert.IsTrue(Lodging.Validate().HasErrors());
        }

        [TestMethod]
        public void PhoneShouldNotBeEmpty()
        {
            Lodging.Phone = "     ";
            Assert.IsTrue(Lodging.Validate().HasErrors());
        }

        [TestMethod]
        public void ConfirmationMessageShouldNotBeNull()
        {
            Lodging.ConfirmationMessage = null;
            Assert.IsTrue(Lodging.Validate().HasErrors());
        }

        [TestMethod]
        public void ConfirmationMessageShouldNotBeEmpty()
        {
            Lodging.ConfirmationMessage = "";
            Assert.IsTrue(Lodging.Validate().HasErrors());
        }

        [TestMethod]
        public void TouristSpotShouldNotBeNull()
        {
            Lodging.TouristSpot = null;
            Assert.IsTrue(Lodging.Validate().HasErrors());
        }

        [TestMethod]
        public void EqualsIsTrueIfIdAndAddressAreTheSame()
        {
            var other = Lodging;
            Assert.IsTrue(other.Equals(Lodging));
        }

        [TestMethod]
        public void EqualsIsFalseIfIdOrAddressAreNotTheSame()
        {
            var other = new Lodging()
            {
                Id = Lodging.Id,
                Address = "123",
            };
            Assert.IsFalse(other.Equals(Lodging));
        }

        [TestMethod]
        public void EqualsIsFalseWithAnotherType()
        {
            Assert.IsFalse(Lodging.Equals("string"));
        }
        
        [TestMethod]
        public void CalculatePriceMultipliesCostPerNightByOneIfOnlyAdults()
        {
            var guests = new List<Guest> { new Adult { Quantity = 2 } };
            var totalPrice = Lodging.CalculatePrice(DateTime.Now, DateTime.Now.AddDays(3), guests);
            
            Assert.AreEqual(totalPrice, 3 * Lodging.PricePerNight * 2);
        }
        
        [TestMethod]
        public void CalculatePriceMultipliesCostDependingOnGuestsAdded()
        {
            var guests = new List<Guest>
            {
                new Adult { Quantity = 1}, new Baby { Quantity = 1}, new Child { Quantity = 1}
            };
            var totalPrice = Lodging.CalculatePrice(DateTime.Now, DateTime.Now.AddDays(3), guests);
            
            Assert.AreEqual(totalPrice, Lodging.PricePerNight * 3 * (0.25 + 0.5 + 1));
        }
        
        [TestMethod]
        public void CalculatePriceHasSpecialDiscountOnElders()
        {
            var guests = new List<Guest>
            {
                new Adult { Quantity = 1}, new Baby { Quantity = 1}, new Retiree { Quantity = 5 }
            };
            var totalPrice = Lodging.CalculatePrice(DateTime.Now, DateTime.Now.AddDays(3), guests);

            var adultsPrice = 1 * Lodging.PricePerNight * 3;
            var babiesPrice = 0.25 * Lodging.PricePerNight * 3;
            var eldersPrice = Lodging.PricePerNight * 3 * 5 - Lodging.PricePerNight * 3 * Math.Floor(5/2.0) * 0.3;
            Assert.AreEqual(totalPrice, adultsPrice + babiesPrice + eldersPrice);
        }

        [TestMethod]
        public void SetTotalPriceSetsTotalPrice()
        {
            const int TOTAL_PRICE = 30;
            Lodging.TotalPrice = TOTAL_PRICE;
            Assert.AreEqual(Lodging.TotalPrice, TOTAL_PRICE);
        }
    }
}
