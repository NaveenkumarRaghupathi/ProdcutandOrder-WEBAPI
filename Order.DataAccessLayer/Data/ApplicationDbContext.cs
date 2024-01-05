using Microsoft.EntityFrameworkCore;
using Order.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Order.DataAccessLayer.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>().Property(e => e.Price).HasPrecision(18, 2);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Orders>().Property(e => e.TotalAmount).HasPrecision(18, 2);
            base.OnModelCreating(modelBuilder);
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
           
        }
        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
    }
}
