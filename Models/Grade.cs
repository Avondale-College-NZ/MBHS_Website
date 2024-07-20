using System.ComponentModel.DataAnnotations;

namespace MBHS_Website.Models
{
	public class Grade
	{
		public int GradeId { get; set; }

        //Validation through data annotation to only allow a grade as percentage between 0 and 100
        [Range(0,100, ErrorMessage = "Please enter a valid value between 0 and 100")]
        public int Mark { get; set; }
		public int ExamId { get; set; }
		public int StudentId { get; set; }
		public Exam? Exam { get; set; }
		public Student? Student { get; set; }


	}
}
