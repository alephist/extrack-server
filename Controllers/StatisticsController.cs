using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExTrackAPI.Contracts;
//using ExTrackAPI.Models;

namespace ExTrackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IWrapperRepository _repo;

        public StatisticsController(IWrapperRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatisticsByCategory()
        {
            var results = await _repo.Statistics.GetStatisticsByCategory();

            return Ok(results);
        }
    }
}