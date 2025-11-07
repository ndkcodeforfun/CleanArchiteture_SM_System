using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;
using System.Reflection;

namespace StudentManagement.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext //, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<SchoolYears> SchoolYears { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Student_Parent> Student_Parent { get; set; }
        // public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tự động áp dụng tất cả cấu hình (IEntityTypeConfiguration)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student_Parent>()
            .HasKey(sp => new { sp.StudentId, sp.ParentId });
        }
    }
}
