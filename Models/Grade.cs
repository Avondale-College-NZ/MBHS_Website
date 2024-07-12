using System.ComponentModel.DataAnnotations;

namespace MBHS_Website.Models
{
	public class Grade
	{
		public int GradeId { get; set; }

        //set annotation for grade out of 100
        [Range(0, 100, ErrorMessage = " Enter Valid grade")]
        public string Mark { get; set; }
		public int ExamId { get; set; }
		public int StudentId { get; set; }
		public Exam Exam { get; set; }
		public Student Student { get; set; }


	}
}
