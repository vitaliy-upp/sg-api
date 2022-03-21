using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.BusinessLogic.ServiceInterfaces;
using Domain.BusinessLogic.Models;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Security.Claims;
using Domain.BusinessLogic.Extensions;
using System.Collections.Generic;

namespace NoLimitTech.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly ILogger<EducationController> _logger;
        private readonly IKidEducationService _service;
        private readonly IUserService _userService;
        public EducationController(ILogger<EducationController> logger
            , IKidEducationService service
            , IUserService userService)
        {
            _logger = logger;
            _service = service;
            _userService = userService;
        }


        /// <summary>
        /// Get education information
        /// </summary>
        /// <returns>Education Profile Dto</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EducationProfileDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var result = await _service.GetByKidIdAsync(id);
            return Ok(result);
        }


        /// <summary>
        /// Update kid education
        /// </summary>
        /// <param name="model">Update Kid Model</param>
        /// <param name="id">Id of a kid</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(KidProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromForm] EducationProfileDto model, [FromRoute] int id)
        {
            var parent = await _userService.GetUserByIdentityAsync(User.Identity);
            try
            {
                await _service.UpdateAsync(id, parent.Id, model);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }            
        }

        /// <summary>
        /// Create kid education profile
        /// </summary>
        /// <param name="model">A new kid education profile</param>
        /// <param name="id">Kid id</param>
        [HttpPost("{id}")]
        [ProducesResponseType(typeof(EducationProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromForm] EducationProfileDto model, [FromRoute] int id)
        {
            var parent = await _userService.GetUserByIdentityAsync(User.Identity);
            try
            {
                await _service.CreateAsync(id, parent.Id, model);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
