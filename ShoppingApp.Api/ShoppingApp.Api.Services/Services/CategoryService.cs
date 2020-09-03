using AutoMapper;
using Microsoft.Extensions.Logging;
using ShoppingApp.Api.API.Common.Exceptions;
using ShoppingApp.Api.API.Data.Contracts;
using ShoppingApp.Api.API.Data.Entities;
using ShoppingApp.Api.Services.Contracts;
using ShoppingApp.Api.Services.Model.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingApp.Api.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryService> _logger;
        private readonly IMapper _mapper;
        private readonly ILocalFileService _localFileService;

        public CategoryService(
            ICategoryRepository categoryRepository,
            ILogger<CategoryService> logger,
            IMapper mapper,
            ILocalFileService localFileService)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
            _mapper = mapper;
            _localFileService = localFileService;
        }

        public async Task<CategoryResourceModel> GetById(int id)
        {
            var result = await _categoryRepository.GetAsync(id);
            if (result == null) throw new CategoryServiceException
            { Errors = new Dictionary<string, string[]> { { "Category", new string[] { $"Could not find category with id {id}" } } } };

            return _mapper.Map<CategoryResourceModel>(result);
        }

        public async Task<List<CategoryResourceModel>> GetAll()
        {
            List<CategoryResourceModel> categories = new List<CategoryResourceModel>();

            var results = await _categoryRepository.GetAllAsync();
            categories.AddRange(_mapper.Map<List<CategoryResourceModel>>(results));

            return categories;
        }

        public async Task<CategoryResourceModel> Create(CreateCategoryResourceModel model)
        {
            try
            {
                var categoryEntity = _mapper.Map<CategoryEntity>(model);
                categoryEntity.ImageUrl = await _localFileService.SaveImage(model.Image, FileMode.Create);
                categoryEntity = await _categoryRepository.CreateAsync(categoryEntity);
                return _mapper.Map<CategoryResourceModel>(categoryEntity);
            }
            catch (Exception e)
            {
                throw new CategoryServiceException("Failed to create new category", e);
            }
        }

        public async Task<CategoryResourceModel> Update(UpdateCategoryResourceModel model)
        {
            try
            {
                var categoryEntity = _mapper.Map<CategoryEntity>(model);
                categoryEntity.ImageUrl = await _localFileService.SaveImage(model.Image, FileMode.Create);
                categoryEntity = await _categoryRepository.UpdateAsync(categoryEntity);
                return _mapper.Map<CategoryResourceModel>(categoryEntity);
            }
            catch (Exception e)
            {
                throw new CategoryServiceException("Failed to update category", e);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                await _categoryRepository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                throw new CategoryServiceException("Failed to delete a category", e);
            }
        }
    }
}