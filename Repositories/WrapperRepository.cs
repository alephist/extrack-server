using System.Threading.Tasks;
using ExTrackAPI.Contracts;
using ExTrackAPI.Models;

namespace ExTrackAPI.Repositories
{
    public class WrapperRepository : IWrapperRepository
    {
        private readonly DataContext _context;
        private ICategoryRepository _category;
        private ITransactionRepository _transaction;
        private IStatisticsRepository _statistics;
        private IAuthRepository _auth;
        private IUserRepository _user;

        public WrapperRepository(DataContext context)
        {
            _context = context;
        }

        public ICategoryRepository Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepository(_context);
                }

                return _category;
            }
        }

        public ITransactionRepository Transaction
        {
            get
            {
                if (_transaction == null)
                {
                    _transaction = new TransactionRepository(_context);
                }

                return _transaction;
            }
        }

        public IStatisticsRepository Statistics
        {
            get
            {
                if (_statistics == null)
                {
                    _statistics = new StatisticsRepository(_context);
                }

                return _statistics;
            }
        }

        public IAuthRepository Auth
        {
            get
            {
                if (_auth == null)
                {
                    _auth = new AuthRepository(_context);
                }

                return _auth;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_context);
                }

                return _user;
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}