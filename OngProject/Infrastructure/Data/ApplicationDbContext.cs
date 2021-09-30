using Microsoft.EntityFrameworkCore;
using OngProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Threading.Tasks;

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
        //referencia a la entidad : User
        public DbSet<User> Users { get; set; }
        //Referencia a la Entidad : Slides
        public DbSet<Slides> Slides { get; set; }

    }
}
