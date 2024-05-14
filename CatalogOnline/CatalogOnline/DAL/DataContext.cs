using CatalogOnline.DAL.DBO;
using Microsoft.EntityFrameworkCore;
using System;

namespace CatalogOnline.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<Notification> Notification { get; set; }

        public DbSet<Document> Document { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
        }
    }
}
