using System;

namespace LB4.DTO
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string PhotoUrl { get; set; }
        public string TransactionNumber { get; set; }
    }
}
