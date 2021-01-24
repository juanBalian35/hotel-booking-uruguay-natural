using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Import;

namespace XMLImport
{
    public class XmlImport : IImportLodging
    {
        private readonly Func<string, string> IdentityFunction = str => str;
        
        public string GetFormatName()
        {
            return "Xml";
        }

        public ICollection<LodgingParsed> Parse(string content)
        {
            XDocument document;
            try
            {
                document = XDocument.Parse(content);
            }
            catch (System.Xml.XmlException)
            {
                throw new ParsingNotValidTypeException(GetFormatName(), "Badly formatted");
            }

            var lodgingsParsed =  new List<LodgingParsed>();
            if (document.Root != null)
            {
                lodgingsParsed = document.Root.Descendants("element").Select(xElement => new LodgingParsed
                {
                    Name = ReadOrDefault(xElement.Element("Name"), "", IdentityFunction),
                    Description = ReadOrDefault(xElement.Element("Description"), "", IdentityFunction),
                    Rating = ReadOrDefault(xElement.Element("Rating"), 0, int.Parse),
                    PricePerNight = ReadOrDefault(xElement.Element("PricePerNight"), 0.0, double.Parse),
                    Address =  ReadOrDefault(xElement.Element("Address"), "", IdentityFunction),
                    Phone = ReadOrDefault(xElement.Element("Phone"), "", IdentityFunction),
                    ConfirmationMessage = ReadOrDefault(xElement.Element("ConfirmationMessage"), "", IdentityFunction),
                    TouristSpot = ReadTouristSpotParsed(xElement)
                }).ToList();
            }
            
            return lodgingsParsed;
        }

        private TouristSpotParsed ReadTouristSpotParsed(XElement xElement)
        {
            if (xElement.Element("TouristSpot") == null)
            {
                return null;
            }

            var xmlTouristSpot = xElement.Element("TouristSpot");

            return new TouristSpotParsed
            {
                Name = ReadOrDefault(xmlTouristSpot.Element("Name"), "", IdentityFunction),
                Description = ReadOrDefault(xmlTouristSpot.Element("Description"), "", IdentityFunction),
                RegionId = ReadOrDefault(xmlTouristSpot.Element("RegionId"), 0, int.Parse)
            };
        }

        private static T ReadOrDefault<T>(XElement xElement, T placeholder, Func<string, T> transform)
        {
            var returnValue = placeholder;
            if (xElement != null)
            {
                returnValue = transform.Invoke(xElement.Value);
            }

            return returnValue;
        }
    }
}
