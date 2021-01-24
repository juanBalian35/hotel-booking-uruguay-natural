using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Import;

namespace JSONImport
{
    public class JsonImport : IImportLodging
    {
        public string GetFormatName()
        {
            return "Json";
        }

        public ICollection<LodgingParsed> Parse(string content)
        {
            var obj = Activator.CreateInstance<List<LodgingParsed>>();

            using var ms = new MemoryStream(Encoding.Unicode.GetBytes(content));
            var deserializer = new DataContractJsonSerializer(obj.GetType());

            try
            {
                return (ICollection<LodgingParsed>) deserializer.ReadObject(ms);
            }
            catch (System.Runtime.Serialization.SerializationException)
            {
                throw new ParsingNotValidTypeException(GetFormatName(), "Badly formatted");
            }
        }
    }
}