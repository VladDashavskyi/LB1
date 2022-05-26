namespace DB.Entities
{
    public class Documents
    {
        public int DocumentId { get; set; }
        public string Url { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string PhotoUrl { get; set; }
        public string TransactionNumber { get; set; }       
        //public ICollection<DocumentExt> DocumentExts { get; set; }
    }
}
