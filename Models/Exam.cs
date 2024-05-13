namespace MBHS_Website.Models
{
	public class Exam
	{
		public int ExamId { get; set; }
		public int SubjectId { get; set; }
		public DateTime Date { get; set; }
		public Subject Subject { get; set; }

		public ICollection<Grade> Grades { get; set; }
	}
}
