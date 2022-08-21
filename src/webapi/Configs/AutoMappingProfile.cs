using AutoMapper;
using CoffeeRoastery.BLL.Interface.Services;
using CoffeeRoastery.DAL.Domain.Models;

namespace webapi.Configs;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductResponse>();
    }
}