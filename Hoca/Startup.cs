using Hoca.Data;
using Hoca.Models;
using Hoca.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca
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
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("HocaDBConnection")));

            services.AddIdentity<OgrenciUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 7;
                
                options.SignIn.RequireConfirmedEmail = true;

                options.Tokens.EmailConfirmationTokenProvider = "OgrenciEmailConfirmation";

                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<OgrenciEmailConfirmationTokenProvider
                    <OgrenciUser>>("OgrenciEmailConfirmation");

            services.Configure<DataProtectionTokenProviderOptions>(o =>
                            o.TokenLifespan = TimeSpan.FromHours(4));//4saat icinde onay

            services.Configure<OgrenciEmailConfirmationTokenProviderOptions>(o =>
                        o.TokenLifespan = TimeSpan.FromDays(2));

            services.AddAuthentication().AddGoogle(options =>
            {
                IConfigurationSection googleAuthNSection =
                        Configuration.GetSection("Authentication:Google");

                options.ClientId = "779662800852-pbvq0t2kdpjdgtgap46kmpsdedbange0.apps.googleusercontent.com";
                options.ClientSecret = "Ci5NYAc4FgW9XTu2PuD71Xg6";
            })
            .AddFacebook(options =>
            {
                options.AppId = "996223600913121";
                options.AppSecret = "f3111e6e509873c7d65d5c629c63ecd9";
            });

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("RolSilPolicy",
                    policy => policy.RequireClaim("Rol Sil"));

                options.AddPolicy("RolDuzenlePolicy",
                    policy => policy.RequireClaim("Rol Düzenle", "true"));

                options.AddPolicy("AdminRolePolicy",
                    policy => policy.RequireRole("Admin"));
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Hesap/Giris";
                options.LogoutPath = $"/Hesap/Cikis";
                options.AccessDeniedPath = $"/Administration/AccessDenied";
            });

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddScoped<IOgrenciRepository, SQLOgrenciRepository>();
            services.AddScoped<ISorularRepository, SQLSoruRepository>();

            services.AddSingleton<DataProtectionPurposeStrings>();
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
                app.UseExceptionHandler("/Hata");
                app.UseStatusCodePagesWithReExecute("/Hata/{0}");
                //app.UseHsts();
            }

            //app.UseStatusCodePages();

            //app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Ogrenci}/{action=Index}/{id?}");
            });
        }
    }
}
