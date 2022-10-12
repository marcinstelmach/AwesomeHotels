using AwesomeHotels.Services.Users.Api;
using AwesomeHotels.Services.Users.Api.Mappings;
using AwesomeHotels.Services.Users.Application;
using AwesomeHotels.Services.Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiVersioning();

builder.Services.AddApi();
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddMappings();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
