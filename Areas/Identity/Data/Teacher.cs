using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBHS_Website.Models;
using Microsoft.AspNetCore.Identity;

namespace MBHS_Website.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Teacher class
public class Teacher : IdentityUser
{

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<SubjectTeacher> SubjectTeachers { get; set; }

}

