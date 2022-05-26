using System;
using System.ComponentModel.DataAnnotations;

namespace LB4.DTO
{
    public class DocumentDto
    {     
        public int Id { get; set; }
        [Required]
        [Url]
        public string Url { get; set; }
        [Required]
       [DataType(DataType.Date, ErrorMessage = "Validation Error: Date - is not valid")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Url]
        public string PhotoUrl { get; set; }
        [Required]
        [RegularExpression (@"^[A-Z]{2}\-\d{3}\-\b[A-Z]{2}\/\d{2}", ErrorMessage = "Validation Error: Transaction number - is not valid(Format: AA-111-AA/11)")]
       
        public string TransactionNumber { get; set; }
    }
}
