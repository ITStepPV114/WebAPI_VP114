using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EntiitesConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data
{
    public class CinemaDbContext:IdentityDbContext
    {

        //public ShopMVCDbContext()
        //{
        //    //Database.EnsureDeleted();
        //    //Database.EnsureCreated();
        //}
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { 
        //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ShopMVC;Integrated Security=True;"); 
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.SeedGenres();
            modelBuilder.SeedMovies();
            modelBuilder.SeedMovieGenre();
            
            #region Fluent API => Configurations
            ////Set Primary Key
            //modelBuilder.Entity<Product>().HasKey(x => x.Id);

            ////Set Property configurations
            //modelBuilder.Entity<Product>()
            //            .Property(x => x.Name)
            //            .HasMaxLength(150)
            //            .IsRequired();

            ////Set Relationship configurations
            //modelBuilder.Entity<Product>()
            //    .HasOne<Category>(x => x.Category)
            //    .WithMany(x => x.Products)
            //    .HasForeignKey(x => x.CategoryId);
            #endregion

            // ApplyConfigurations for Entities 
            // modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopMVCDbContext).Assembly);
            // or 
            modelBuilder.ApplyConfiguration(new MovieConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new MovieGenreConfiguration());


        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }


    }
}
