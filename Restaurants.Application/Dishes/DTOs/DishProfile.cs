

using AutoMapper;
using Restaurants.Application.Dishes.Conmmands.CreateDish;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.DTOs;

public class DishProfile :Profile
{
    public DishProfile()
    {
        CreateMap<CreateDishCommand, Dish>();
        CreateMap<Dish, DishDto>();
    }

}
