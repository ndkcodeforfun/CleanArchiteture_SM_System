using AutoMapper;
using MediatR;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Features.Class.Queries
{

    public class GetClassByIdQuery : IRequest<ClassDto?>
    {
        public Guid ClassId { get; set; }
    }



    // 2. Định nghĩa Handler
    public class GetClassByIdQueryHandler : IRequestHandler<GetClassByIdQuery, ClassDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; // Dùng AutoMapper

        public GetClassByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ClassDto?> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _unitOfWork.StudentRepository.GetByIDAsync(request.ClassId, cancellationToken);

            if (student == null)
                return null;

            // 3. Map từ Entity -> DTO
            return _mapper.Map<ClassDto>(student);
        }
    }
}


