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
            modelBuilder.Entity<PropuestaEntity>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Propuestas);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Cursos)
                .WithMany(e => e.Users);
        }

        //Add models here
        public DbSet<User> Users { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<PropuestaEntity> Propuesta { get; set; }
        public DbSet<CursoEntity> Curso { get; set; }
    }
}
