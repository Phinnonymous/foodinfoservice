﻿// <auto-generated />
using System;
using FoodInfo.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoodInfo.Service.Migrations
{
    [DbContext(typeof(FoodInfoServiceContext))]
    [Migration("20190111153254_NutritionFact_brcode_language")]
    partial class NutritionFact_brcode_language
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FoodInfo.Service.Models.Comment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("CreatedUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int?>("ModifiedUserId");

                    b.Property<int?>("ProductContentID");

                    b.Property<string>("UserComment");

                    b.HasKey("ID");

                    b.HasIndex("ProductContentID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("FoodInfo.Service.Models.Error", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("CreatedUserId");

                    b.Property<string>("ErrorCode");

                    b.Property<string>("ErrorMessage");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int?>("ModifiedUserId");

                    b.HasKey("ID");

                    b.ToTable("Errors");
                });

            modelBuilder.Entity("FoodInfo.Service.Models.Language", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("CreatedUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LanguageCode");

                    b.Property<string>("LanguageName");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int?>("ModifiedUserId");

                    b.Property<string>("NativeLanguageName");

                    b.HasKey("ID");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("FoodInfo.Service.Models.NutritionFacts", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BarcodeId");

                    b.Property<decimal?>("Carbohydrate");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("CreatedUserId");

                    b.Property<decimal?>("Energy");

                    b.Property<decimal?>("Fat");

                    b.Property<decimal?>("Fiber");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LanguageCode");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int?>("ModifiedUserId");

                    b.Property<decimal?>("Protein");

                    b.Property<decimal?>("Salt");

                    b.Property<decimal?>("SaturatedFattyAcids");

                    b.Property<decimal?>("TransFattyAcids");

                    b.HasKey("ID");

                    b.ToTable("NutritionFacts");
                });

            modelBuilder.Entity("FoodInfo.Service.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BarcodeId")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("CreatedUserId");

                    b.Property<byte[]>("FirstImage");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int?>("ModifiedUserId");

                    b.Property<int?>("ProductCategoryID");

                    b.Property<int?>("ProductGroupId");

                    b.Property<string>("ProductName");

                    b.Property<byte[]>("SecondImage");

                    b.Property<byte[]>("ThirdImage");

                    b.HasKey("ID");

                    b.HasIndex("ProductCategoryID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("FoodInfo.Service.Models.ProductCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("CreatedUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LanguageCode");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int?>("ModifiedUserId");

                    b.HasKey("ID");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("FoodInfo.Service.Models.ProductContent", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CookingTips");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("CreatedUserId");

                    b.Property<string>("Details");

                    b.Property<string>("Ingredients");

                    b.Property<bool>("IsDeleted");

                    b.Property<int?>("LanguageID");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int?>("ModifiedUserId");

                    b.Property<int?>("NutritionFactID");

                    b.Property<int?>("ProductID");

                    b.Property<string>("Recommendations");

                    b.Property<string>("VideoURL");

                    b.Property<string>("Warnings");

                    b.HasKey("ID");

                    b.HasIndex("LanguageID");

                    b.HasIndex("NutritionFactID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductContents");
                });

            modelBuilder.Entity("FoodInfo.Service.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("CreatedUserId");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsModerator");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int?>("ModifiedUserId");

                    b.Property<string>("Name");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Surname");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("User");

                    b.HasData(
                        new { ID = 1, CreatedDate = new DateTime(2019, 1, 11, 16, 32, 54, 41, DateTimeKind.Local), Email = "f@gmail.com", IsAdmin = false, IsDeleted = false, IsModerator = false, Name = "Fatih", Password = "b41af4c157c87c6c8278ec45127c235fb5c991288e6a07da88b87549076acf02", Surname = "Cankurtaran", Username = "fatih" },
                        new { ID = 2, CreatedDate = new DateTime(2019, 1, 11, 16, 32, 54, 44, DateTimeKind.Local), Email = "y@gmail.com", IsAdmin = false, IsDeleted = false, IsModerator = false, Name = "Yusuf", Password = "b41af4c157c87c6c8278ec45127c235fb5c991288e6a07da88b87549076acf02", Surname = "Kocadas", Username = "yusuf" }
                    );
                });

            modelBuilder.Entity("FoodInfo.Service.Models.Vote", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("CreatedUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int?>("ModifiedUserId");

                    b.Property<int?>("ProductID");

                    b.Property<int?>("UserVote");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("FoodInfo.Service.Models.Comment", b =>
                {
                    b.HasOne("FoodInfo.Service.Models.ProductContent", "ProductContent")
                        .WithMany()
                        .HasForeignKey("ProductContentID");
                });

            modelBuilder.Entity("FoodInfo.Service.Models.Product", b =>
                {
                    b.HasOne("FoodInfo.Service.Models.ProductCategory", "ProductCategory")
                        .WithMany()
                        .HasForeignKey("ProductCategoryID");
                });

            modelBuilder.Entity("FoodInfo.Service.Models.ProductContent", b =>
                {
                    b.HasOne("FoodInfo.Service.Models.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageID");

                    b.HasOne("FoodInfo.Service.Models.NutritionFacts", "NutritionFact")
                        .WithMany()
                        .HasForeignKey("NutritionFactID");

                    b.HasOne("FoodInfo.Service.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");
                });

            modelBuilder.Entity("FoodInfo.Service.Models.Vote", b =>
                {
                    b.HasOne("FoodInfo.Service.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");
                });
#pragma warning restore 612, 618
        }
    }
}
