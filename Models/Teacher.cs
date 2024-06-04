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

    public string FirstName { get; set; }

    public string LastName { get; set; }

    [DataType(DataType.Date)]

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

    public DateTime DateOfBirth { get; set; }

    public ICollection<SubjectTeacher> SubjectTeachers { get; set; }

}

