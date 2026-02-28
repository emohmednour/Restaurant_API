using Restaurants.Domain.Entities;
namespace Restaurants.Domain.Repositories;

public interface IRestaurantsRepoSitory
{
    Task<IEnumerable<Restaurant?>> GetAllAsync();
    Task<Restaurant?> GetAsync(int Id);
    Task<int> Create(Restaurant restaurant);
    Task Delete(Restaurant rest);
    Task SaveChanges();
}
