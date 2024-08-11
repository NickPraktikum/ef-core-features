﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Interceptors.Migrations
{
    [DbContext(typeof(InterceptorDbContext))]
    partial class InterceptorDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Interceptors.Entities.AuthorEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("BirthDate")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("BirthDate")
                        .HasColumnOrder(12);

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("DeletedAt")
                        .HasColumnOrder(30);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("FirstName")
                        .HasColumnOrder(10);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted")
                        .HasColumnOrder(13);

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("SecondName")
                        .HasColumnOrder(11);

                    b.HasKey("Id");

                    b.HasIndex(new[] { "FirstName" }, "IX_AuthorEntity_FirstName");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Interceptors.Entities.BookEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint")
                        .HasColumnName("AuthorId")
                        .HasColumnOrder(2);

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("DeletedAt")
                        .HasColumnOrder(30);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted")
                        .HasColumnOrder(14);

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Isbn")
                        .HasColumnOrder(10);

                    b.Property<int>("Pages")
                        .HasColumnType("int")
                        .HasColumnName("Pages")
                        .HasColumnOrder(12);

                    b.Property<float>("Price")
                        .HasColumnType("real")
                        .HasColumnName("Price")
                        .HasColumnOrder(13);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Title")
                        .HasColumnOrder(11);

                    b.Property<int>("Version")
                        .HasColumnType("int")
                        .HasColumnName("Version")
                        .HasColumnOrder(15);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex(new[] { "Title" }, "IX_BookEntitys_Title");

                    b.HasIndex(new[] { "Isbn" }, "UX_BookEntitys_Isbn")
                        .IsUnique();

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Interceptors.Entities.BookEntity", b =>
                {
                    b.HasOne("Interceptors.Entities.AuthorEntity", null)
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Interceptors.Entities.AuthorEntity", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
