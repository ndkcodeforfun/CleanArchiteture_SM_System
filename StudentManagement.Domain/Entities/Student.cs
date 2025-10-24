namespace StudentManagement.Domain.Entities
{
    public class Student
    {
        public Guid StudentId { get; private set; }
        public string FullName { get; private set; }
        public string? Address { get; private set; }
        public int StatusStudent { get; private set; }
        public DateTime DateOfBirth { get; private set; }

        // Constructor factory
        public static Student Create(string fullname, string email, DateTime dob)
        {
            // ... (thêm logic validation ở đây nếu cần)
            return new Student
            {
                StudentId = Guid.NewGuid(),
                FullName = fullname,
                Address = email,
                DateOfBirth = dob
            };
        }

        // Phương thức để update, đảm bảo tính nhất quán
        public void Update(string fullname, string address, DateTime dob)
        {
            FullName = fullname;
            Address = address;
            DateOfBirth = dob;
        }

        // Phương thức để update, đảm bảo tính nhất quán
        public void UpdateStatus(int status)
        {
            StatusStudent = status;
        }
    }


}
