using System.ComponentModel.DataAnnotations;

namespace MBHS_Website.Models
{
    public class SubjectTeacher
	{
		[Key]
		public int SubjectTeacherId { get; set; }
		public int SubjectId { get; set; }
		public string Id { get; set; }
		public string Room { get; set; }

		public Teacher Teacher { get; set; }

		public Subject Subject { get; set; }

		public ICollection<StudentSubjectTeacher> StudentSubjectTeachers { get; set; }	
	}
}
