using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Extenstions;
using Restaurants.Infrastructure.Seeders;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


builder.Host.UseSerilog((context,confg) => 

    confg.
    MinimumLevel.Override("Microsoft", LogEventLevel.Warning).
    MinimumLevel.Override("Microsoft.EntityFrameWorkCore", LogEventLevel.Warning).
    WriteTo.Console(outputTemplate:
    " [{ Timestamp: dd - MM HH: mm: ss}\r\n{ Level: u3}] |{ SourceContext}| " +
    "{ NewLine}\r\n{ Message: lj}\r\n{ NewLine}\r\n{ Exception}")

);
//builder.Host.UseSerilog((context,confg) => {

//    confg.ReadFrom.Configuration(context.Configuration);
//});

//outputTemplate:" [{ Timestamp: dd - MM HH: mm: ss}{ Level: u3}] |{ SourceContext}| { NewLine}{ Message: lj}{ NewLine}{ Exception}

var app = builder.Build();

// Configure the HTTP request pipeline.


var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();

await seeder.Seed();

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseAuthorization();

app.MapControllers();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Run();
