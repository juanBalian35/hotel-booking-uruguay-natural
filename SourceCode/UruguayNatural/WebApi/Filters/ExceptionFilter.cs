using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BusinessLogicInterface.Exceptions;
using Import;
using Newtonsoft.Json;

namespace WebApi.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            try
            {
                throw context.Exception;
            }
            catch (NotUniqueException exception)
            {
                context.Result = ResponseForSingleError(400, exception.Field, "Has to be unique");
            }
            catch (NotFoundException exception)
            {
                context.Result = ResponseForSingleError(404, exception.Field, "Was not found");
            }
            catch (InvalidCredentialsException)
            {
                context.Result = ResponseForSingleError(401, "credentials", "Are not valid");
            }
            catch (EntityNotValidException exception)
            {
                context.Result = ResponseForDictionary(400, 
                        AddOutterKeyToDictionary("errors", exception.Notification.GetErrors()));
            }
            catch (ParsingNotValidTypeException exception)
            {
                context.Result = ResponseForSingleError(400, "parser", exception.ParserName + " " + exception.Message);
            }
            catch (Exception exception)
            {
                context.Result = ResponseForException(exception); 
            }
        }

        private ContentResult ResponseForException(Exception exception)
        {
            var response = new Dictionary<string, string>();
            response["errors"] = exception.Message;

            return ResponseForDictionary(500, response);
        }

        private ContentResult ResponseForSingleError(int httpStatus, string field, string error)
        {
            var dictionary = new Dictionary<string, List<string>> { { field, new List<string> { error } } };
            return ResponseForDictionary(httpStatus, AddOutterKeyToDictionary("errors", dictionary));
        }

        private ContentResult ResponseForDictionary<T, V>(int httpStatus, IDictionary<T, V> dictionary)
        {
            var content = DictionaryToJson(dictionary);
            return new ContentResult { StatusCode = httpStatus, Content = content };
        }

        private static IDictionary<string, IDictionary<T, V>> AddOutterKeyToDictionary<T, V>(string key, 
            IDictionary<T, V> dictionary)
        {
            var response = new Dictionary<string, IDictionary<T, V>> { [key] = dictionary };

            return response; 
        }

        private static string DictionaryToJson<T, V>(IDictionary<T, V> dictionary)
        {
            return JsonConvert.SerializeObject(dictionary, Formatting.Indented);
        }
    }
}
