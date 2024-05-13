namespace MBHS_Website.Models
{
    public class StudentSubjectTeacher
    {
        public int StudentSubjectTeacherId { get; set; }
        public int StudentId { get; set; }
        public int SubjectTeacherId { get; set; }
        public Student Student { get; set; }
        public SubjectTeacher SubjectTeacher { get; set; }  

    }
}
