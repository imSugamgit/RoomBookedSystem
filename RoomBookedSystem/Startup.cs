using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RoomMainSetup.BaseRepo;
using RoomMainSetup.Data;
using RoomModel.MainSetup.Assembler;
using RoomModel.MainSetup.Repository;
using RoomModel.MainSetup.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomBookedSystem
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
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("BookedSystem"), x => x.MigrationsAssembly("RoomMainSetup")));
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(24);
            });
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));


            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IRoomAssembler, RoomAssembler>();
            services.AddTransient<IRoomService, RoomService>();

            services.AddTransient<IRoomTypeRepository, RoomTypeRepository>();
            services.AddTransient<IRoomTypeAssembler, RoomTypeAssembler>();
            services.AddTransient<IRoomTypeService, RoomTypeService>();

            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<ICartAssembler, CartAssembler>();
            services.AddTransient<ICartService, CartService>(); 

            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICustomerAssembler, CustomerAssembler>();
            services.AddTransient<ICustomerService, CustomerService>(); 
            
            services.AddTransient<IFacilitiesRepository, FacilitiesRepository>();
            services.AddTransient<IFacilitiesAssembler, FacilitiesAssembler>();
            services.AddTransient<IFacilitiesService, FacilitiesService>();
            
            services.AddTransient<IRoomFacilitiesRepository, RoomFacilitiesRepository>();
            services.AddTransient<IRoomFacilitiesAssembler, RoomFacilitiesAssembler>();
            services.AddTransient<IRoomFacilitiesService, RoomFacilitiesService>();

            services.AddTransient<IInvoiceRepository, InvoiceRepository>();
            services.AddTransient<IInvoiceAssembler, InvoiceAssembler>();
            services.AddTransient<IInvoiceService, InvoiceService>();

            services.AddTransient<IRoomTypeRepository, RoomTypeRepository>();
            services.AddTransient<IRoomTypeAssembler, RoomTypeAssembler>();
            services.AddTransient<IRoomTypeService, RoomTypeService>();





            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/login";
                options.Cookie.IsEssential = true;
                options.SlidingExpiration = true; // here 1
                options.ExpireTimeSpan = TimeSpan.FromSeconds(10);// here 2
            });


            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
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
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
