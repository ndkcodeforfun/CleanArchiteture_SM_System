namespace StudentManagement.Application.Interfaces
{
    public interface IAuthenticateUser
    {
        Task<string> HashPassword(string password);
        Task<string> GenerateAccessToken(Guid userId, string role);
        Task<string> TaoMatKhauSo(int doDai);
    }
}
