using MBHS_Website.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MBHS_Website.Areas.Identity.Data;

public class MBHS_Context : IdentityDbContext<Teacher>
{
    public MBHS_Context(DbContextOptions<MBHS_Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<MBHS_Website.Models.Grade> Grade { get; set; } = default!;

    public DbSet<MBHS_Website.Models.Exam> Exam { get; set; } = default!;

    public DbSet<MBHS_Website.Models.Student> Student { get; set; } = default!;

    public DbSet<MBHS_Website.Models.StudentSubjectTeacher> StudentSubjectTeacher { get; set; } = default!;

    public DbSet<MBHS_Website.Models.Subject> Subject { get; set; } = default!;

    public DbSet<MBHS_Website.Models.SubjectTeacher> SubjectTeacher { get; set; } = default!;
}
