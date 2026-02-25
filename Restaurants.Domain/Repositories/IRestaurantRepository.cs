using Restaurants.Domain.Entities;
namespace Restaurants.Domain.Repositories;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant?>> GetAllAsync();
    Task<Restaurant?> GetAsync(int Id);
    Task<int> Create(Restaurant restaurant);
    Task Delete(Restaurant rest);
    Task SaveChanges();
}
