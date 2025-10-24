using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure.Persistence.Configurations
{
    public class SchoolYearsConfiguration : IEntityTypeConfiguration<SchoolYears>
    {
        public void Configure(EntityTypeBuilder<SchoolYears> builder)
        {
            // Chỉ cho EF Core biết thuộc tính nào là khóa chính
            builder.HasKey(c => c.SchoolYearId);

            // Cấu hình các thứ khác
            builder.Property(c => c.YearName)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
