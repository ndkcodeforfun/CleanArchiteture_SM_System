namespace StudentManagement.Api.DTOs
{
    public class UpdateStudentDtoRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }
}
