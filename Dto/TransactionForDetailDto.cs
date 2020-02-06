using System;

namespace ExTrackAPI.Dto
{
    public class TransactionForDetailDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public CategoryDto Category { get; set; }
    }
}