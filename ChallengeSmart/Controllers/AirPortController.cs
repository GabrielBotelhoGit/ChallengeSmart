using ChallengeSmart.DTOS;
using ChallengeSmart.Exceptions;
using ChallengeSmart.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ChallengeSmart.Controllers
{
    [ApiController]
    [Route("api/airport")]
    public class AirPortController : Controller
    {
        private readonly IAirPortService _airPortService;
        public AirPortController(IAirPortService airPortService)
        {
            _airPortService = airPortService;
        }

        /// <summary>
        /// Calculate the distance between two airports and gives the awnser in miles.
        /// </summary>
        /// <returns></returns>
        [HttpPost("calculateDistance")]
        [ProducesResponseType(typeof(CalculateDistanceResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetDistanceAsync([FromBody, Required] CalculateDistanceRequestDTO calculateDistanceRequest)
        {
            try
            {
                return Ok(await _airPortService.CalculateDistanceAsync(calculateDistanceRequest));
            }
            catch (AirPortException ex)
            {

                return BadRequest(ex.Message);
            }            
        }
    }
}
