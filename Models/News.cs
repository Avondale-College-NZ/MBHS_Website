namespace MBHS_Website.Models
{
    public class News
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Post { get; set; }
        public string TeacherId { get; set; }
        public DateTime Date {get; set;}
        public Teacher? Teacher { get; set; }
    }
}
