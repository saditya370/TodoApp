namespace TodoApp.Business.IServices
{
    public interface IJwtService
    {
        string GenerateToken(int userId, string username);
    }
}