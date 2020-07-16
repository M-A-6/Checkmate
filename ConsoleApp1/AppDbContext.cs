using ConsoleApp1.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(): base(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
        { 
        
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Request>().ToTable("Requests");
        }



        public DbSet<Request> Requests { get; set; }
    }

    public class AppDbContext2 : IdentityDbContext
    {
        public AppDbContext2() : base(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           // modelBuilder.Entity<Request>().ToTable("Requests");
        }



        public DbSet<Request> Requests { get; set; }
    }
}
