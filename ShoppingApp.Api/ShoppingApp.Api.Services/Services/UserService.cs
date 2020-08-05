using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShoppingApp.Api.API.Common.Exceptions;
using ShoppingApp.Api.API.Common.Settings;
using ShoppingApp.Api.API.Data.Entities;
using ShoppingApp.Api.Services.Contracts;
using ShoppingApp.Api.Services.Model.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApp.Api.Services
{
    // TODO:: Finish user service
    public class UserService : IUserService
    {
        private AppSettings _settings;
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IOptions<AppSettings> settings,
            IMapper mapper,
            UserManager<UserEntity> userManager,
            ILogger<UserService> logger)
        {
            _settings = settings?.Value;
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<UserResourceModel> CreateAsync(CreateUserModel user)
        {
            UserResourceModel resource = new UserResourceModel();

            try
            {
                var newUser = new UserEntity()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.EmailAddress,
                    Email = user.EmailAddress,
                    Address = _mapper.Map<AddressEntity>(user.Address)
                };

                var result = await _userManager.CreateAsync(newUser, user.Password);
                var errorDictionary = new Dictionary<string, string[]>();
                errorDictionary.Add("Password", result.Errors.Select(s => s.Description).ToArray());

                if (!result.Succeeded) throw new UserServiceException("Failed to create a new user") { Errors = errorDictionary };
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to create new user. Ex: " + e);
                throw new UserServiceException("Failed to create a new user", e);
            }

            return resource;
        }

        public async Task<UserResourceModel> UpdateAsync(UpdateUserModel user)
        {
            UserResourceModel resource = new UserResourceModel();

            try
            {
                var result = await _userManager.UpdateAsync(_mapper.Map<UserEntity>(user));
                if (!result.Succeeded) throw new UserServiceException("Failed to update current user");
                resource = _mapper.Map<UserResourceModel>(user);
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to update current user. Ex: " + e);
                throw new UserServiceException("Failed to update current user", e);
            }

            return resource;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            bool isDeleted = false;

            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null) throw new UserServiceException($"User with ID {id} does not exist");

                var result = await _userManager.DeleteAsync(user);
                isDeleted = result.Succeeded;
                if (!result.Succeeded) throw new UserServiceException("Failed to delete a user");
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to delete user. Ex: " + e);
            }

            return isDeleted;
        }

        public async Task<UserResourceModel> GetAsync(string id)
        {
            UserResourceModel resource = new UserResourceModel();

            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null) throw new UserServiceException($"User with ID of {id} is not found");
                resource = _mapper.Map<UserResourceModel>(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return resource;
        }
    }
}