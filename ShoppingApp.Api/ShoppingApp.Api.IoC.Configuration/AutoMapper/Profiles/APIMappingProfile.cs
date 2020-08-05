using AutoMapper;
using ShoppingApp.Api.API.Data.Entities;
using ShoppingApp.Api.Services.Model;
using ShoppingApp.Api.Services.Model.User;

namespace ShoppingApp.Api.IoC.Configuration.AutoMapper.Profiles
{
    public class APIMappingProfile : Profile
    {
        public APIMappingProfile()
        {
            RegisterUserMaps();
        }

        private void RegisterUserMaps()
        {
            // Create
            CreateMap<CreateUserModel, UserEntity>().ReverseMap();
            // View Model
            CreateMap<UserEntity, UserResourceModel>().ReverseMap();
            CreateMap<UserEntity, UpdateUserModel>().ReverseMap();
            // Dependency of user
            CreateMap<AddressEntity, AddressModel>().ReverseMap();
        }
    }
}