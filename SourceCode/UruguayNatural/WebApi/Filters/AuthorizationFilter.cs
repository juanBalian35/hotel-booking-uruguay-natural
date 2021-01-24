using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BusinessLogicInterface;
using Newtonsoft.Json;

namespace WebApi.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(token) || !IsValidGuid(token))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = CreateTokenErrorJson("Must be provided")
                };
            }
            else if (!GetSessionLogic(context).IsValidToken(Guid.Parse(token)))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = CreateTokenErrorJson("Is not valid")
                };
            }
        }

        private static bool IsValidGuid(string token)
        {
            try
            {
                Guid.Parse(token);
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        }

        private static ISessionLogic GetSessionLogic(AuthorizationFilterContext context)
        {
            var sessionType = typeof(ISessionLogic);

            return context.HttpContext.RequestServices.GetService(sessionType) as ISessionLogic;
        }

        private static string CreateTokenErrorJson(string error)
        {
            var response = new Dictionary<string, Dictionary<string, List<string>>>();
            response["errors"] = new Dictionary<string, List<string>>();
            response["errors"]["token"] = new List<string> { error };

            return JsonConvert.SerializeObject(response, Formatting.Indented);
        }
    }
}
