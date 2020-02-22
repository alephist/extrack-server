using ExTrackAPI.Models;
using ExTrackAPI.Contracts;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ExTrackAPI.Repositories
{
    public class StatisticsRepository : BaseRepository<Transaction>, IStatisticsRepository
    {
        public StatisticsRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<ChartData>> GetStatisticsByCategory()
        {
            return await GetAll()
                .Include(t => t.Category)
                .GroupBy(t => t.Category.Name)
                .Select(g => new ChartData { Label = g.Key, Data = g.Sum(t => t.Amount) })
                .ToListAsync();
        }
    }
}