using Restaurants.Infrastructure.Persistance;
using Restaurants.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constant;
using Microsoft.EntityFrameworkCore;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder(RestaurantDbContext db) : IRestaurantSeeder
{

    public async Task Seed()
    {
        if(db.Database.GetPendingMigrations().Any()){

            await db.Database.MigrateAsync();
        }

        if (await db.Database.CanConnectAsync())
        {
            if (!db.Restaurants.Any())
            {
                var resturants = GetRestaurants();
                db.Restaurants.AddRange(resturants);
                await db.SaveChangesAsync();
            }
            if (!db.Roles.Any()) {
                var roles = GetRoles();
                db.Roles.AddRange(roles);
                await db.SaveChangesAsync();
            }
        }
    }
    private IEnumerable<IdentityRole> GetRoles(){
    
        return [
            
            new(UserRoles.User){
            
                NormalizedName = UserRoles.User .ToUpper()
            },
            new(UserRoles.Admin){

                NormalizedName = UserRoles.Admin .ToUpper()
            },
            new(UserRoles.Owner){

                NormalizedName = UserRoles.Owner .ToUpper()
            },

            ];
    
    
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {

        User owner = new User { Email = "seed-user@test.com" };
        List<Restaurant> restaurants = [

            new()
            {
                Owner = owner,
                Name = "KFC",
                Category = "Fast Food",
                Description =
                    "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                Dishes =
                [
                    new ()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken (10 pcs.)",
                        Price = 10.30M,
                    },

                    new ()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken Nuggets (5 pcs.)",
                        Price = 5.30M,
                    },
                ],
                Address = new ()
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N 5DU"
                }
            },
            new ()
            {
                Owner = owner,

                Name = "McDonald",
                Category = "Fast Food",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                ContactEmail = "contact@mcdonald.com",
                HasDelivery = true,
                Address = new Address()
                {
                    City = "London",
                    Street = "Boots 193",
                    PostalCode = "W1F 8SR"
                }
            }
        ];

        return restaurants;

    }
}
