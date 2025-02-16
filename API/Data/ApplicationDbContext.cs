using API.Models.Entity;
using Azure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;


namespace API.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PropuestaUserEntity>()
                .HasKey(e => new { e.PropuestaId, e.UserId });

            modelBuilder.Entity<PropuestaEntity>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Propuestas)
                .UsingEntity<PropuestaUserEntity>(
                j => j
                    .HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .HasPrincipalKey(e => e.Id),
                j => j
                    .HasOne(e => e.Propuesta)
                    .WithMany()
                    .HasForeignKey(e => e.PropuestaId)
                    .HasPrincipalKey(e => e.Id));
        }

        //Add models here
        public DbSet<User> Users { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<PropuestaEntity> Propuesta { get; set; }
        public DbSet<PropuestaUserEntity> PropuestaUser { get; set; }
    }
}
