using MediatR;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Application.Features.Teacher.Commands
{
    // 1. Định nghĩa Command (dữ liệu đầu vào)
    public class CreateTeacherCommand : IRequest<Guid> // Trả về Id của sinh viên mới
    {
        public string FullName { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [RegularExpression(@"^0\d{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ. Ví dụ: 0901234567")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "Số điện thoại phải từ 10 đến 11 chữ số.")]
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; } = string.Empty;
    }

    // 2. Định nghĩa Handler (logic xử lý)
    public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticateUser _authenticateUser;

        public CreateTeacherCommandHandler(IUnitOfWork unitOfWork, IAuthenticateUser authenticateUser)
        {
            _unitOfWork = unitOfWork;
            _authenticateUser = authenticateUser;
        }

        public async Task<Guid> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var password = await _authenticateUser.HashPassword(request.Password);
                // 4. Tạo Entity từ Domain
                var teacher = Domain.Entities.Teacher.Create(request.FullName, request.PhoneNumber, request.Email, password);


                // 5. Dùng Repository (từ Domain interface) để thêm
                await _unitOfWork.TeacherRepository.InsertAsync(teacher, cancellationToken);

                // 6. Trả về kết quả
                return teacher.TeacherId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
    }
}
