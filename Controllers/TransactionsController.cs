using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ExTrackAPI.Contracts;
using ExTrackAPI.Dto;
using ExTrackAPI.Models;

namespace ExTrackAPI.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IWrapperRepository _repo;
        private readonly IMapper _mapper;

        public TransactionsController(IWrapperRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var transactions = await _repo.Transaction.GetAllTransactionsByUser(userId);

            var transactionsResult = _mapper.Map<IEnumerable<TransactionForDetailDto>>(transactions);

            return Ok(transactionsResult);
        }

        [HttpGet("{id}", Name = nameof(GetTransaction))]
        public async Task<IActionResult> GetTransaction(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var transactionFromRepo = await _repo.Transaction.GetTransaction(id);

            if (transactionFromRepo == null)
            {
                return NotFound();
            }

            var transactionResult = _mapper.Map<TransactionForDetailDto>(transactionFromRepo);

            return Ok(transactionResult);
        }


        [HttpPost]
        public async Task<IActionResult> AddTransaction(int userId, TransactionForCreationDto transaction)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var transactionEntity = _mapper.Map<Transaction>(transaction);
            transactionEntity.UserId = userId;

            _repo.Transaction.CreateTransaction(transactionEntity);
            await _repo.Save();

            var createdTransaction = _mapper.Map<TransactionDto>(transactionEntity);

            return CreatedAtRoute(nameof(GetTransaction), new { userId = userId, id = createdTransaction.Id }, createdTransaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int userId, int id, TransactionForUpdateDto transaction)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var transactionFromRepo = await _repo.Transaction.GetTransaction(id);

            if (transactionFromRepo == null)
            {
                return NotFound($"Transaction with id {id} does not exist");
            }

            _mapper.Map(transaction, transactionFromRepo);

            _repo.Transaction.UpdateTransaction(transactionFromRepo);
            await _repo.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var transactionFromRepo = await _repo.Transaction.GetTransaction(id);

            if (transactionFromRepo == null)
            {
                return NotFound($"Transaction with id {id} does not exist");
            }

            _repo.Transaction.DeleteTransaction(transactionFromRepo);
            await _repo.Save();

            return NoContent();
        }
    }
}