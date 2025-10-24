using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure.Persistence.Configurations
{
    public class ClassesConfiguration : IEntityTypeConfiguration<Classes>
    {
        public void Configure(EntityTypeBuilder<Classes> builder)
        {
            // Chỉ cho EF Core biết thuộc tính nào là khóa chính
            builder.HasKey(c => c.ClassId);

            // Cấu hình các thứ khác
            builder.Property(c => c.ClassName)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
