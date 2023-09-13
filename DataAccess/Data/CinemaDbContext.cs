﻿using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using DataAccess.EntiitesConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Data
{
    public class CinemaDbContext:DbContext
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
            modelBuilder.SeedMovies();
            
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


        }
        public DbSet<Movie> Movies { get; set; }
   
    }
}
