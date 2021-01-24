using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Out;

namespace ModelTests.Out
{
    [TestClass]
    public class TokenModelTest
    {
        private TokenModel TokenModel;
        private Guid Token;
    
        [TestInitialize]
        public void TestInitialize()
        {
            Token = Guid.NewGuid();
            TokenModel = new TokenModel(Token);
        }

        [TestMethod]
        public void ConstructorAddsErrors()
        {
            Assert.IsTrue(Token.Equals(TokenModel.Token));
        }
    }
}
