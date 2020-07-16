using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;



namespace Checkmate.Libs
{
    public class RequestService //: IAccountService
    {
        //public readonly UserManager<ApplicationUser> userManager;

        //public AccountService(UserManager<ApplicationUser> userManager)
        //{
        //    this.userManager = userManager;
        //}

        //public async Task CreateUser()
        //{
        //    var user = new ApplicationUser { UserName = "TestUser", Email = "test@example.com" };
        //    var result = await this.userManager.CreateAsync(user, "Test@123");

        //    if (result.Succeeded == false)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            Console.WriteLine(error);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Done.");
        //    }
        //}


        //private ApplicationUserManager _userManager;

        //public AccountService()
        //{ 
        //}
        //public AccountService(ApplicationUserManager userManager)
        //{
        //    _userManager = userManager;        
        //}

        //public  Task<IdentityResult> createUser(ApplicationUser user)
        //{
        //  return _userManager.CreateAsync(user);
        //}
    }
}
