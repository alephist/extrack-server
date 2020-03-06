using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExTrackAPI.Contracts;

namespace ExTrackAPI.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IWrapperRepository _repo;

        public StatisticsController(IWrapperRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatisticsByCategory(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var results = await _repo.Statistics.GetStatisticsByCategory(userId);

            return Ok(results);
        }
    }
}