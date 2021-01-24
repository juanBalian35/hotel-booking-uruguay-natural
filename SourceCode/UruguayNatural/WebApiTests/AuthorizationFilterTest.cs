using BusinessLogicInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using WebApi.Filters;

namespace WebApiTests
{
    [TestClass]
    public class AuthorizationFilterTest
    {
        private AuthorizationFilter AuthorizationFilter;

        [TestInitialize]
        public void TestInitialize()
        {
            AuthorizationFilter = new AuthorizationFilter();
        }

        [TestMethod]
        public void AuthorizationFilterWithNullTokenHasStatusCode401()
        {
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(a => a.Request.Headers["Authorization"]).Returns((string)null);

            var authFilterContext = CreateAuthorizationFilterContext(httpContextMock.Object);
            AuthorizationFilter.OnAuthorization(authFilterContext);
            var result = authFilterContext.Result as ContentResult;

            Assert.IsTrue(result.StatusCode == 401);
        }

        [TestMethod]
        public void AuthorizationFilterWithNonGuidTokenHasStatusCode401()
        {
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(a => a.Request.Headers["Authorization"]).Returns("not a valid guid");

            var authFilterContext = CreateAuthorizationFilterContext(httpContextMock.Object);
            AuthorizationFilter.OnAuthorization(authFilterContext);
            var result = authFilterContext.Result as ContentResult;

            Assert.IsTrue(result.StatusCode == 401);
        }

        [TestMethod]
        public void AuthorizationFilterWithNotFoundTokenHasStatusCode403()
        {
            var mock = new Mock<ISessionLogic>(MockBehavior.Strict);
            mock.Setup(m => m.IsValidToken(It.IsAny<Guid>())).Returns(false);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(a => a.Request.Headers["Authorization"])
                   .Returns(Guid.NewGuid().ToString());
            httpContextMock.Setup(a => a.RequestServices.GetService(typeof(ISessionLogic)))
                    .Returns(mock.Object);

            var authFilterContext = CreateAuthorizationFilterContext(httpContextMock.Object);
            AuthorizationFilter.OnAuthorization(authFilterContext);
            var result = authFilterContext.Result as ContentResult;

            Assert.IsTrue(result.StatusCode == 403);
        }

        [TestMethod]
        public void AuthorizationFilterWithValidTokenHasStatusCode200()
        {
            var mock = new Mock<ISessionLogic>(MockBehavior.Strict);
            mock.Setup(m => m.IsValidToken(It.IsAny<Guid>())).Returns(true);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(a => a.Request.Headers["Authorization"])
                   .Returns(Guid.NewGuid().ToString());
            httpContextMock.Setup(a => a.RequestServices.GetService(typeof(ISessionLogic)))
                    .Returns(mock.Object);

            var authFilterContext = CreateAuthorizationFilterContext(httpContextMock.Object);
            AuthorizationFilter.OnAuthorization(authFilterContext);

            Assert.IsNull(authFilterContext.Result);
        }

        private AuthorizationFilterContext CreateAuthorizationFilterContext(HttpContext httpContext)
        {
            var actionContext = new ActionContext(
                httpContext,
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>()
            );

            return new AuthorizationFilterContext(actionContext, new List<IFilterMetadata> { });
        }
    }
}
