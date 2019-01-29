using AspNetCoreAuthCookie.Models;
using AspNetCoreAuthCookie.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAuthCookie.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(u =>
            {
                u.HasData(new User { UserId = 1, UserName = "Admin", Password = Hash.GenerateHash("12345678"), UserTypes = UserType.Admin, ActiveUser = true});
                u.HasData(new User { UserId = 2, UserName = "Manager", Password = Hash.GenerateHash("12345678"), UserTypes = UserType.Manager, ActiveUser = true});
                u.HasData(new User { UserId = 3, UserName = "Users", Password = Hash.GenerateHash("12345678"), UserTypes = UserType.Users, ActiveUser = true});
            });
        }

        public DbSet<User> Users { get; set; }
    }
}
