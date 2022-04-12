using System;

namespace Lab2
{
    public class InputModel
    {
        public int? ID { get; set; }
        public string URL { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Price { get; set; }
        public string Title { get; set; }
        public string PhotoURl { get; set; }
        public string TransactionNumber { get; set; }
    }
}
