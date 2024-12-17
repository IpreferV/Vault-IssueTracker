namespace Vault_IssueTracker.Model
{
    public class Report
    {
        public int id { get; set; }
        public string title { get; set; }
        public int userId { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string priority { get; set; }
        public DateTime date_create { get; set; }
        public DateTime date_update { get; set; }
        public string? notes { get; set; }
    }
}
