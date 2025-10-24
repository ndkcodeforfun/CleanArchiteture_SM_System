namespace StudentManagement.Domain.Entities
{
    public class Classes
    {
        public Guid ClassId { get; set; }
        public string ClassName { get; set; }
        public Guid SchoolYearId { get; set; }

        public static Classes Create(string className, Guid yearId)
        {
            // ... (thêm logic validation ở đây nếu cần)
            return new Classes
            {
                ClassId = Guid.NewGuid(),
                ClassName = className,
                SchoolYearId = yearId
            };
        }
    }
}
