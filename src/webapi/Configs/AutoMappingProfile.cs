using System;
using AutoMapper;
using CoffeeRoastery.BLL.Interface.Dto;
using CoffeeRoastery.BLL.Interface.Services;
using CoffeeRoastery.DAL.Domain.Models;
using webapi.Models;

namespace webapi.Configs;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductResponse>();
        CreateMap<ProductModel, ProductDto>();
        CreateMap<ProductDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow));
        CreateMap<UpdateProductModel, UpdateProductDto>();

    }
}