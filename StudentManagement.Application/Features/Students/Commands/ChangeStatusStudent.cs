using MediatR;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Features.Students.Commands
{
    public class ChangeStatusCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class ChangeStatusCommandHandler : IRequestHandler<ChangeStatusCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangeStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
        {
            var student = await _unitOfWork.StudentRepository.GetByIDAsync(request.Id);
            if (student != null)
            {
                if (student.StatusStudent == 1)
                {
                    student.UpdateStatus(2);
                    await _unitOfWork.StudentRepository.UpdateAsync(student);
                }
                else
                {
                    student.UpdateStatus(1);
                    await _unitOfWork.StudentRepository.UpdateAsync(student);
                }
            }

            // 6. Trả về kết quả
            return student.StudentId;
        }
    }
}
