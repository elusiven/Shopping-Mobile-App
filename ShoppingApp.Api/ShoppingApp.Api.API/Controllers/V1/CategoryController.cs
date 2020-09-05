using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingApp.Api.Services.Contracts;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShoppingApp.Api.Services.Model.Product;

namespace ShoppingApp.Api.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/categories")]
    [Route("api/v{version:apiVersion}/categories")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("CategoryController::GetAll");
            return Ok(await _categoryService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("CategoryController::Get");
            return Ok(await _categoryService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryResourceModel model)
        {
            _logger.LogInformation("CategoryController::Create");
            var category = await _categoryService.Create(model);
            return Created(new Uri(Url.Action("Get", new { id = category.Id }), UriKind.Relative), category);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateCategoryResourceModel model)
        {
            _logger.LogInformation("CategoryController::Update");
            var category = await _categoryService.Update(model);
            return Accepted(new Uri(Url.Action("Get", new { id = category.Id }), UriKind.Relative), category);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("CategoryController::Delete");

            try
            {
                await _categoryService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}