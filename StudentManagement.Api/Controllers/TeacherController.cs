using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.Features.Teacher.Commands;

namespace StudentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ISender _sender;

        public TeacherController(ISender sender)
        {
            _sender = sender;
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<IActionResult> Login([FromBody] UserAuthenticatingDtoRequest loginInfo)
        {
            try
            {
                IActionResult response = Unauthorized();
                var accessToken = await _sender.Send(loginInfo);
                if (accessToken != null)
                {
                    //var accessToken = await _authService.GenerateAccessToken(isAuthenticated);
                    if (string.IsNullOrEmpty(accessToken))
                    {
                        return BadRequest("Something went wrong");
                    }
                    response = Ok(new { accessToken = accessToken });
                    return response;
                }
                return NotFound("Sai sđt hoặc password");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateTeacher([FromBody] CreateTeacherCommand create)
        {
            try
            {
                var teacherId = await _sender.Send(create);
                return Ok($"Created teacher {create.FullName} successfully. UserId {teacherId}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }

}
