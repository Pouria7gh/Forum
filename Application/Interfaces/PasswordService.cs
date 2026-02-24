using Domain.User;

namespace Application.Interfaces
{
    public interface PasswordService
    {
        string HashPassword(AppUser user, string password);

        bool VerifyPassword(AppUser user, string password, string passwordHashed);
    }
}
