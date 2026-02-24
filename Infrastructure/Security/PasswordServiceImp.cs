using Application.Interfaces;
using Domain.User;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Security
{
    internal class PasswordServiceImp : PasswordService
    {
        private readonly PasswordHasher<AppUser> _passwordHasher;

        public PasswordServiceImp()
        {
            _passwordHasher = new();
        }

        public string HashPassword(AppUser user, string password)
        {
            string hashed = _passwordHasher.HashPassword(user, password);

            return hashed;
        }

        public bool VerifyPassword(AppUser user, string password, string passwordHashed)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, password, passwordHashed);

            bool success = result == PasswordVerificationResult.Success ||
                result == PasswordVerificationResult.SuccessRehashNeeded;

            return success;
        }
    }
}
