using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace StudentManagement.Infrastructure.Persistence.Repositories
{
    public class AutheticateUserService : IAuthenticateUser
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;

        public AutheticateUserService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<string> GenerateAccessToken(Guid userId, string role)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var accessClaims = new List<Claim>
                {
                    new Claim("userId", userId.ToString()),
                    new Claim("role", role)
                };
                var accessExpiration = DateTime.Now.AddMinutes(30);
                var accessJwt = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], accessClaims, expires: accessExpiration, signingCredentials: credentials);
                var accessToken = new JwtSecurityTokenHandler().WriteToken(accessJwt);
                return accessToken;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> HashPassword(string password)
        {
            try
            {
                using (SHA512 sha512 = SHA512.Create())
                {
                    byte[] hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));

                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        stringBuilder.Append(hashBytes[i].ToString("x2"));
                    }

                    return await Task.FromResult(stringBuilder.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> TaoMatKhauSo(int doDai)
        {
            const string chuSo = "0123456789";

            // Random.Shared là một thực thể (instance) của lớp Random
            // được chia sẻ, an toàn cho luồng (thread-safe) trong .NET 6+
            return string.Concat(Enumerable.Range(0, doDai)
                                           .Select(_ => chuSo[Random.Shared.Next(chuSo.Length)]));
        }
    }
}
