namespace Application.DTOs.Account
{
    public class LoginDto
    {
        public string Username { get; set; } = string.Empty;
        public List<string>? Roles { get; set; }
    }
}
