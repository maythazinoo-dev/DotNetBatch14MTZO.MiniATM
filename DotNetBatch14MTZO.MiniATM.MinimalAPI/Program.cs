using DotNetBatch14MTZO.DB;
using DotNetBatch14MTZO.MiniATM.Domain.MiniATMServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

app.MapPost("api/miniatm/login", async (string cardNumber, int pin) =>
{
    MiniATMEFCoreService miniATMEFCoreService = new MiniATMEFCoreService();
    var user = miniATMEFCoreService.Login(cardNumber, pin);
    return user != null ? Results.Ok(user) : Results.NotFound("Invalid card number or PIN.");
});

app.MapPost("/api/ATM/create", async (UserAcountModel account) =>
{
    MiniATMEFCoreService miniATMEFCoreService = new MiniATMEFCoreService();
    var response = miniATMEFCoreService.CreateAcount(account);
    return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
});

app.MapPost("/api/ATM/withdraw", async (string cardNumber, decimal amount) =>
{
    MiniATMEFCoreService miniATMEFCoreService = new MiniATMEFCoreService();
    var response = miniATMEFCoreService.WithdrawCash(cardNumber, amount);
    return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
});

app.MapPost("/api/ATM/deposit", async (string cardNumber, decimal amount, MiniATMEFCoreService service) =>
{
    MiniATMEFCoreService miniATMEFCoreService = new MiniATMEFCoreService();
    var response = miniATMEFCoreService.Deposit(cardNumber, amount);
    return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
});

app.MapGet("/api/ATM/balance", (string cardNumber) =>
{
    MiniATMEFCoreService miniATMEFCoreService = new MiniATMEFCoreService();
    var user = miniATMEFCoreService.GetBalance(cardNumber);

    if (user == null)
    {
        return Results.NotFound("Acount Not Found");
    }

    return Results.Ok(user);
   
});

app.Run();

//internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}
