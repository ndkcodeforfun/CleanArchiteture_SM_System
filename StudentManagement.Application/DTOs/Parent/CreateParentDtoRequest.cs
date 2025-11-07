namespace StudentManagement.Application.DTOs.Parent
{
    public class CreateParentDtoRequest
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string Occupation { get; set; }
        public string Relationship { get; set; }
    }
}
