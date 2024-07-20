using System.ComponentModel.DataAnnotations;

namespace MBHS_Website.Models
{
	public class Subject
	{
		public int SubjectId { get; set; }

        //RegEx validation to limit string length to 40 characters and only only allow for certain characters
        [RegularExpression(@"[a-zA-ZāàáâäãåąčćęèéêëėįìíîïłńōòóôöõøùúûüųūÿýżźñçčšžæÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'\-\s]{1,40}$", ErrorMessage = "Enter a valid name")]
        public string Title { get; set; }
		public int DepartmentId { get; set; }
		public Department Department { get; set; }
		public ICollection<Exam> Exams { get; set;}
		public ICollection<SubjectTeacher> SubjectTeachers { get; set; }	
	}
}
