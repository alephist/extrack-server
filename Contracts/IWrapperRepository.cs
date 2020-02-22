using System.Threading.Tasks;

namespace ExTrackAPI.Contracts
{
    public interface IWrapperRepository
    {
        ICategoryRepository Category { get; }
        ITransactionRepository Transaction { get; }
        IStatisticsRepository Statistics { get; }
        Task Save();
    }
}