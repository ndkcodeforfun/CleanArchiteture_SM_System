using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Features.Teacher.Commands
{
    public class AuthenticateTeacherCommandHandler : IRequestHandler<UserAuthenticatingDtoRequest, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticateUser _authenticateUser;
        private readonly IMapper _mapper;
        private IConfiguration _configuration;

        public AuthenticateTeacherCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IAuthenticateUser authenticateUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _authenticateUser = authenticateUser;
        }

        public async Task<string> Handle(UserAuthenticatingDtoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var hashedPassword = await _authenticateUser.HashPassword(request.Password);
                var account = (await _unitOfWork.TeacherRepository.FindAsync(a => a.PhoneNumber == request.PhoneNumber && a.HashPassword == hashedPassword)).FirstOrDefault();
                if (account != null)
                {
                    string accessToken = await _authenticateUser.GenerateAccessToken(account.TeacherId, "Teacher");
                    return accessToken;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
