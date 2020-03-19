using System.Collections.Generic;
using System.Threading.Tasks;
using ExTrackAPI.Models;

namespace ExTrackAPI.Contracts
{
    public interface IStatisticsRepository
    {
        Task<IEnumerable<ChartData>> GetStatisticsByCategory(int userId);
        Task<IEnumerable<Transaction>> GetRecentTransactions(int userId);
    }
}