using System.ComponentModel.DataAnnotations;


namespace MBHS_Website.Models
{
    using System.ComponentModel.DataAnnotations;

   
        public class EditUserViewModel
        {
            //To avoid NullReferenceExceptions at runtime,
            //initialise Claims and Roles with a new list in the constructor.
            public EditUserViewModel()
            {
                Roles = new List<string>();
            }
            
            [Required]
            public string Id { get; set; }

            [Required]
            [Display(Name = "User Name")]
            public string UserName { get; set; }

            [Required]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [RegularExpression(@"[a-zA-ZāàáâäãåąčćęèéêëėįìíîïłńōòóôöõøùúûüųūÿýżźñçčšžæĀÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'\-\s]{1,40}$", ErrorMessage = "Enter a valid name")]
        [Display(Name = "First Name")]
            public string FirstName { get; set; }

        [RegularExpression(@"[a-zA-ZāàáâäãåąčćęèéêëėįìíîïłńōòóôöõøùúûüųūÿýżźñçčšžæĀÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'\-\s]{1,40}$", ErrorMessage = "Enter a valid name")]
        [Display(Name = "Last Name")]
            public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

            public IList<string> Roles { get; set; }
        }
 }

