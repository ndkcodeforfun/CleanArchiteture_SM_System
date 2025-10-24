using MediatR;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Features.Students.Commands
{
    // 1. Định nghĩa Command (dữ liệu đầu vào)
    public class UpdateStudentCommand : IRequest<Guid> // Trả về Id của sinh viên mới
    {
        public Guid StudentId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }

    // 2. Định nghĩa Handler (logic xử lý)
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _unitOfWork.StudentRepository.GetByIDAsync(request.StudentId);
            if (student != null)
            {
                student.Update(request.FullName, request.Address, request.DateOfBirth);
                await _unitOfWork.StudentRepository.UpdateAsync(student);
            }

            // 6. Trả về kết quả
            return student.StudentId;
        }
    }
}
