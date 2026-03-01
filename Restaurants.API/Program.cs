using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;
using Restaurants.Application.Extensions;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extenstions;
using Restaurants.Infrastructure.Seeders;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//add two row to gen swagger  
 
builder.Services.AddEndpointsApiExplorer(); // add this line to show identity endpoint in swagger
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeLoggingMiddleware>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


builder.Host.UseSerilog((context, confg) =>
{

    confg.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.


var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();

await seeder.Seed();

app.UseMiddleware < ErrorHandlingMiddleware > ();
app.UseMiddleware < RequestTimeLoggingMiddleware > ();
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();

//for dev only not for prod
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<User>();


app.UseAuthorization();

app.MapControllers();


app.Run();
