using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FoodInfo.Service.Models
{
    public class FoodInfoServiceContext : DbContext
    {
        public FoodInfoServiceContext()
        {
        }

        public FoodInfoServiceContext(DbContextOptions<FoodInfoServiceContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<NutritionFacts> NutritionFacts { get; set; }
        public DbSet<ProductContent> ProductContents { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=13.59.157.144; Database=FoodInfoService; User Id=fiservice;Password=foodinfoservice;");
             // optionsBuilder.UseSqlServer(@"Server=localhost; Database=FoodInforService; Integrated Security=True;");


            }

        }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(new User { ID = 1, Name = "Fatih", Surname = "Cankurtaran" ,Email="f@gmail.com", Username= "fatih",Password ="123"},
                        new User { ID = 2, Name = "Yusuf", Surname = "Kocadas" , Email ="y@gmail.com",Username="yusuf", Password = "123" }
                );

            /// This one will be used for setting properties.
            //modelBuilder.Entity<User>()
            //    .Property(b => b.Name)
            //    .IsRequired(true);
            


            modelBuilder.Entity<ProductCategory>()
                .HasMany(p => p.Products);

            
            
        }

    }
}