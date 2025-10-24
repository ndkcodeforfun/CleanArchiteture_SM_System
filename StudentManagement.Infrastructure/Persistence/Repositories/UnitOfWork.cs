using Microsoft.EntityFrameworkCore.Storage;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private GenericRepository<Student> _studentRepository;
        private GenericRepository<Classes> _classRepository;
        private GenericRepository<SchoolYears> _schoolYearRepository;
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }
        private bool disposed = false;
        public IGenericRepository<Student> StudentRepository => _studentRepository ?? new GenericRepository<Student>(context);
        public IGenericRepository<Classes> ClassRepository => _classRepository ?? new GenericRepository<Classes>(context);
        public IGenericRepository<SchoolYears> SchoolYearRepository => _schoolYearRepository ?? new GenericRepository<SchoolYears>(context);

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await context.Database.BeginTransactionAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
