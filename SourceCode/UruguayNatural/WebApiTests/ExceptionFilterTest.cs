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
using BusinessLogicInterface.Exceptions;
using Domain.Validations;
using Import;

namespace WebApiTests
{
    [TestClass]
    public class ExceptionFilterTest
    {
        [TestMethod]
        public void ExceptionContextWithNormalExceptionHasStatusCode500()
        {
            var exceptionFilter = new ExceptionFilter();

            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>()
            );

            var mockException = new Mock<Exception>();
            mockException.Setup(e => e.StackTrace).Returns("Test stacktrace");
            mockException.Setup(e => e.Message).Returns("Test message");
            mockException.Setup(e => e.Source).Returns("Test source");

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = mockException.Object
            };
            exceptionFilter.OnException(exceptionContext);

            var result = exceptionContext.Result as ContentResult;

            Assert.AreEqual(result.StatusCode, 500);
        }

        [TestMethod]
        public void ExceptionContextWithNotFoundExceptionHas404StatusCode()
        {
            var exceptionFilter = new ExceptionFilter();

            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>()
            );

            var mockException = new Mock<NotFoundException>("fieldName");
            mockException.Setup(e => e.StackTrace).Returns("Test stacktrace");
            mockException.Setup(e => e.Message).Returns("Test message");
            mockException.Setup(e => e.Source).Returns("Test source");

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = mockException.Object
            };
            exceptionFilter.OnException(exceptionContext);

            var result = exceptionContext.Result as ContentResult;

            Assert.AreEqual(result.StatusCode, 404);
        }

        [TestMethod]
        public void ExceptionContextWithNotUniqueExceptionHas400StatusCode()
        {
            var exceptionFilter = new ExceptionFilter();

            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>()
            );

            var mockException = new Mock<NotUniqueException>("fieldName");
            mockException.Setup(e => e.StackTrace).Returns("Test stacktrace");
            mockException.Setup(e => e.Message).Returns("Test message");
            mockException.Setup(e => e.Source).Returns("Test source");

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = mockException.Object
            };
            exceptionFilter.OnException(exceptionContext);

            var result = exceptionContext.Result as ContentResult;

            Assert.AreEqual(result.StatusCode, 400);
        }

        [TestMethod]
        public void ExceptionContextWithEntityNotValidExceptionHas400StatusCode()
        {
            var exceptionFilter = new ExceptionFilter();

            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>()
            );

            var mockException = new Mock<EntityNotValidException>(new Notification());
            mockException.Setup(e => e.StackTrace).Returns("Test stacktrace");
            mockException.Setup(e => e.Message).Returns("Test message");
            mockException.Setup(e => e.Source).Returns("Test source");

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = mockException.Object
            };
            exceptionFilter.OnException(exceptionContext);

            var result = exceptionContext.Result as ContentResult;

            Assert.AreEqual(result.StatusCode, 400);
        }
        
        [TestMethod]
        public void ExceptionContextWithInvalidCredentialsExceptionHas401StatusCode()
        {
            var exceptionFilter = new ExceptionFilter();

            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>()
            );

            var mockException = new Mock<InvalidCredentialsException>();
            mockException.Setup(e => e.StackTrace).Returns("Test stacktrace");
            mockException.Setup(e => e.Message).Returns("Test message");
            mockException.Setup(e => e.Source).Returns("Test source");

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = mockException.Object
            };
            exceptionFilter.OnException(exceptionContext);

            var result = exceptionContext.Result as ContentResult;

            Assert.AreEqual(result.StatusCode, 401);
        }
        
        [TestMethod]
        public void ExceptionContextWithParsingNotValidTypeExceptionHas400StatusCode()
        {
            var exceptionFilter = new ExceptionFilter();

            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>()
            );

            var mockException = new Mock<ParsingNotValidTypeException>("parser name", "error");
            mockException.Setup(e => e.StackTrace).Returns("Test stacktrace");
            mockException.Setup(e => e.Message).Returns("Test message");
            mockException.Setup(e => e.Source).Returns("Test source");

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = mockException.Object
            };
            exceptionFilter.OnException(exceptionContext);

            var result = exceptionContext.Result as ContentResult;

            Assert.AreEqual(result.StatusCode, 400);
        }
    }
}
