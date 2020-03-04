using System.Threading.Tasks;

namespace ExTrackAPI.Contracts
{
    public interface IWrapperRepository
    {
        ICategoryRepository Category { get; }
        ITransactionRepository Transaction { get; }
        IStatisticsRepository Statistics { get; }
        IAuthRepository Auth { get; }
        IUserRepository User { get; }
        Task Save();
    }
}