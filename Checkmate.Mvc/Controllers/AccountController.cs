using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkmate.Model;
using Checkmate.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Checkmate.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
             
        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await userManager.CreateAsync(user, model.Password);
               
                if (result.Succeeded)
                {

                    if (!User.Identity.IsAuthenticated)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
        #endregion
        
        #region Login
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


    
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
              
                var result =  signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false).Result;

                if (result.Succeeded)
                {                    
                    return RedirectToAction("About");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use.");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword()
        {           
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var token =  userManager.GeneratePasswordResetTokenAsync(user);
                    if (token.Result != null)
                    {
                        var result = await userManager.ResetPasswordAsync(user, token.Result, model.Password);
                        if (result.Succeeded)
                        {
                            return View("ResetPasswordConfirmation");
                        }
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(model);
                    }
                    else
                    {
                        ModelState.AddModelError("", "token generation failed");
                    }
                }

                return View("ResetPasswordConfirmation");
            }
            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation() 
        {
            return View();
        }

        
        #endregion

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            return View();
        }
    }
}