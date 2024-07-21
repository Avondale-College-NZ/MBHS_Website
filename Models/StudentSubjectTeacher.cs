using System.ComponentModel.DataAnnotations;

namespace MBHS_Website.Models
{
    public class StudentSubjectTeacher
    {
        public int StudentSubjectTeacherId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int SubjectTeacherId { get; set; }
        public Student? Student { get; set; }
        public SubjectTeacher? SubjectTeacher { get; set; }  

    }
}
