namespace DB.Entities
{
    public class DocumentExt
    {
        public int DocumentExtId { get; set; }
        public int DocumentId { get; set; }
        public int UserId { get; set; }
        public int DocumentStatusId { get; set; }
        public int ActionId { get; set; }
        public string Message { get; set; } = string.Empty;
        public Documents Documents { get; set; }
        public User Users { get; set; }
        public DocumentStatus DocumentStatus { get; set; }
        public ActionType ActionTypes { get; set; }
    }
}
