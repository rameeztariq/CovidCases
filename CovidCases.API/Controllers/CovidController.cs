using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidCases.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CovidController : ControllerBase
    {
    }
}
