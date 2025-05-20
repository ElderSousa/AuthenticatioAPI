using Domain.MS_AuthorizationAutentication.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MS_AuthenticationAutorization.Data;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) {}

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<User>(entity => 
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .IsRequired();

            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(e => e.Active)
                .IsRequired();

            entity.Property(e => e.CreatedOn)
                .IsRequired()
                .HasDefaultValue(null);

            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasDefaultValue(null);

            entity.Property(e => e.ModifiedOn)
                .HasDefaultValue(null);

            entity.Property(e => e.ModifiedBy)
                .HasDefaultValue(null);

            entity.Property(e => e.DeletedOn)
                .HasDefaultValue(null);
        });

        modelBuilder.Entity<Role>(entity => {

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .IsRequired();

            entity.Property(e => e.Name)
                .HasMaxLength(250);

            entity.Property(e => e.Description)
                .HasMaxLength(250);

            entity.Property(e => e.CreatedOn)
                .IsRequired();

            entity.Property(e => e.CreatedBy)
                .IsRequired();

            entity.Property(e => e.ModifiedOn)
                .HasDefaultValue(null);

            entity.Property(e => e.ModifiedBy)
                .HasDefaultValue(null);

            entity.Property(e => e.DeletedOn)
                .HasDefaultValue(null);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(ur => new { ur.UserId, ur.RoleId });

            entity.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);
            
            entity.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            entity.Property(e => e.ModifiedOn)
                .HasDefaultValue(null);

            entity.Property(e => e.ModifiedBy)
                .HasDefaultValue(null);

            entity.Property(e => e.DeletedOn)
                .HasDefaultValue(null);
        });
               
    }
}
