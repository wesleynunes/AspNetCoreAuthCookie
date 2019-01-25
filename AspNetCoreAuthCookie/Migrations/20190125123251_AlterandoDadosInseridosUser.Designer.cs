﻿// <auto-generated />
using AspNetCoreAuthCookie.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AspNetCoreAuthCookie.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190125123251_AlterandoDadosInseridosUser")]
    partial class AlterandoDadosInseridosUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AspNetCoreAuthCookie.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ActiveUser");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<bool>("RememberMe");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("UserTypes");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new { UserId = 1, ActiveUser = true, Password = "8BB0CF6EB9B17DF7D22B456F121257DC1254E1F1665370476383EA776DF414", RememberMe = true, UserName = "Admin", UserTypes = 0 }
                    );
                });
#pragma warning restore 612, 618
        }
    }
}
