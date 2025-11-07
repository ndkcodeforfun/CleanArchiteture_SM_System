using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Features.Class.Commands;
using StudentManagement.Application.Features.Class.Queries;

namespace StudentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly ISender _sender;

        public ClassController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCLass([FromBody] CreateClassCommand command)
        {
            // 4. Gửi Command đến Application layer
            var classId = await _sender.Send(command);

            // Trả về 201 Created với ID của tài nguyên mới
            return Ok(new { classId });
        }

        [HttpGet]
        public async Task<IActionResult> GetCLasses(int pageIndex, int pageSize)
        {
            var command = new GetAllClassesQuery { index = pageIndex, size = pageSize };
            // 4. Gửi Command đến Application layer
            var classId = await _sender.Send(command);

            // Trả về 201 Created với ID của tài nguyên mới
            return Ok(new { classId });
        }
    }
}
