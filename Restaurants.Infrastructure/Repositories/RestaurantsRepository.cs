using Microsoft.EntityFrameworkCore;
using Restaurants.Infrastructure.Persistance;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Entities;


namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository(RestaurantDbContext dbcontext) : IRestaurantsRepository
{
    public async Task<int> Create(Restaurant restaurant)
    {
        await dbcontext.Restaurants.AddAsync(restaurant);
        await dbcontext.SaveChangesAsync();
        return restaurant.Id;
    }

    public async Task Delete(Restaurant rest)
    {
         dbcontext.Restaurants.Remove(rest);
       await dbcontext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Restaurant?>> GetAllAsync()
    {
        var resturants = await dbcontext.Restaurants.ToListAsync();
        return resturants;
    }



    public async Task<(IEnumerable<Restaurant>,int )> GetAllMatchingAsync(
        string? searchPhrase,int pageSize, int pageNumber)
    {
      
        var searchPhraselower = searchPhrase?.ToLower();

        var query =  dbcontext.Restaurants.
            Where(r=> searchPhraselower == null || ( r.Name.ToLower().Contains(searchPhraselower)
                                                       || r.Description.ToLower().Contains(searchPhraselower)));
                                                       


        var totalCount = await query.CountAsync();

        var restaurants = await query.
            Skip(pageSize * (pageNumber - 1)).
            Take(pageSize).ToListAsync();


            return (restaurants, totalCount);
                

        
    }

    public async Task<Restaurant?> GetAsync(int Id)
    {
        var rest = await dbcontext.Restaurants.Include(i=>i.Dishes)
                                              .FirstOrDefaultAsync(u => u.Id == Id);

        return rest;
    }

    public Task SaveChanges() => dbcontext.SaveChangesAsync();
    

    
}
