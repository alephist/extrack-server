using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CryptoHelper;
using ExTrackAPI.Contracts;
using ExTrackAPI.Models;

namespace ExTrackAPI.Repositories
{
    public class AuthRepository : BaseRepository<User>, IAuthRepository
    {
        public AuthRepository(DataContext context) : base(context) { }

        public async Task<bool> EmailExists(string email)
        {
            return await GetByCondition(u => u.Email == email).AnyAsync();
        }

        public void RegisterUser(User user, string password)
        {
            user.PasswordHash = HashPassword(password);
            Create(user);
        }

        public async Task<bool> UsernameExists(string username)
        {
            return await GetByCondition(u => u.Username == username).AnyAsync();
        }

        private string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }
    }
}