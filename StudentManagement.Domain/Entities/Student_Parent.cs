namespace StudentManagement.Domain.Entities
{
    public class Student_Parent
    {
        public Guid StudentId { get; set; }
        public Guid ParentId { get; set; }
        public string Relationship { get; set; }
    }
}
