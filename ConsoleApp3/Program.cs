using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {

            RegisterViewModel model = new RegisterViewModel();
            model.Email = "test12345678@example.com";
            model.Password = "P@ss1234";
            model.ConfirmPassword = "P@ss1234";

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PasswordH1= EncodeStr("P@ss1234") };

            //using (ApplicationDbContext ctx = new ApplicationDbContext())
            //{
            UserStore<ApplicationUser> Store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            ApplicationUserManager userManager = new ApplicationUserManager(Store);



            //var item = userManager.Find("administrator@example.com", "P@ss1234");

            //var item3 = userManager.Find(model.Email, "P@ss1234");

            //var item4 = userManager.Find("administrator@example.com", "P@ss1234");

            //var item2 = userManager.Find(model.Email, model.Password + "test");

            //if (item != null)
            //{
            //    Console.WriteLine("valid  = " + item.Email);
            //}
            //else
            //{
            //    Console.WriteLine("item 1 is invalid");
            //}


            //if (item2 != null)
            //{
            //    Console.WriteLine("valid  = " + item2.Email);
            //}
            //else
            //{
            //    Console.WriteLine("item 2 is invalid");
            //}
            var result = userManager.CreateAsync(user, model.Password).Result;

            //if (result.Succeeded)
            //{
            //var pas = userManager.CheckPasswordAsync(user, model.Password).Result;
            //Console.WriteLine("pass result = " + pas);
            //var pasfail = userManager.CheckPasswordAsync(user, model.Password + "12").Result;
            //Console.WriteLine("fail result = " + pasfail);
            ////}
            //Request req1 = new Request();
            //req1.BookletAccountName = "test666";

            //  ctx.Requests.Add(req1);
            //  ctx.SaveChanges();
            //}



            //ApplicationUser user;
            //ApplicationUserStore Store = new ApplicationUserStore(new ApplicationDbContext());
            //ApplicationUserManager userManager = new ApplicationUserManager(Store);



            //var item = userManager.FindByEmailAsync("administrator@example.com").Result;
            //var result = userManager.CreateAsync(user, model.Password).Result;

            //var result =  userManager.CreateAsync(user).Result;


            //var usr= userManager.FindByEmailAsync("administrator@example.com").Result;
            //userManager.CheckPasswordAsync(usr, "P@ss1234");

            //var result1= userManager.CheckPasswordAsync(user, "P@ss1234").Result;

            Console.WriteLine("testtt");

        }

        public static string EncodeStr(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
    public class Request
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string PrinterBranchID { get; set; }
        public string PrinterClientID { get; set; }
        public string RequestID { get; set; }
        public string OperatorName { get; set; }
        public int RequestType { get; set; }
        public string BookletAccountNumber { get; set; }
        public string BookletStyleName { get; set; }
        public int BookletSize { get; set; }
        public string BookletAccountName { get; set; }
    }
}
