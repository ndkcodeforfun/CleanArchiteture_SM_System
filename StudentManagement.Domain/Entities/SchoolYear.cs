namespace StudentManagement.Domain.Entities
{
    public class SchoolYears
    {
        public Guid SchoolYearId { get; private set; }
        public string YearName { get; private set; }

        public void Update(string yearName)
        {
            YearName = yearName;
        }
    }
}
