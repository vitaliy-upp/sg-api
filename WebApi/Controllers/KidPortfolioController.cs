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
    public class KidPortfolioController : ControllerBase
    {
        private readonly ILogger<KidPortfolioController> _logger;
        private readonly IUserService _userApplicationService;
        private readonly IKidPortfolioService _service;

        public KidPortfolioController(IUserService userApplicationService
            , ILogger<KidPortfolioController> logger
            , IKidPortfolioService kidProfileService)
        {
            _logger = logger;
            _userApplicationService = userApplicationService;
            _service = kidProfileService;
        }


        /// <summary>
        /// Returns a kid portfolio
        /// </summary>
        /// <returns>Kid portfolio Model</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(KidPortfolioDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var result = await _service.GetAsync(id);
            return Ok(result);
        }


        /// <summary>
        /// Creates a kid portfolio item
        /// </summary>
        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromForm] AttachmentDto model, [FromRoute] int id)
        {
            var parent = await _userApplicationService.GetUserByIdentityAsync(User.Identity);
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


        /// <summary>
        /// Deletes a kid portfolio item
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var parent = await _userApplicationService.GetUserByIdentityAsync(User.Identity);
            try
            {
                await _service.DeleteAsync(id, parent.Id, id);
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
