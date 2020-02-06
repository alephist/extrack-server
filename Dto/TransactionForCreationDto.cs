using System;
using System.ComponentModel.DataAnnotations;

namespace ExTrackAPI.Dto
{
    public class TransactionForCreationDto
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}