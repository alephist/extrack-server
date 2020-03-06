using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExTrackAPI.Contracts;
using ExTrackAPI.Models;

namespace ExTrackAPI.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DataContext context) : base(context) { }

        public void CreateTransaction(Transaction transaction)
        {
            Create(transaction);
        }

        public void DeleteTransaction(Transaction transaction)
        {
            Delete(transaction);
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsByUser(int userId)
        {
            return await GetByCondition(t => t.UserId == userId)
                            .OrderByDescending(t => t.Date)
                            .Include(t => t.Category)
                            .ToListAsync();
        }

        public async Task<Transaction> GetTransaction(int transactionId)
        {
            return await GetByCondition(t => t.Id == transactionId).FirstOrDefaultAsync();
        }

        public void UpdateTransaction(Transaction transaction)
        {
            Update(transaction);
        }
    }
}