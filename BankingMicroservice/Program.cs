using System.Net;
using BankingMicroservice.Services;

ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICardService, CardService>();
builder.Services.AddTransient<IActionsService, ActionsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.Use(async (context, next) =>
{
    context.Response.ContentType = "application/json";
    await next();
});

app.MapGet("/allowedactions/{userId}/{cardId}", async (ICardService cardService, IActionsService actionsService, string userId, string cardId) =>
{
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