using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingApp.Api.Services.Contracts;
using System;
using System.Threading.Tasks;
using ShoppingApp.Api.Services.Model;
using ShoppingApp.Api.Services.Model.User;

namespace ShoppingApp.Api.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/users")]
    [Route("api/v{version:apiVersion}/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

#pragma warning disable CS1591

        public UserController(IUserService service, IMapper mapper, ILogger<UserController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

#pragma warning restore CS1591

        #region GET

        /// <summary>
        /// Returns a user entity according to the provided Id.
        /// </summary>
        /// <remarks>
        /// XML comments included in controllers will be extracted and injected in Swagger/OpenAPI file.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// Returns a user entity according to the provided Id.
        /// </returns>
        /// <response code="201">Returns the newly created item.</response>
        /// <response code="204">If the item is null.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResourceModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(UserResourceModel))]
        [HttpGet("{id}")]
        public async Task<UserResourceModel> Get(string id)
        {
            _logger.LogDebug($"UserControllers::Get::{id}");

            var data = await _service.GetAsync(id);

            if (data != null)
                return _mapper.Map<UserResourceModel>(data);
            else
                return null;
        }

        #endregion GET

        #region POST

        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>A newly created user.</returns>
        /// <response code="201">Returns the newly created item.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserResourceModel))]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserModel value)
        {
            _logger.LogDebug($"UserControllers::Post::");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _service.CreateAsync(value));
        }

        #endregion POST

        #region PUT

        /// <summary>
        /// Updates an user entity.
        /// </summary>
        /// <remarks>
        /// No remarks.
        /// </remarks>
        /// <param name="parameter"></param>
        /// <returns>
        /// Returns a boolean notifying if the user has been updated properly.
        /// </returns>
        /// <response code="200">Returns a boolean notifying if the user has been updated properly.</response>
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserModel parameter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _service.UpdateAsync(parameter));
        }

        #endregion PUT

        #region DELETE

        /// <summary>
        /// Deletes an user entity.
        /// </summary>
        /// <remarks>
        /// No remarks.
        /// </remarks>
        /// <param name="id">User Id</param>
        /// <returns>
        /// Boolean notifying if the user has been deleted properly.
        /// </returns>
        /// <response code="200">Boolean notifying if the user has been deleted properly.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteDevice(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _service.DeleteAsync(id));
        }

        #endregion DELETE

        #region Excepions

        [HttpGet("exception/{message}")]
        [ProducesErrorResponseType(typeof(Exception))]
        public async Task RaiseException(string message)
        {
            _logger.LogDebug($"UserControllers::RaiseException::{message}");

            throw new Exception(message);
        }

        #endregion Excepions
    }
}