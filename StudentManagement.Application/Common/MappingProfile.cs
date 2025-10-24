using AutoMapper;
using StudentManagement.Application.Features.Students.Queries;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Định nghĩa quy tắc map từ Student (Entity) sang StudentDto (DTO)
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.Age,
                           opt => opt.MapFrom(src => CalculateAge(src.DateOfBirth)));
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            var age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;
            return age;
        }
    }
}
