using System.Threading.Tasks;

namespace ExTrackAPI.Contracts
{
    public interface IWrapperRepository
    {
        ICategoryRepository Category { get; }
        Task Save();
    }
}