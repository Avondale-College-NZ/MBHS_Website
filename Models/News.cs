using System.ComponentModel.DataAnnotations;

namespace MBHS_Website.Models
{
    public class News
    {
        public int NewsId { get; set; }
        //data annotation to not allow more than 200 characters in the field
        [StringLength(200, ErrorMessage = "Exceeded character limit.")]
        public string Title { get; set; }
        //data annotation to not allow more than 10000 characters in the field
        [StringLength(10000, ErrorMessage = "Exceeded character limit.")]
        public string Post { get; set; }
        public string? TeacherId { get; set; }
        //converts datetype format into normal date format
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date {get; set;}
        public Teacher? Teacher { get; set; }
    }
}
