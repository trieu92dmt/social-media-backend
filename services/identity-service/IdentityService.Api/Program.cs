using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

app.MapGet("/", () =>
{
    Log.Information("API called at {Time}", DateTime.Now);
    return "Hello Microservice";
});

app.Run();