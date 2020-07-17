using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Checkmate.AppCreator;
using Checkmate.Business;
using Checkmate.Data;
using Checkmate.Model;
using Checkmate.Mvc.Resources;
using Checkmate.Mvc.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;

namespace Checkmate.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
              services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton<CultureLocalizer>();
            services.ConfigureRequestLocalization();
            services.AddTransient<IPasswordHasher<IdentityUser>, CustomPasswordHasher>();




            services.AddDbContextPool<AppDataDb>(options =>
                                            options.UseSqlServer(Configuration.GetConnectionString("ConnectionStringDB")));
            //services.AddControllersWithViews().AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = new CustomJsonNamingPolicy());

       

        services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                //.AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = new CustomJsonNamingPolicy())
                .AddViewLocalization(o => o.ResourcesPath = "Resources")
                .AddModelBindingMessagesLocalizer(services)


                // Option A: use this for localization with shared resource
                .AddDataAnnotationsLocalization(o => {
                    var type = typeof(ViewResource);
                    var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
                    var factory = services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
                    var localizer = factory.Create("ViewResource", assemblyName.Name);
                    o.DataAnnotationLocalizerProvider = (t, f) => localizer;
                })

                // Option B: use this for localization by view specific resource
                //.AddDataAnnotationsLocalization() 
                .AddRazorPagesOptions(o => {
                    o.Conventions.Add(new CultureTemplateRouteModelConvention());
                });

            
            services.AddTransient<IRequestService, RequestService>();
            //public int RequiredLength { get; set; } = 6;
            //public int RequiredUniqueChars { get; set; } = 1;
            //public bool RequireNonAlphanumeric { get; set; } = true;
            //public bool RequireLowercase { get; set; } = true;
            //public bool RequireUppercase { get; set; } = true;
            //public bool RequireDigit { get; set; } = true;
            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequiredLength = 10;
            //    options.Password.RequiredUniqueChars = 3;
            //    options.Password.RequireNonAlphanumeric = false;
            //});
            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<AppDataDb>().AddDefaultTokenProviders();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //app.UseStaticFiles();

           app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();






            app.UseRequestLocalization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });



        }
    }
    public class CustomJsonNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            return name.ToLower();
        }
    }
    
    public class CustomPasswordHasher : IPasswordHasher<IdentityUser>
    {
        public string HashPassword(IdentityUser user, string password)
        {
            return CreateHash(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(IdentityUser user, string hashedPassword, string providedPassword)
        {
            if (hashedPassword == CreateHash(providedPassword))
            {
                return PasswordVerificationResult.Success;
            }

            return PasswordVerificationResult.Failed;
        }

        private string CreateHash(string password)
        {
            MD5CryptoServiceProvider md5Crypt = new MD5CryptoServiceProvider();
            byte[] passBytes = Encoding.UTF8.GetBytes(password);
            passBytes = md5Crypt.ComputeHash(passBytes);
            StringBuilder strEncryptPass = new StringBuilder();
            foreach (byte passwordByte in passBytes)
            {
                strEncryptPass.Append(passwordByte.ToString("x2").ToLower());
            }
            return strEncryptPass.ToString();

        }

    }
}

