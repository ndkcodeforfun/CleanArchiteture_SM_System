using MediatR;
using StudentManagement.Application.DTOs.Parent;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Features.Students.Commands
{
    // 1. Định nghĩa Command (dữ liệu đầu vào)
    public class CreateStudentCommand : IRequest<(Guid studentId, List<(string phone, string pass, string relation)> passes)> // Trả về Id của sinh viên mới
    {
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        public ICollection<CreateParentDtoRequest> Parents { get; set; }
    }

    // 2. Định nghĩa Handler (logic xử lý)
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, (Guid studentId, List<(string phone, string pass, string relation)> passes)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticateUser _authenticateUser;

        public CreateStudentCommandHandler(IUnitOfWork unitOfWork, IAuthenticateUser authenticateUser)
        {
            _unitOfWork = unitOfWork;
            _authenticateUser = authenticateUser;
        }

        public async Task<(Guid studentId, List<(string phone, string pass, string relation)> passes)> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    // 4. Tạo Entity từ Domain
                    var student = Student.Create(
                        request.FullName,
                        request.Address,
                        request.DateOfBirth
                    );

                    // 5. Dùng Repository (từ Domain interface) để thêm
                    await _unitOfWork.StudentRepository.InsertAsync(student, cancellationToken);

                    List<(string phone, string pass, string relation)> newPasses = new List<(string phone, string pass, string relation)>();

                    foreach (var parent in request.Parents)
                    {
                        var newPass = await _authenticateUser.TaoMatKhauSo(8);
                        var newHashedPassword = await _authenticateUser.HashPassword(newPass);
                        var newParent = Parent.Create(
                                parent.FullName,
                                parent.PhoneNumber,
                                parent.Email,
                                newHashedPassword,
                                parent.Occupation
                            );
                        await _unitOfWork.ParentRepository.InsertAsync(newParent, cancellationToken);
                        var relationship = new Student_Parent
                        {
                            StudentId = student.StudentId,
                            ParentId = newParent.ParentId,
                            Relationship = parent.Relationship
                        };
                        await _unitOfWork.StudentParentRepository.InsertAsync(relationship, cancellationToken);

                        newPasses.Add((parent.PhoneNumber, newPass, parent.Relationship));
                    }
                    await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();
                    // 6. Trả về kết quả
                    return (student.StudentId, newPasses);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw new Exception(ex.Message, ex);
                }

            }
        }
    }
}
