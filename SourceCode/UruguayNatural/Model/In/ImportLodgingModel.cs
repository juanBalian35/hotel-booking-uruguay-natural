using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Model.In
{
    public class ImportLodgingModel
    {
        public string Format { get; set; }
        public IFormFile File { get; set; }

        public string GetFileContent()
        {
            if (File == null)
            {
                return null;
            }

            var result = new StringBuilder();
            using (var reader = new StreamReader(File.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(reader.ReadLine()); 
            }

            return result.ToString().TrimEnd('\n');
        }
    }
}