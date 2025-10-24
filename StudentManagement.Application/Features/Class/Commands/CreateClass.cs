using MediatR;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Features.Class.Commands
{
    public class CreateClassCommand : IRequest<Guid>
    {
        public string ClassName { get; set; }
        public string YearName { get; set; }
    }

    // 2. Định nghĩa Handler (logic xử lý)
    public class CreateClassCommandHandler : IRequestHandler<CreateClassCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateClassCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateClassCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var year = (await _unitOfWork.SchoolYearRepository.GetAsync(y => y.YearName == request.YearName.Trim().ToString())).FirstOrDefault();
                if (year == null)
                {
                    throw new Exception("Không tìm thấy năm học");
                }
                var newClass = Classes.Create(request.ClassName, year.SchoolYearId);


                await _unitOfWork.ClassRepository.InsertAsync(newClass, cancellationToken);

                return newClass.ClassId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
