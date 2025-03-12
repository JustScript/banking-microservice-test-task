using System.Net;
using BankingMicroservice.Services;
using Microsoft.AspNetCore.Diagnostics;

ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddEventSourceLogger();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICardService, CardService>();
builder.Services.AddTransient<IActionsService, ActionsService>();

var app = builder.Build();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.Use(async (context, next) =>
{
    context.Response.ContentType = "application/json";
    await next();
});

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            var error = new { message = "Internal Server Error" };
            await context.Response.WriteAsJsonAsync(error);
        }
    });
});

app.MapGet("/allowedactions/{userId}/{cardId}", async (ICardService cardService, IActionsService actionsService, string userId, string cardId) =>
{
    if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(cardId))
    {
        return Results.BadRequest("UserId and CardId cannot be empty");
    }
    
    var card = await cardService.GetCardDetails(userId, cardId);
    if (card == null)
    {
        return Results.NotFound("Card not found");
    }

    var allowedActions = actionsService.GetAllowedActions(card);

    return Results.Ok(new { allowedActions });
})
.WithName("GetAllowedActions")
.WithOpenApi();

app.Run();