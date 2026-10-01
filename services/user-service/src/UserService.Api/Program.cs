using UserService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

#region Connection Strings

var connectionString =
    builder.Configuration.GetConnectionString("Postgres");

#endregion

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.Run();