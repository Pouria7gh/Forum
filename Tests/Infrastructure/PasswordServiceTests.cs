using Domain.User;
using Infrastructure.Security;

namespace Tests.Infrastructure
{
    public class PasswordServiceTests
    {
        [Theory]
        [InlineData("myStrong.Passw0rd")]
        [InlineData("Pouria.123.ASd")]
        [InlineData("!@#KL:")]
        public void HashAndVerifyPassword(string password)
        {
            AppUser user = new()
            {
                Id = Guid.NewGuid(),
                Username = "pouria7gh",
                Email = "pouria@gmail.com",
                DisplayName = "pouria",
                PasswordHashed = string.Empty
            };

            PasswordServiceImp passService = new();

            string hashed = passService.HashPassword(user, password);

            bool result = passService.VerifyPassword(user, password, hashed);

            Assert.True(result);
        }
    }
}
