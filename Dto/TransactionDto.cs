using System;

namespace ExTrackAPI.Dto
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
    }
}