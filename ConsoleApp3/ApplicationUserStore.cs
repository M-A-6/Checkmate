using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()  : base(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, throwIfV1Schema: false)
        {
        }



      

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>().ToTable("User");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            //modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            
            
        }

    }

    public class ApplicationUserStore : UserStore<ApplicationUser>
    {
        public ApplicationUserStore(ApplicationDbContext context) : base(context)
        {
        }

    }

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }

    }

    public class ApplicationUser : IdentityUser
    {
        public  string Email { get; set; }

        public  bool EmailConfirmed { get; set; }

        public  string Id { get; set; }

        public  bool LockoutEnabled { get; set; }

        public  DateTime? LockoutEndDateUtc { get; set; }

        public  string PasswordHash { get; set; }

        public  string PhoneNumber { get; set; }

        public  bool PhoneNumberConfirmed { get; set; }

   
        public  string SecurityStamp { get; set; }

        public  bool TwoFactorEnabled { get; set; }

        public  string UserName { get; set; }
        //You can extend this class by adding additional fields like Birthday
    }
}
