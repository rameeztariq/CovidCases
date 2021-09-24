using CovidCases.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CovidCases.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CovidController : ControllerBase
    {
        private readonly ICovidService _covidService;
        public CovidController(ICovidService covidService)
        {
            _covidService = covidService;
        }
        
        [HttpGet]
        [Route("summary")]
        public async Task<IActionResult> GenerateSummary()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _covidService.GetCovidSummary();
            if (result.Succeeded == true)
            {

                return StatusCode(result.StatusCode, result.Payload);
            }

            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpGet]
        [Route("uae/history")]
        public async Task<IActionResult> GetUAEHistory()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _covidService.GetUAEHistory();
            if (result.Succeeded == true)
            {

                return StatusCode(result.StatusCode, result.Payload);
            }

            return StatusCode(result.StatusCode, result.Message);
        }

    }
}
