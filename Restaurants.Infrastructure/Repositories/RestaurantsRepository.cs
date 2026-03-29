using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistance;
using System.Globalization;
using System.Linq.Expressions;


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
        string? searchPhrase,int pageSize, int pageNumber,string? SortBy,
        SortDirection sortDirection)
    {
      
        var searchPhraselower = searchPhrase?.ToLower();

        var query =  dbcontext.Restaurants.
            Where(r=> searchPhraselower == null || ( r.Name.ToLower().Contains(searchPhraselower)
                           
                            || r.Description.ToLower().Contains(searchPhraselower)));
                                                       


        var totalCount = await query.CountAsync();

        if (SortBy != null)
        {

            var columnSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                {nameof(Restaurant.Name),r=>r.Name }    ,
                {nameof(Restaurant.Description),r=>r.Description }    ,
                {nameof(Restaurant.Category),r=>r.Category }    

            };

            var selectedColumn = columnSelector[SortBy];

            query = sortDirection == SortDirection.Ascending
                ? query.OrderBy(selectedColumn)
                : query.OrderByDescending(selectedColumn);
        }

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
