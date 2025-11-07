namespace StudentManagement.Domain.Entities
{
    public class Teacher
    {
        public Guid TeacherId { get; private set; }
        public string FullName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string? Email { get; private set; }
        public int StatusTeacher { get; private set; }
        public string HashPassword { get; private set; }

        // Constructor factory
        public static Teacher Create(string fullname, string phoneNumber, string? email, string hashedPassword)
        {
            // ... (thêm logic validation ở đây nếu cần)
            return new Teacher
            {
                TeacherId = Guid.NewGuid(),
                FullName = fullname,
                PhoneNumber = phoneNumber,
                Email = email,
                HashPassword = hashedPassword,
                StatusTeacher = 1
            };
        }



        // Phương thức để update, đảm bảo tính nhất quán
        public void Update(string? fullname, string? phoneNumber, string? email)
        {
            FullName = fullname;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        // Phương thức để update, đảm bảo tính nhất quán
        public void UpdateStatus(int status)
        {
            StatusTeacher = status;
        }
    }
}
