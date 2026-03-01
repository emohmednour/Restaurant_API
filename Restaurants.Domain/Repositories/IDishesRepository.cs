using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IDishesRepository
{
    Task<int> Create(Dish dish);
    Task DeleteRangeAsync(IEnumerable<Dish> dishes);
}
