using System.ComponentModel.DataAnnotations;

namespace MBHS_Website.Models
{
    public class News
    {
        public int NewsId { get; set; }
        [StringLength(200, ErrorMessage = "Exceeded character limit.")]
        public string Title { get; set; }
        [StringLength(10000, ErrorMessage = "Exceeded character limit.")]
        public string Post { get; set; }
        public string? TeacherId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date {get; set;}
        public Teacher? Teacher { get; set; }
    }
}
