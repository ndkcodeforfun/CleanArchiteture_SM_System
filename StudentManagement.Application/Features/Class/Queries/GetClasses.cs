using AutoMapper;
using MediatR;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Features.Class.Queries
{
    // DTO: Dữ liệu trả về cho client
    public class ClassDto
    {
        public Guid ClassId { get; set; }
        public string ClassName { get; set; }
        public Guid SchoolYearId { get; set; }
    }

    public class GetAllClassesQuery : IRequest<IEnumerable<ClassDto>>
    {
        public int index { get; set; }
        public int size { get; set; }
    }

    // 2. Định nghĩa Handler
    public class GetAllClassesQueryHandler : IRequestHandler<GetAllClassesQuery, IEnumerable<ClassDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllClassesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClassDto>> Handle(GetAllClassesQuery request, CancellationToken cancellationToken)
        {
            // 3. Gọi phương thức GetAllAsync từ repository
            // Lớp GenericRepository của bạn đã có sẵn phương thức này.
            var classes = await _unitOfWork.ClassRepository.GetAsync(pageIndex: request.index, pageSize: request.size, cancellationToken: cancellationToken);

            // 4. Map danh sách Entity (Student) sang DTO (StudentDto)
            var classDtos = _mapper.Map<IEnumerable<ClassDto>>(classes);

            return classDtos;
        }
    }
}
