using CovidCases.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CovidCases.API.Controllers
{   /// <summary>
    /// The API will be used for generating the reports related to covid cases. The API needs an authentication token from the users API in order to be consumed.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CovidController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ICovidService _covidService;
        public CovidController(ICovidService covidService)
        {
            _covidService = covidService;
        }
        /// <summary>
        /// Get summary for the covid cases
        /// </summary>
        /// <header>
        /// User token is required
        /// </header>
        /// <returns></returns>
        [HttpGet]
        [Route("summary")]
        public async Task<IActionResult> GenerateSummary()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Call the service to fetch the data
            var result = await _covidService.GetCovidSummary();

            // On success returns the data whatever received it from the covidcase API
            if (result.Succeeded == true)
            {
                return StatusCode(result.StatusCode, result.Payload);
            }
            // On failure returns the data returns the error message with error occured
            return StatusCode(result.StatusCode, result.Message);
        }

        /// <summary>
        /// Get covid history for the UAE
        /// </summary>
        /// <header>
        /// User token is required
        /// </header>
        /// <returns></returns>
        [HttpGet]
        [Route("uae/history")]
        public async Task<IActionResult> GetUAEHistory()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Call the service to fetch the data
            var result = await _covidService.GetUAEHistory();

            // On success returns the data whatever received it from the covidcase API
            if (result.Succeeded == true)
            {
                return StatusCode(result.StatusCode, result.Payload);
            }
            // On failure returns the data returns the error message with error occured
            return StatusCode(result.StatusCode, result.Message);
        }

    }
}
