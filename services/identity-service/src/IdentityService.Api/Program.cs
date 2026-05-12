using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityService.Application.Features.Auth.Register;
using IdentityService.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

#region Serilog

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

#endregion

#region Connection Strings

var connectionString =
    builder.Configuration.GetConnectionString("Postgres");

#endregion

#region Services

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(
    builder.Configuration);

builder.Services.AddHealthChecks()
    .AddNpgSql(connectionString!);

builder.Services.AddMediatR(
    cfg =>
    {
        cfg.RegisterServicesFromAssembly(
            typeof(RegisterHandler).Assembly);
    });

builder.Services
    .AddFluentValidationAutoValidation();

builder.Services
    .AddValidatorsFromAssemblyContaining<
        RegisterValidator>();

#endregion

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#region Health Check

app.MapGet("/", () =>
{
    Log.Information("Identity Service is running at {Time}", DateTime.UtcNow);

    return Results.Ok(new
    {
        Service = "IdentityService",
        Status = "Running",
        Time = DateTime.UtcNow
    });
});

app.MapHealthChecks("/health");

#endregion

app.Run();
