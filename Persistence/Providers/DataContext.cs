using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Providers;

public class DataContext : DbContext
{

    public DataContext(DbContextOptions options) : base(options)
    {
     
    }

    public DbSet<AppUser> Users { get; set; }

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
    }
}
