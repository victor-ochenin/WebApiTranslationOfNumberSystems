using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<WebApiTranslationOfNumberSystems.Controllers.GlobalExceptionFilter>();
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errorType = "BadRequest";
        var statusCode = 400;
        var errorMessage = "Некорректный запрос (400)";
        return new ObjectResult(new WebApiTranslationOfNumberSystems.Controllers.ErrorMessage(errorType, errorMessage, statusCode))
        {
            StatusCode = statusCode
        };
    };
});

var app = builder.Build();

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        var errorType = ex.GetType().Name;
        var statusCode = 400;
        var errorMessage = ex.Message;
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
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new WebApiTranslationOfNumberSystems.Controllers.ErrorMessage(errorType, errorMessage, statusCode));
    }
});

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.MapControllers();

app.Run();

