using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.In;

namespace ModelTests.In
{
    [TestClass]
    public class ImportLodgingModelTest
    {
        private ImportLodgingModel ImportLodgingModel;
        private const string FILE_CONTENT = "fileContent";

        [TestInitialize]
        public void TestInitialize()
        {
            var contentBytes = Encoding.UTF8.GetBytes(FILE_CONTENT);
            ImportLodgingModel = new ImportLodgingModel
            {
                Format = "Name",
                File = new FormFile(
                    new MemoryStream(contentBytes),
                    0,
                    contentBytes.Length,
                    "placeholder", 
                    "placeholder"
                )
            };
        }
        
        [TestMethod]
        public void GetContentReturnsFileContent()
        {
            Assert.AreEqual(ImportLodgingModel.GetFileContent().Replace("\r", string.Empty), FILE_CONTENT);
        }
    }
}