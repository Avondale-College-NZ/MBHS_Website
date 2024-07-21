using System.ComponentModel.DataAnnotations;

namespace MBHS_Website.Models
{
	public class Student
	{
		public int StudentId { get; set; }

        //RegEx validation to limit string length to 40 characters and only only allow for certain characters
        [RegularExpression(@"[a-zA-ZāàáâäãåąčćęèéêëėįìíîïłńōòóôöõøùúûüųūÿýżźñçčšžæĀÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'\-\s]{1,40}$", ErrorMessage = "Enter a valid name")]
        [Required]
        public string FirstName { get; set; }

        //RegEx validation to limit string length to 40 characters and only only allow for certain characters
        [RegularExpression(@"[a-zA-ZāàáâäãåąčćęèéêëėįìíîïłńōòóôöõøùúûüųūÿýżźñçčšžæĀÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'\-\s]{1,40}$", ErrorMessage = "Enter a valid name")]
        [Required]
        public string LastName { get; set; }

        //Validation to aonly allow date, there's further validation in the view which stops people from entering a DOB in the future
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

		public ICollection<Grade>? Grades { get; set; }
		public ICollection<StudentSubjectTeacher>? StudentSubjectTeachers { get; set; }
	}
}
