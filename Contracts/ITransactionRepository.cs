using System.Collections.Generic;
using System.Threading.Tasks;
using ExTrackAPI.Models;

namespace ExTrackAPI.Contracts
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsByUser(int userId);
        Task<Transaction> GetTransaction(int transactionId);
        void CreateTransaction(Transaction transaction);
        void UpdateTransaction(Transaction transaction);
        void DeleteTransaction(Transaction transaction);
    }
}