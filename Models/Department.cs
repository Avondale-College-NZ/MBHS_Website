namespace MBHS_Website.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Title { get; set; }
        public string Building { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}
