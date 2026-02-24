using Restaurants.Domain.Entities;

using AutoMapper;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.DTOs;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<CreateRestaurantDto, Restaurant>().
            ForMember(dest => dest.Address, opt => opt.MapFrom(src=> new Address
            {
                City=src.City,
                PostalCode = src.PostalCode,
                Street = src.Street
            }));

        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(dest=>dest.City,
            option =>option.MapFrom(src=>src.Address==null ? null: src.Address.City))
            .ForMember(dest=>dest.PostalCode,
            option =>option.MapFrom(src=>src.Address==null ? null: src.Address.PostalCode))
            .ForMember(dest=>dest.Street,
            option =>option.MapFrom(src=>src.Address==null ? null: src.Address.Street))
            .ForMember(dest => dest.Dishes,opt=>opt.MapFrom(src=>src.Dishes))
            ;
    }

}
