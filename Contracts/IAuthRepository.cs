using System.Threading.Tasks;
using ExTrackAPI.Models;

namespace ExTrackAPI.Contracts
{
    public interface IAuthRepository
    {
        void RegisterUser(User user, string password);
        Task<bool> UsernameExists(string username);
        Task<bool> EmailExists(string email);
    }
}