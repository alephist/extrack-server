using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExTrackAPI.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}