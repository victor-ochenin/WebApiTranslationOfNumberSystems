using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace WebApiTranslationOfNumberSystems.Controllers
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            var errorType = ex.GetType().Name;
            var errorMessage = ex.Message;
            int statusCode = 400;

            switch (errorType)
            {
                case "BadHttpRequestException":
                    statusCode = 400;
                    errorType = "BadRequest";
                    errorMessage = "Некорректный запрос (400)";
                    break;
                case "KeyNotFoundException":
                    statusCode = 404;
                    errorType = "NotFound";
                    errorMessage = "Ресурс не найден (404)";
                    break;
                case "MethodNotAllowedException":
                    statusCode = 405;
                    errorType = "MethodNotAllowed";
                    errorMessage = "Метод не поддерживается (405)";
                    break;
                default:
                    statusCode = 400;
                    break;
            }

            context.Result = new ObjectResult(new ErrorMessage(errorType, errorMessage))
            {
                StatusCode = statusCode
            };
            context.ExceptionHandled = true;
        }
    }
} 