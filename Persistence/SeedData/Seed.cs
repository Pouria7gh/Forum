using Domain.Forum;
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

        #region admin account

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

        #endregion

        #region dummy rooms

        var dummyRoom1 = new ForumRoom()
        {
            Id = Guid.NewGuid(),
            UpdatedAt = DateTime.UtcNow,
            Subtitle = "Sample",
            Title = "Sample Room 1",
            IsPinned = true,
            Rules = "No Rules",
            IsClosed = true,
            Description = "Sample",
            IsDisabled = false,
            CreatedAt = DateTime.UtcNow,
        };

        var dummyRoom2 = new ForumRoom()
        {
            Title = "Sample Room 2",
            Id = Guid.NewGuid(),
            UpdatedAt = DateTime.UtcNow,
            Subtitle = "Sample",
            IsPinned = false,
            Rules = "No Rules",
            IsClosed = false,
            Description = "Sample",
            IsDisabled = false,
            CreatedAt = DateTime.UtcNow,
        };

        var dummyRoom3 = new ForumRoom()
        {
            Title = "Sample Room 3",
            Id = Guid.NewGuid(),
            UpdatedAt = DateTime.UtcNow,
            Subtitle = "Sample",
            IsPinned = false,
            Rules = "No Rules",
            IsClosed = false,
            Description = "Sample",
            IsDisabled = false,
            CreatedAt = DateTime.UtcNow,
        };

        var dummyRoom4 = new ForumRoom()
        {
            Title = "Sample Room 4",
            Id = Guid.NewGuid(),
            UpdatedAt = DateTime.UtcNow,
            Subtitle = "Sample",
            IsPinned = false,
            Rules = "No Rules",
            IsClosed = false,
            Description = "Sample",
            IsDisabled = false,
            CreatedAt = DateTime.UtcNow,
        };

        var dummyRoom5 = new ForumRoom()
        {
            Title = "Sample Room 5",
            Id = Guid.NewGuid(),
            UpdatedAt = DateTime.UtcNow,
            Subtitle = "Sample",
            IsPinned = false,
            Rules = "No Rules",
            IsClosed = false,
            Description = "Sample",
            IsDisabled = false,
            CreatedAt = DateTime.UtcNow,
        };

        var dummyRoom6 = new ForumRoom()
        {
            Title = "Sample Room 6",
            Id = Guid.NewGuid(),
            UpdatedAt = DateTime.UtcNow,
            Subtitle = "Sample",
            IsPinned = false,
            Rules = "No Rules",
            IsClosed = false,
            Description = "Sample",
            IsDisabled = false,
            CreatedAt = DateTime.UtcNow,
        };

        var dummyRoom7 = new ForumRoom()
        {
            Title = "Sample Room 7",
            Id = Guid.NewGuid(),
            UpdatedAt = DateTime.UtcNow,
            Subtitle = "Sample",
            IsPinned = false,
            Rules = "No Rules",
            IsClosed = false,
            Description = "Sample",
            IsDisabled = false,
            CreatedAt = DateTime.UtcNow,
        };

        var dummyRoom8 = new ForumRoom()
        {
            Title = "Sample Room 8",
            Id = Guid.NewGuid(),
            UpdatedAt = DateTime.UtcNow,
            Subtitle = "Sample",
            IsPinned = false,
            Rules = "No Rules",
            IsClosed = false,
            Description = "Sample",
            IsDisabled = false,
            CreatedAt = DateTime.UtcNow,
        };

        var dummyRoom9 = new ForumRoom()
        {
            Title = "Sample Room 8",
            Id = Guid.NewGuid(),
            UpdatedAt = DateTime.UtcNow,
            Subtitle = "Sample",
            IsPinned = false,
            Rules = "No Rules",
            IsClosed = false,
            Description = "Sample",
            IsDisabled = false,
            CreatedAt = DateTime.UtcNow,
        };

        var dummyRoom10 = new ForumRoom()
        {
            Title = "Sample Room 10",
            Id = Guid.NewGuid(),
            UpdatedAt = DateTime.UtcNow,
            Subtitle = "Sample",
            IsPinned = false,
            Rules = "No Rules",
            IsClosed = false,
            Description = "Sample",
            IsDisabled = false,
            CreatedAt = DateTime.UtcNow,
        };

        var dummyRoom11 = new ForumRoom()
        {
            Title = "Sample Room 11",
            Id = Guid.NewGuid(),
            UpdatedAt = DateTime.UtcNow,
            Subtitle = "Sample",
            IsPinned = false,
            Rules = "No Rules",
            IsClosed = false,
            Description = "Sample",
            IsDisabled = false,
            CreatedAt = DateTime.UtcNow,
        };

        dataContext.ForumRooms.Add(dummyRoom1);
        dataContext.ForumRooms.Add(dummyRoom2);
        dataContext.ForumRooms.Add(dummyRoom3);
        dataContext.ForumRooms.Add(dummyRoom4);
        dataContext.ForumRooms.Add(dummyRoom5);
        dataContext.ForumRooms.Add(dummyRoom6);
        dataContext.ForumRooms.Add(dummyRoom7);
        dataContext.ForumRooms.Add(dummyRoom8);
        dataContext.ForumRooms.Add(dummyRoom9);
        dataContext.ForumRooms.Add(dummyRoom10);
        dataContext.ForumRooms.Add(dummyRoom11);

        #endregion

        #region dummy users

        var bob = new AppUser()
        {
            Id = Guid.NewGuid(),
            Username = "bob",
            DisplayName = "Bob",
            Email = "bob@test.com",
            EmailVerified = true,
            PasswordHashed = "AQAAAAIAAYagAAAAEIHp0Jn2+5kzWLr6ecDJQr+V5B6rmSjEqqqMrE2mALH/yCFIcEqst49hqbhApXuRRg=="
        };

        var tom = new AppUser()
        {
            Id = Guid.NewGuid(),
            Username = "tom",
            DisplayName = "Tom",
            Email = "tom@test.com",
            EmailVerified = true,
            PasswordHashed = "AQAAAAIAAYagAAAAEIHp0Jn2+5kzWLr6ecDJQr+V5B6rmSjEqqqMrE2mALH/yCFIcEqst49hqbhApXuRRg=="
        };

        var jane = new AppUser()
        {
            Id = Guid.NewGuid(),
            Username = "jane",
            DisplayName = "Jane",
            Email = "jane@test.com",
            EmailVerified = true,
            PasswordHashed = "AQAAAAIAAYagAAAAEIHp0Jn2+5kzWLr6ecDJQr+V5B6rmSjEqqqMrE2mALH/yCFIcEqst49hqbhApXuRRg=="
        };

        dataContext.Users.Add(bob);
        dataContext.Users.Add(tom);
        dataContext.Users.Add(jane);

        #endregion

        #region main room

        Guid mainRoomId = Guid.NewGuid();

        var mainForumRoom = new ForumRoom()
        {
            Id = mainRoomId,
            CreatedAt = DateTime.UtcNow,
            Description = "This is the main room of this website.",
            IsClosed = false,
            IsDisabled = false,
            IsPinned = true,
            Rules = "Be polite.",
            Title = "Main Room",
            Subtitle = "Contains sample posts",
            UpdatedAt = DateTime.UtcNow,
        };

        dataContext.ForumRooms.Add(mainForumRoom);

        #endregion

        #region main room main post

        Guid mainPostId = Guid.NewGuid();

        ForumPost mainPost = new()
        {
            Id = mainPostId,
            CreatedAt = DateTime.UtcNow,
            Description = "Thanks",
            ForumRoomId = mainRoomId,
            IsDisabled = false,
            PostContent = "Does anyone know how to work with Entity Framework? \nI can't set it up, It's my first time.",
            UpdatedAt = DateTime.UtcNow,
            UserId = bob.Id,
            Replies = [
                new() {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    PostContent = "Sorry man, I can't help you :(",
                    UpdatedAt = DateTime.UtcNow,
                    UserId = tom.Id,
                    ForumRoomId = mainRoomId,
                    IsDisabled = false,
                    ParentPostId = mainPostId,
                    Description = string.Empty,
                },
                new() {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    PostContent = "It's okey.",
                    UpdatedAt = DateTime.UtcNow,
                    UserId = bob.Id,
                    ForumRoomId = mainRoomId,
                    IsDisabled = false,
                    ParentPostId = mainPostId,
                    Description = string.Empty,
                },
                new() {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    PostContent = "have you seen official microsoft documentation ?",
                    UpdatedAt = DateTime.UtcNow,
                    UserId = jane.Id,
                    ForumRoomId = mainRoomId,
                    IsDisabled = false,
                    ParentPostId = mainPostId,
                    Description = string.Empty,
                },
                new() {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    PostContent = "No, I haven't. \nLet mee see...",
                    UpdatedAt = DateTime.UtcNow,
                    UserId = bob.Id,
                    ForumRoomId = mainRoomId,
                    IsDisabled = false,
                    ParentPostId = mainPostId,
                    Description = string.Empty,
                },
                new() {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    PostContent = "Update:\nI can't undrestand this nonesence.",
                    UpdatedAt = DateTime.UtcNow,
                    UserId = bob.Id,
                    ForumRoomId = mainRoomId,
                    IsDisabled = false,
                    ParentPostId = mainPostId,
                    Description = string.Empty,
                },
                new() {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    PostContent = "Have you just started programing?",
                    UpdatedAt = DateTime.UtcNow,
                    UserId = tom.Id,
                    ForumRoomId = mainRoomId,
                    IsDisabled = false,
                    ParentPostId = mainPostId,
                    Description = string.Empty,
                },
                new() {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    PostContent = "Yes, and i'm thinking of quiting!\nI have this project for my uni, It's a contact book.",
                    UpdatedAt = DateTime.UtcNow,
                    UserId = bob.Id,
                    ForumRoomId = mainRoomId,
                    IsDisabled = false,
                    ParentPostId = mainPostId,
                    Description = string.Empty,
                },
                new() {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    PostContent = "That sounds like my story!!! Don't give up, You can do it.",
                    UpdatedAt = DateTime.UtcNow,
                    UserId = tom.Id,
                    ForumRoomId = mainRoomId,
                    IsDisabled = false,
                    ParentPostId = mainPostId,
                    Description = string.Empty,
                },
                new() {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    PostContent = "I'm so happy that you started your journey.",
                    UpdatedAt = DateTime.UtcNow,
                    UserId = jane.Id,
                    ForumRoomId = mainRoomId,
                    IsDisabled = false,
                    ParentPostId = mainPostId,
                    Description = string.Empty,
                },
                new() {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    PostContent = "Thanks guys <3",
                    UpdatedAt = DateTime.UtcNow,
                    UserId = tom.Id,
                    ForumRoomId = mainRoomId,
                    IsDisabled = false,
                    ParentPostId = mainPostId,
                    Description = string.Empty,
                },
                new() {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    PostContent = "Second page Hope you liked the story :)",
                    UpdatedAt = DateTime.UtcNow,
                    UserId = admin.Id,
                    ForumRoomId = mainRoomId,
                    IsDisabled = false,
                    ParentPostId = mainPostId,
                    Description = string.Empty,
                }
            ],
        };

        dataContext.ForumPosts.Add(mainPost);

        #endregion

        await dataContext.SaveChangesAsync();
    }
}