using DotNetBatch14MTZO.DB.Model;
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



app.MapPost("api/miniatm/login", async (string cardNumber, int pin) =>
{
    MiniATMEFCoreService miniATMEFCoreService = new MiniATMEFCoreService();
    var user = miniATMEFCoreService.Login(cardNumber, pin);
    return user != null ? Results.Ok(user) : Results.NotFound("Invalid card number or PIN.");
});

app.MapPost("/api/miniatm/register", async (UserAcountModel account) =>
{
    MiniATMEFCoreService miniATMEFCoreService = new MiniATMEFCoreService();
    var response = miniATMEFCoreService.RegisterAcount(account);
    return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
});

app.MapPost("/api/miniatm/withdraw", async (string cardNumber, decimal amount) =>
{
    MiniATMEFCoreService miniATMEFCoreService = new MiniATMEFCoreService();
    var response = miniATMEFCoreService.WithdrawCash(cardNumber, amount);
    return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
});

app.MapPost("/api/miniatm/deposit", async (string cardNumber, decimal amount) =>
{
    MiniATMEFCoreService miniATMEFCoreService = new MiniATMEFCoreService();
    var response = miniATMEFCoreService.Deposit(cardNumber, amount);
    return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
});

app.MapGet("/api/miniatm/balance", (string cardNumber) =>
{
    MiniATMEFCoreService miniATMEFCoreService = new MiniATMEFCoreService();
    var response = miniATMEFCoreService.GetBalance(cardNumber);

    if (response == null)
    {
        return Results.NotFound("Acount Not Found");
    }

    return Results.Ok(response);
   
});

app.Run();

