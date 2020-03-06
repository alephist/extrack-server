using System;
using System.ComponentModel.DataAnnotations;

namespace ExTrackAPI.Dto
{
    public class TransactionForUpdateDto
    {
        [Required]
        [MinLength(4)]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Required]
        [Range(typeof(decimal), "0", "9223372036854775807")]
        public decimal Amount { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}