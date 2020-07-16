using Checkmate.Libs;
using ConsoleApp1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDbContext2 _ctxf = new AppDbContext2();
            Request request1 = new Request();
            request1.BookletStyleName = "test";
            var req1 = _ctxf.Requests.Find(50);


            IdentityUser user = new IdentityUser();
            user.Email = "email1@emaill.com";
            user.UserName = "email1@emaill.com";
            user.PasswordHash = "AQAAAAEAACcQAAAAEIf4Q3TndPHzIvXHbsQDOink6GbjTDpFzLphQmGGE8r8uU1x3W5jTz/3sZvVE5LMWw==";

            var userStore = new UserStore<IdentityUser>();
            IdentityResult result = new UserManager<IdentityUser>(userStore).Create(user);
             
            if (result.Succeeded)
            {

            }

            AppDbContext db = new AppDbContext();
            Request request = new Request();
            request.BookletStyleName = "test";
             var req=  db.Requests.Find(50);
           // db.SaveChanges();


            Request updateReq = db.Requests.Find(60);
            updateReq.BookletStyleName = "test";
            updateReq.BookletSize = 3;
            updateReq.BookletAccountNumber = "034";

            db.Requests.AddOrUpdate(updateReq);
            db.SaveChanges();


            

        }
    }
}
