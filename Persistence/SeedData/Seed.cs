using Domain.User;
using Persistence.Providers;

namespace Persistence.SeedData;

public class Seed
{
    public static async Task AddSeedData(DataContext dataContext)
    {
        if (dataContext.Users.Any())
        {
            return;
        }

        var admin = new AppUser()
        {
            Id = Guid.NewGuid(),
            Username = "admin",
            DisplayName = "Admin",
            Email = "admin@test.com",
            EmailVerified = true,
            PasswordHashed = "AQAAAAIAAYagAAAAEIHp0Jn2+5kzWLr6ecDJQr+V5B6rmSjEqqqMrE2mALH/yCFIcEqst49hqbhApXuRRg=="
        };

        var role = new Role()
        {
            Id = Guid.NewGuid(),
            Name = "Admin",
        };

        var userRole = new UserRole()
        {
            RoleId = role.Id,
            UserId = admin.Id
        };

        dataContext.Users.Add(admin);
        dataContext.Roles.Add(role);
        dataContext.UserRoles.Add(userRole);

        await dataContext.SaveChangesAsync();
    }
}