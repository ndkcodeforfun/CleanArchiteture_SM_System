using Microsoft.EntityFrameworkCore.Storage;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Student> StudentRepository { get; }
        IGenericRepository<Classes> ClassRepository { get; }
        IGenericRepository<SchoolYears> SchoolYearRepository { get; }
        IGenericRepository<Teacher> TeacherRepository { get; }
        IGenericRepository<Parent> ParentRepository { get; }
        IGenericRepository<Student_Parent> StudentParentRepository { get; }
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task SaveAsync();
    }
}
