using System.ComponentModel.DataAnnotations;

namespace MBHS_Website.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        [RegularExpression(@"[a-zA-ZāàáâäãåąčćęèéêëėįìíîïłńōòóôöõøùúûüųūÿýżźñçčšžæÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'\-\s]{1,40}$", ErrorMessage = "Enter a valid name")]
        public string Title { get; set; }
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        public string Building { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}
