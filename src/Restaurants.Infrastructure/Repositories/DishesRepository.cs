using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Repositories
{
    internal class DishesRepository(RestaurantDbContext dbContext) : IDishesRepository
    {
        public async Task<int> Create(Dish dish)
        {

            dbContext.Dishes.Add(dish);
            await dbContext.SaveChangesAsync();

            return dish.Id;

        }

        public async Task DeleteRangeAsync(IEnumerable<Dish> dishes)
        {
            dbContext.Dishes.RemoveRange(dishes);
            await dbContext.SaveChangesAsync();

           

        }
    }
}
