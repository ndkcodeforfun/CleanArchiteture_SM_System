using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.Student;
using StudentManagement.Application.Features.Students.Commands;
using StudentManagement.Application.Features.Students.Queries;

namespace StudentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // 3. Chỉ inject ISender (MediatR), không inject repository hay service
        private readonly ISender _sender;

        public StudentsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var query = new GetClassByIdQuery { StudentId = id };
            var studentDto = await _sender.Send(query);

            return studentDto != null ? Ok(studentDto) : NotFound();
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetStudents(int pageIndex = 1, int pageSize = 5)
        {
            var query = new GetAllStudentsQuery { index = pageIndex, size = pageSize };
            var studentDto = await _sender.Send(query);

            return studentDto != null ? Ok(studentDto) : NotFound();
        }

        [HttpPut("UpdateStatusStudent")]
        public async Task<IActionResult> UpdateStatusStudent(ChangeStatusCommand command)
        {
            // 4. Gửi Command đến Application layer
            var studentId = await _sender.Send(command);

            // Trả về 201 Created với ID của tài nguyên mới
            return CreatedAtAction(nameof(GetStudentById), new { id = studentId }, command);
        }

        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(Guid Id, [FromBody] UpdateStudentDtoRequest request)
        {
            var command = new UpdateStudentCommand
            {
                StudentId = Id,
                FullName = request.FullName,
                Address = request.Address,
                DateOfBirth = request.DateOfBirth,
            };
            // 4. Gửi Command đến Application layer
            var studentId = await _sender.Send(command);

            // Trả về 201 Created với ID của tài nguyên mới
            return CreatedAtAction(nameof(GetStudentById), new { id = studentId }, command);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentCommand command)
        {
            // 4. Gửi Command đến Application layer
            var newStudent = await _sender.Send(command);

            // Trả về 201 Created với ID của tài nguyên mới
            return Ok(new
            {
                studentId = newStudent.studentId,
                Parents = newStudent.passes.Select(p => new
                {
                    PhoneNumber = p.phone,
                    Pass = p.pass,
                    Relation = p.relation
                })
            });
        }
    }
}
