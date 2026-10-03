using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityService.Application.Features.Auth.Register;
using IdentityService.Infrastructure;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

// Add Infrastructure services
builder.Services.AddInfrastructure(
    builder.Configuration);


// Add Health Checks
builder.Services.AddHealthChecks()
    .AddNpgSql(connectionString!);


// Add MediatR
builder.Services.AddMediatR(
    cfg =>
    {
        cfg.RegisterServicesFromAssembly(
            typeof(RegisterHandler).Assembly);
    });


// Add FluentValidation
builder.Services
    .AddFluentValidationAutoValidation();

builder.Services
    .AddValidatorsFromAssemblyContaining<
        RegisterValidator>();

// Add Authorization
// builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
// builder.Services.AddAuthorization(options =>
// {
//     options.DefaultPolicy = new AuthorizationPolicyBuilder()
//         .RequireAuthenticatedUser()
//         .AddRequirements(new PermissionAuthorizationRequirement())
//         .Build();
// }); 

// Add Authentication
var jwtKey =
    builder.Configuration["Jwt:Key"]!;

builder.Services
    .AddAuthentication(
        JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration[
                        "Jwt:Issuer"],

                ValidAudience =
                    builder.Configuration[
                        "Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            jwtKey))
            };
    });
#endregion

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();

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
