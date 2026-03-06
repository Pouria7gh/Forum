namespace Application.DTOs.Account
{
    public class LoginDto
    {
        public Guid UserId { get; set; }
        public List<string>? Roles { get; set; }
    }
}
