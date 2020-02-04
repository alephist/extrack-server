using System;
using System.ComponentModel.DataAnnotations;

namespace ExTrackAPI.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}