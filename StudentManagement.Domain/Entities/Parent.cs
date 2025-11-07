namespace StudentManagement.Domain.Entities
{
    public class Parent
    {
        public Guid ParentId { get; private set; }
        public string FullName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string? Email { get; private set; }
        public string HashPassword { get; private set; }
        public string? Occupation { get; private set; }

        // Constructor factory
        public static Parent Create(string fullname, string phoneNumber, string? email, string hashedPassword, string occupation)
        {
            // ... (thêm logic validation ở đây nếu cần)
            return new Parent
            {
                ParentId = Guid.NewGuid(),
                FullName = fullname,
                PhoneNumber = phoneNumber,
                Email = email,
                HashPassword = hashedPassword,
                Occupation = occupation
            };
        }



        // Phương thức để update, đảm bảo tính nhất quán
        public void Update(string? fullname, string? phoneNumber, string? email, string? occupation)
        {
            FullName = fullname;
            PhoneNumber = phoneNumber;
            Email = email;
            Occupation = occupation;
        }
    }
}
