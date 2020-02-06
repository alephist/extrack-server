using System.Threading.Tasks;

namespace ExTrackAPI.Contracts
{
    public interface IWrapperRepository
    {
        ICategoryRepository Category { get; }
        ITransactionRepository Transaction { get; }
        Task Save();
    }
}