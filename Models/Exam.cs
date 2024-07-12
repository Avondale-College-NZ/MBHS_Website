using System.ComponentModel.DataAnnotations;

namespace MBHS_Website.Models
{
	public class Exam
	{
		public int ExamId { get; set; }
		public int SubjectId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
		public Subject Subject { get; set; }
		public ICollection<Grade> Grades { get; set; }
	}
}
