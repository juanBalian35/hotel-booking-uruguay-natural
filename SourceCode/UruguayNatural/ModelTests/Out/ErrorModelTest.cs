using Domain.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Out;

namespace ModelTests.Out
{
    [TestClass]
    public class ErrorModelTest
    {
        private ErrorModel ErrorModel;
        private Notification Notification;
        
        [TestInitialize]
        public void TestInitialize()
        {
            Notification = new Notification();
            Notification.AddError("example", "error message");
            
            ErrorModel = new ErrorModel(Notification);
        }

        [TestMethod]
        public void ConstructorAddsErrors()
        {
            Assert.IsTrue(Notification.GetErrors().Equals(ErrorModel.Errors));
        }
    }
}
