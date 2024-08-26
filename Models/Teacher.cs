using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MBHS_Website.Models;

// Add profile data for application users by adding properties to the Teacher class
public class Teacher : IdentityUser
{

    //RegEx validation to limit string length to 40 characters and only only allow for certain characters
    [RegularExpression(@"[a-zA-ZāàáâäãåąčćęèéêëėįìíîïłńōòóôöõøùúûüųūÿýżźñçčšžæĀÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'\-\s]{1,40}$", ErrorMessage = "Enter a valid name")]
    [Required]
    public string FirstName { get; set; }


    //RegEx validation to limit string length to 40 characters and only only allow for certain characters
    [RegularExpression(@"[a-zA-ZāàáâäãåąčćęèéêëėįìíîïłńōòóôöõøùúûüųūÿýżźñçčšžæĀÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'\-\s]{1,40}$", ErrorMessage = "Enter a valid name")]
    [Required]
    public string LastName { get; set; }

    //Validation to aonly allow date, there's further validation in the view which stops people from entering a DOB in the future
    [DataType(DataType.Date)]
    [Required]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime DateOfBirth { get; set; }

    public ICollection<SubjectTeacher>? SubjectTeachers { get; set; }
    public ICollection<News>? News { get; set; }

}

