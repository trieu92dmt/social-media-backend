using Microsoft.EntityFrameworkCore;
using PostService.Infrastructure;
using PostService.Infrastructure.Persistence;
using PostService.Application.Features.Posts.CreatePost;
using Serilog;
using FluentValidation;
using FluentValidation.AspNetCore;
using PostService.Api.Validators;
using HealthChecks.NpgSql;

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

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<CreatePostHandler>();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<
    CreatePostRequestValidator>();

builder.Services.AddHealthChecks()
    .AddNpgSql(connectionString!);

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
    Log.Information("Post Service is running at {Time}", DateTime.UtcNow);

    return Results.Ok(new
    {
        Service = "PostService",
        Status = "Running",
        Time = DateTime.UtcNow
    });
});

app.MapHealthChecks("/health");

#endregion

app.Run();