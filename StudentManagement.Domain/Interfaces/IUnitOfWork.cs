using Microsoft.EntityFrameworkCore.Storage;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Student> StudentRepository { get; }
        IGenericRepository<Classes> ClassRepository { get; }
        IGenericRepository<SchoolYears> SchoolYearRepository { get; }
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task SaveAsync();
    }
}
