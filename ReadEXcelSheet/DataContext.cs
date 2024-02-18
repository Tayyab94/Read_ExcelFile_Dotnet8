using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadEXcelSheet
{
    public class DataContext :DbContext
    {
        public DbSet<User> Users { get; set; } // DbSet representing your User table

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=TAYYAB;Database=testingDb; user id=admintest; Password=123;;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property<int>("Id"); // Shadow property as primary key

            modelBuilder.Entity<User>().HasKey("Id"); // Define the shadow property as the primary key
        }
    }
}
