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

        public async Task<User> LoginUser(string email, string password)
        {
            var user = await GetByCondition(u => u.Email == email).FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            if (!VerifyPassword(user, password))
            {
                return null;
            }

            return user;
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

        private bool VerifyPassword(User user, string password)
        {
            return Crypto.VerifyHashedPassword(user.PasswordHash, password);
        }
    }
}