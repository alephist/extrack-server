using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ExTrackAPI.Contracts;
using ExTrackAPI.Dto;

namespace ExTrackAPI.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IWrapperRepository _repo;
        private readonly IMapper _mapper;

        public StatisticsController(IWrapperRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatisticsByCategory(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var results = await _repo.Statistics.GetStatisticsByCategory(userId);

            var recentTransactions = await _repo.Statistics.GetRecentTransactions(userId);

            var transactionsToReturn = _mapper.Map<IEnumerable<TransactionForDetailDto>>(recentTransactions);

            return Ok(new { ChartData = results, RecentTransactions = transactionsToReturn });
        }
    }
}