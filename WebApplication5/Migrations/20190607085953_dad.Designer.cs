﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication5.Models;

namespace WebApplication5.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20190607085953_dad")]
    partial class dad
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplication5.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorName");

                    b.Property<string>("BookName");

                    b.Property<int>("CategoryId");

                    b.Property<int>("Qty");

                    b.HasKey("BookId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("WebApplication5.Models.BookRent", b =>
                {
                    b.Property<int>("BookRentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookId");

                    b.Property<int>("Qty");

                    b.Property<string>("RentDate");

                    b.Property<int>("StudentId");

                    b.HasKey("BookRentId");

                    b.HasIndex("BookId");

                    b.HasIndex("StudentId");

                    b.ToTable("BookRent");
                });

            modelBuilder.Entity("WebApplication5.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName");

                    b.Property<string>("Description");

                    b.HasKey("CategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("WebApplication5.Models.ReturnBook", b =>
                {
                    b.Property<int>("ReturnBookId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookRentId");

                    b.Property<string>("ReturnDate");

                    b.Property<int>("ReturnQty");

                    b.HasKey("ReturnBookId");

                    b.HasIndex("BookRentId")
                        .IsUnique();

                    b.ToTable("ReturnBook");
                });

            modelBuilder.Entity("WebApplication5.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Grade");

                    b.Property<string>("NRC");

                    b.Property<string>("RollNo");

                    b.Property<string>("StudentName");

                    b.HasKey("StudentId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("WebApplication5.Models.Book", b =>
                {
                    b.HasOne("WebApplication5.Models.Category", "Category")
                        .WithMany("Book")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication5.Models.BookRent", b =>
                {
                    b.HasOne("WebApplication5.Models.Book", "Book")
                        .WithMany("BookRent")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication5.Models.Student", "Student")
                        .WithMany("BookRent")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication5.Models.ReturnBook", b =>
                {
                    b.HasOne("WebApplication5.Models.BookRent", "BookRent")
                        .WithOne("ReturnBook")
                        .HasForeignKey("WebApplication5.Models.ReturnBook", "BookRentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
