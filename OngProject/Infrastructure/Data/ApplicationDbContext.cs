using Microsoft.EntityFrameworkCore;
using OngProject.Core.Entities;

namespace OngProject.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //de esta manera la propiedad email de los usuarios sera unica y no se podra repetir
            builder.Entity<User>(entity => {
                entity.HasIndex(e => e.email).IsUnique();
            });
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Testimonials> Testimonials { get; set; }
        public DbSet<User> Users { get; set; }
        //Referencia a la Entidad : Slides
        public DbSet<Slides> Slides { get; set; }
        public DbSet<Organizations> Organizations { get; set; }
        public DbSet<News> News{ get; set; }

    }
}
