using Serilog;
using SearchService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

#region Serilog

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

#endregion

#region Services

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);

// builder.Services.AddHealthChecks()
//     .AddRabbitMQ();

#endregion

var app = builder.Build();

#region Middleware

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#endregion

#region Health Check

app.MapGet("/", () =>
{
    Log.Information("Search Service is running at {Time}", DateTime.UtcNow);

    return Results.Ok(new
    {
        Service = "SearchService",
        Status = "Running",
        Time = DateTime.UtcNow
    });
});

// app.MapHealthChecks("/health");

#endregion

app.Run();