using AutoMapper;
using MediatR;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Features.Students.Queries
{
    public class GetAllStudentsQuery : IRequest<IEnumerable<StudentDto>>
    {
        public int index { get; set; }
        public int size { get; set; }
    }

    // 2. Định nghĩa Handler
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, IEnumerable<StudentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllStudentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            // 3. Gọi phương thức GetAllAsync từ repository
            // Lớp GenericRepository của bạn đã có sẵn phương thức này.
            //Lấy học sinh đang theo học
            var students = await _unitOfWork.StudentRepository.GetAsync(pageIndex: request.index, pageSize: request.size, cancellationToken: cancellationToken);

            // 4. Map danh sách Entity (Student) sang DTO (StudentDto)
            var studentDtos = _mapper.Map<IEnumerable<StudentDto>>(students);

            return studentDtos;
        }
    }

}
