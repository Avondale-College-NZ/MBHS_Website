using System.ComponentModel.DataAnnotations;

namespace MBHS_Website.Models
{
	public class Student
	{
		public int StudentId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

		public ICollection<Grade> Grades { get; set; }
		public ICollection<StudentSubjectTeacher> StudentSubjectTeachers { get; set; }
	}
}
