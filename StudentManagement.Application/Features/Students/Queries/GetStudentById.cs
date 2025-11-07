using AutoMapper;
using MediatR;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Features.Students.Queries
{
    // DTO: Dữ liệu trả về cho client
    public class StudentDto
    {
        public Guid StudentId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
    }

    // 1. Định nghĩa Query
    public class GetClassByIdQuery : IRequest<StudentDto?>
    {
        public Guid StudentId { get; set; }
    }

    // 2. Định nghĩa Handler
    public class GetClassByIdQueryHandler : IRequestHandler<GetClassByIdQuery, StudentDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; // Dùng AutoMapper

        public GetClassByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<StudentDto?> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _unitOfWork.StudentRepository.GetByIDAsync(request.StudentId, cancellationToken);

            if (student == null)
                return null;

            // 3. Map từ Entity -> DTO
            return _mapper.Map<StudentDto>(student);
        }
    }
}
