using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExTrackAPI.Contracts;
using ExTrackAPI.Dto;
using ExTrackAPI.Models;

namespace ExTrackAPI.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context) { }

        public void DeleteUser(User user)
        {
            Delete(user);
        }

        public async Task<bool> EmailExist(UserForUpdateDto user)
        {
            return await GetByCondition(u => u.Email == user.Email).AnyAsync();
        }

        public async Task<User> GetUser(int id)
        {
            return await GetByCondition(u => u.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateUser(User user)
        {
            Update(user);
        }

        public async Task<bool> UsernameExist(UserForUpdateDto user)
        {
            return await GetByCondition(u => u.Username == user.Username).AnyAsync();
        }
    }
}