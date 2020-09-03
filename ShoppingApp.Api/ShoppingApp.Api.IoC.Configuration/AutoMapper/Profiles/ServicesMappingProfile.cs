using AutoMapper;
using ShoppingApp.Api.API.Data.Entities;
using ShoppingApp.Api.Services.Model.Product;

namespace ShoppingApp.Api.IoC.Configuration.AutoMapper.Profiles
{
    public class ServicesMappingProfile : Profile
    {
        public ServicesMappingProfile()
        {
            CreateMap<CategoryEntity, CreateCategoryResourceModel>().ReverseMap();
            CreateMap<CategoryEntity, UpdateCategoryResourceModel>().ReverseMap();
            CreateMap<CategoryEntity, CategoryResourceModel>().ReverseMap();
        }
    }
}