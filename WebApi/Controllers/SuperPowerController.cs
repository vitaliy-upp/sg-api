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
    public class SuperPowerController : ControllerBase
    {
        private readonly ILogger<SuperPowerController> _logger;
        private readonly ISuperPowerService _superPowerService;

        public SuperPowerController(
             ILogger<SuperPowerController> logger
            , ISuperPowerService superPowerServicee)
        {
            _logger = logger;
            _superPowerService = superPowerServicee;
        }


        /// <summary>
        /// Get All Super Power
        /// </summary>
        /// <returns>Super Power Model</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SuperPowerDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var list = await _superPowerService.GetAllAsync();
            return Ok(list);
        }
    }
}
