using Domain.Forum;
using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Providers;

public class DataContext : DbContext
{

    public DataContext(DbContextOptions options) : base(options)
    {
     
    }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<ForumRoom> ForumRooms { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        #region AppUser
        builder.Entity<AppUser>(user =>
        {
            user.HasKey(x => x.Id);

            user.HasIndex(x => x.Username)
                .IsUnique();

            user.Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(30);

            user.HasIndex(x => x.Email)
                .IsUnique();

            user.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(254);

            user.Property(x => x.DisplayName)
                .IsRequired()
                .HasMaxLength(30);

            user.Property(x => x.PasswordHashed)
                .IsRequired();
        });
        #endregion

        #region Role
        builder.Entity<Role>(role =>
        {
            role.HasKey(r => r.Id);

            role.Property(r => r.Name)
                .IsRequired();
        });
        #endregion

        #region UserRole join table
        builder.Entity<UserRole>(entity =>
        {
            entity.HasKey(x => new { x.UserId, x.RoleId });

            entity.HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId);

            entity.HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId);
        });
        #endregion

        #region ForumRoom
        builder.Entity<ForumRoom>(entity =>
        {
            entity.HasKey(f => f.Id);

            entity.Property(f => f.Title)
                .HasMaxLength(256)
                .IsRequired();

            entity.Property(f => f.Subtitle)
                .HasMaxLength(256);

            entity.Property(f => f.Rules)
                .HasMaxLength(2048);

            entity.Property(f => f.Description)
                .HasMaxLength(2048);

            entity.Property(f => f.IsDisabled)
                .HasDefaultValue(false);
        });
        #endregion
    }
}
