using System.Threading.Tasks;
using ExTrackAPI.Dto;
using ExTrackAPI.Models;

namespace ExTrackAPI.Contracts
{
    public interface IUserRepository
    {
        Task<User> GetUser(int id);
        Task<bool> UsernameExist(UserForUpdateDto user);
        Task<bool> EmailExist(UserForUpdateDto user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}