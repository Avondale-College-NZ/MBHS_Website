using System.ComponentModel.DataAnnotations;

namespace MBHS_Website.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        //RegEx validation to limit string length to 40 characters and only only allow for certain characters
        [Required]
        [RegularExpression(@"[a-zA-ZāàáâäãåąčćęèéêëėįìíîïłńōòóôöõøùúûüųūÿýżźñçčšžæĀÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'\-\s]{1,40}$", ErrorMessage = "Enter a valid name")]
        public string Title { get; set; }
        //data annotation to not allow more than 30 characters in the field
        [Required]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]   
        public string Building { get; set; }
        public ICollection<Subject>? Subjects { get; set; }
    }
}
