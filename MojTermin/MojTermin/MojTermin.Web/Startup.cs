using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MojTermin.Domain.Identity;
using MojTermin.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MojTermin.Repository.Implementation;
using MojTermin.Repository.Interface;
using MojTermin.Service.Implementation;
using MojTermin.Service.Interface;
using MojTermin.Domain;
using MojTermin.Service;

namespace MojTermin.Web
{
    public class Startup
    {
        private EmailSettings emailSettings;
        public Startup(IConfiguration configuration)
        {
            emailSettings = new EmailSettings();
            Configuration = configuration;
            Configuration.GetSection("EmailSettings").Bind(emailSettings);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<MojTerminUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped(typeof(IDoctorRepository), typeof(DoctorRepository));
            services.AddScoped(typeof(IPatientRepository), typeof(PatientRepository));
            services.AddScoped(typeof(ISpecialtyRepository), typeof(SpecialtyRepository));
            services.AddScoped(typeof(IDiagnosisRepository), typeof(DiagnosisRepository));
            services.AddScoped(typeof(IVisitRepository), typeof(VisitRepository));
            services.AddScoped(typeof(IReferralRepository), typeof(ReferralRepository));
            services.AddScoped(typeof(IRoleRepository), typeof(RoleRepository));
            services.AddScoped(typeof(IEmailRepository), typeof(EmailRepository));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));


            services.AddTransient<IDoctorService, Service.Implementation.DoctorService>();
            services.AddTransient<IPatientService, Service.Implementation.PatientService>();
            services.AddTransient<ISpecialtyService, Service.Implementation.SpecialtyService>();
            services.AddTransient<IDiagnosisService, Service.Implementation.DiagnosisService>();
            services.AddTransient<IVisitService, Service.Implementation.VisitService>();
            services.AddTransient<IRoleService, Service.Implementation.RoleService>();
            services.AddTransient<IReferralService, Service.Implementation.ReferralService>();
            services.AddTransient<IBackgroundEmailSender, Service.Implementation.BackgroundEmailSender>();
            services.AddTransient<IEmailService, Service.Implementation.EmailService>();

            services.AddScoped<EmailSettings>(es => emailSettings);
            services.AddScoped<IEmailService, EmailService>(email => new EmailService(emailSettings));
            services.AddScoped<IBackgroundEmailSender, BackgroundEmailSender>();
            services.AddHostedService<EmailScopedHostedService>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
