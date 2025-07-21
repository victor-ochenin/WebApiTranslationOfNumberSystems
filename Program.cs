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

        if (context.ModelState.Keys.Any(k => k.Contains("NotFound") || k.Contains("404")))
        {
            statusCode = 404;
            errorType = "NotFound";
            errorMessage = "Ресурс не найден (404)";
        }
        else if (context.ModelState.Keys.Any(k => k.Contains("MethodNotAllowed") || k.Contains("405")))
        {
            statusCode = 405;
            errorType = "MethodNotAllowed";
            errorMessage = "Метод не поддерживается (405)";
        }

        return new ObjectResult(new WebApiTranslationOfNumberSystems.Controllers.ErrorMessage(errorType, errorMessage, statusCode))
        {
            StatusCode = statusCode
        };
    };
});

var app = builder.Build();

app.UseRouting();

app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;
    if (response.StatusCode == 404)
    {
        var error = new WebApiTranslationOfNumberSystems.Controllers.ErrorMessage(
            "NotFound", "Ресурс не найден (404)", 404);
        response.ContentType = "application/json";
        await response.WriteAsJsonAsync(error);
    }
    else if (response.StatusCode == 405)
    {
        var error = new WebApiTranslationOfNumberSystems.Controllers.ErrorMessage(
            "MethodNotAllowed", "Метод не поддерживается (405)", 405);
        response.ContentType = "application/json";
        await response.WriteAsJsonAsync(error);
    }
    else if (response.StatusCode == 400)
    {
        var error = new WebApiTranslationOfNumberSystems.Controllers.ErrorMessage(
            "BadRequest", "Некорректный запрос (400)", 400);
        response.ContentType = "application/json";
        await response.WriteAsJsonAsync(error);
    }
});

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.MapControllers();

app.Run();

