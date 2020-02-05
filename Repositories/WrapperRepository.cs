using System.Threading.Tasks;
using ExTrackAPI.Contracts;
using ExTrackAPI.Models;

namespace ExTrackAPI.Repositories
{
    public class WrapperRepository : IWrapperRepository
    {
        private readonly DataContext _context;
        private ICategoryRepository _category;

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

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}