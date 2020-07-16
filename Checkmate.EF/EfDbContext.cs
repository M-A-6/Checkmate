using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkmate.EF
{
    public class EfDbContext : DbContext
    {
        public EfDbContext() : base(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
        { 
        
        }
        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("User");
        }

    }
}
