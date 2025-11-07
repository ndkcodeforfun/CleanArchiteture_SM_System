using MediatR;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Application.DTOs
{
    public class UserAuthenticatingDtoRequest : IRequest<string>
    {
        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [RegularExpression(@"^0\d{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ. Ví dụ: 0901234567")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "Số điện thoại phải từ 10 đến 11 chữ số.")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        public string? Password { get; set; }
    }
}
