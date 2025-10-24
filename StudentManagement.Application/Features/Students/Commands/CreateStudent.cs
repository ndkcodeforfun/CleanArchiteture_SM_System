using MediatR;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Features.Students.Commands
{
    // 1. Định nghĩa Command (dữ liệu đầu vào)
    public class CreateStudentCommand : IRequest<Guid> // Trả về Id của sinh viên mới
    {
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }

    // 2. Định nghĩa Handler (logic xử lý)
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            // 3. Validation (có thể dùng FluentValidation behavior)
            // ...

            // 4. Tạo Entity từ Domain
            var student = Student.Create(
                request.FullName,
                request.Address,
                request.DateOfBirth
            );

            // 5. Dùng Repository (từ Domain interface) để thêm
            await _unitOfWork.StudentRepository.InsertAsync(student, cancellationToken);

            // Cần gọi SaveChangesAsync() ở đây nếu dùng UoW
            // await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 6. Trả về kết quả
            return student.StudentId;
        }
    }
}
