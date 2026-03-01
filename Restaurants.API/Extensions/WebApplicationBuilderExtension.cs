using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;
using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Extenstions;
using Serilog;

namespace Restaurants.API.Extensions;

public static  class WebApplicationBuilderExtension
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        //add two row to gen swagger  

        builder.Services.AddEndpointsApiExplorer(); // add this line to show identity endpoint in swagger
        builder.Services.AddSwaggerGen(option => {

            option.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
            {

                Type = SecuritySchemeType.Http,
                Scheme = "bearer",



            });

            option.AddSecurityRequirement(new OpenApiSecurityRequirement {

        {
            new OpenApiSecurityScheme{

                Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme,Id = "BearerAuth" },


            },[]
        }
    });


        });

        builder.Services.AddScoped<ErrorHandlingMiddleware>();
        builder.Services.AddScoped<RequestTimeLoggingMiddleware>();

        


        builder.Host.UseSerilog((context, confg) =>
        {

            confg.ReadFrom.Configuration(context.Configuration);
        });

    }
}
