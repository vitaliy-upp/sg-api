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
    public class KidPersonalityController : ControllerBase
    {
        private readonly ILogger<KidProfileController> _logger;
        private readonly IUserService _userApplicationService;
        private readonly IKidProfileService _kidProfileService;

        public KidPersonalityController(IUserService userApplicationService
            , ILogger<KidProfileController> logger
            , IKidProfileService kidProfileService)
        {
            _logger = logger;
            _userApplicationService = userApplicationService;
            _kidProfileService = kidProfileService;
        }


        /// <summary>
        /// Get a Kid profile
        /// </summary>
        /// <returns>Kid Profile Model</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(KidProfileDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var result = await _kidProfileService.GetByIdAsync(id);
            return Ok(result);
        }



        /// <summary>
        /// Update kid profile
        /// </summary>
        /// <param name="model">Update Kid Model</param>
        /// <param name="id">Id of a kid</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(KidProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromForm] KidProfileDto model, [FromRoute] int id)
        {
            try
            {
                var result = await _kidProfileService.UpdateAsync(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }            
        }

        /// <summary>
        /// Create kid profile
        /// </summary>
        /// <param name="model">A new Kid profile</param>
        [HttpPost]
        [ProducesResponseType(typeof(KidProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromForm] KidProfileDto model)
        {
            var parent = await _userApplicationService.GetUserByIdentityAsync(User.Identity);
            try
            {
                model.ParrentId = parent.Id;
                var result = await _kidProfileService.CreateAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
