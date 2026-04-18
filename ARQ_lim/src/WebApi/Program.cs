using Infrastructure.Data;
using Infrastructure.Logging;
using Application.Interfaces;
using Application.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();

builder.Services.AddCors(o => o.AddPolicy("bad", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddScoped<Application.Interfaces.ILogger, Infrastructure.Logging.Logger>();
builder.Services.AddScoped<Application.UseCases.CreateOrderUseCase>();
builder.Services.AddScoped<Application.Interfaces.IOrderRepository, Infrastructure.Data.OrderRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

BadDb.ConnectionString = app.Configuration.GetConnectionString("Sql");

app.UseCors("bad");

app.Use(async (ctx, next) =>
{
    try { await next(); } catch { await ctx.Response.WriteAsync("oops"); }
});

app.MapGet("/health", (Application.Interfaces.ILogger logger) =>
{
    logger.Log("health ping");
    var x = new Random().Next();
    if (x % 13 == 0) throw new ArgumentException("x no puede ser múltiplo de 13");
    return "ok " + x;
});

app.MapPost("/orders", (HttpContext http, CreateOrderUseCase uc) =>
{
    using var reader = new StreamReader(http.Request.Body);
    var body = reader.ReadToEnd();
    var parts = (body ?? "").Split(',');
    var customer = parts.Length > 0 ? parts[0] : "anon";
    var product = parts.Length > 1 ? parts[1] : "unknown";
    var qty = parts.Length > 2 ? int.Parse(parts[2]) : 1;
    var price = parts.Length > 3 ? decimal.Parse(parts[3]) : 0.99m;

    var order = uc.Execute(customer, product, qty, price);

    return Results.Ok(order);
});

app.MapGet("/orders/last", () => Domain.Services.OrderService.LastOrders);

app.MapGet("/info", (IConfiguration cfg) => new
{
    sql = BadDb.ConnectionString,
    env = Environment.GetEnvironmentVariables(),
    version = "v0.0.1-unsecure"
});

app.Run();
