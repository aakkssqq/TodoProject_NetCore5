using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;

using System;
using System.Linq;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.UI.Services;
using NLog;
using ObserverPattern.Base.Organization;
using Panda.DynamicWebApi;
using Swashbuckle.AspNetCore.Swagger;
using ObserverPattern.Services.Dapper;
using ObserverPattern.Services.Formate;
using ObserverPattern.Services.TableData;
using ObserverPatternWeb.Data;
using ObserverPatternWeb.Middleware;

namespace ObserverPatternWeb
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

            #region �n�J����
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDatabaseDeveloperPageExceptionFilter();

            // Password settings  
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;    //�K�X���ݭn���Ʀr
                options.Password.RequiredLength = 8;     //�K�X�ܤ֭n8�Ӧr����
                options.Password.RequireNonAlphanumeric = false;  //���ݭn�Ÿ��r��
                options.Password.RequireUppercase = false;         //���ݭn�j�g�^��r��
                options.Password.RequireLowercase = false;        //���@�w�n���p�g�^��r��
                options.Password.RequiredUniqueChars = 6;         //�ܤ֭n���Ӧr�����@��

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);  //5�����S�����R�N�۰����w�����A�w�]5����
                options.Lockout.MaxFailedAccessAttempts = 3;                       //�T���K�X�~�N��w����, �w�]5��
                options.Lockout.AllowedForNewUsers = true;                         //�s�W���ϥΪ̤]�|�Q��w�A�N�O�ǳW�S���s�H�u��

                // User settings  
                options.User.RequireUniqueEmail = true;             //�l�c���୫�Шϥ�
            });

            services.AddDefaultIdentity<CusUser>(
                options => {
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedAccount = false;
                }
            ).AddEntityFrameworkStores<ApplicationDbContext>();


            //Seting the Account Login page  
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings  
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.LoginPath = "/Pages/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login  
                options.LogoutPath = "/Pages/Login2"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout  
                options.AccessDeniedPath = "/Pages/Register"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied  
                options.SlidingExpiration = true;
            });

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(option =>
            //    {
            //        //option.AccessDeniedPath = "/Pages/Register";
            //        //option.LoginPath = "/Pages/Login";
            //    });
            #endregion
            services.AddControllersWithViews();

            services.AddDirectoryBrowser();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            

            #region �h��y�t
            services.AddControllersWithViews()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();
            services.AddLocalization(options =>
            {
                //options.ResourcesPath = "OPStrings";
            });
            #endregion

            // �ʺAWebApi �����m�� AddMvc ����
            services.AddDynamicWebApi();

            //Swagger
            services.AddSwaggerGen(options =>
            {
                //c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "�p��O�ƥ� WebApi", Version = "v1" });

                options.DocInclusionPredicate((docName, description) => true);

                //options.IncludeXmlComments(@"bin\Debug\netcoreapp2.2\Xc.StuMgr.WebApiHost.xml");
                //options.IncludeXmlComments(@"bin\Debug\netcoreapp2.2\Xc.StuMgr.Application.xml");
            });

            //���U�A��
            services.AddScoped<IFormateService, FormateService>();
            services.AddScoped<IDapperService, DapperService>();
            services.AddScoped<IDataServiceBase,DataServiceBase>();
            
            //�P�_����
            string cnSetting = Configuration.GetSection("Environment").Value;
            string cnStrSetting = $@"{cnSetting.Substring(0, 1).ToUpper()}{cnSetting.Substring(1, cnSetting.Length - 1)}Connection";
            var cnString = Configuration.GetConnectionString(cnStrSetting) != null ? $@"ConnectionStrings:{cnStrSetting}" : "ConnectionStrings:DefaultConnection";

            //���U�s�u�r��
            var dBConnection = Configuration.GetSection(cnString);
            services.Configure<DapperService>(dBConnection);
            DapperService._defaultConnection = dBConnection.Value;

            services.AddMvcCore();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            LogManager.Configuration.Variables["connectionString"] = Configuration.GetConnectionString("DefaultConnection");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            #region �h��y�t
            var supportedCultures = new List<CultureInfo>()
            {
                new CultureInfo("en"),
                new CultureInfo("ja"),
                new CultureInfo("zh"),
                new CultureInfo("zh-CHT"),
                new CultureInfo("zh-CHS")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("zh"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            //var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            //app.UseRequestLocalization(locOptions.Value);
            #endregion

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "�p��O�ƥ� WebApi v1"));

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            #region �n�J����
            app.UseAuthentication();
            #endregion
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Dashboards}/{action=Dashboard_1}/{id?}");
                endpoints.MapRazorPages();
            });

            //---ADD
            app.UseFileServer(new FileServerOptions
            {
                EnableDirectoryBrowsing = true
            });

            //app.UseMiddleware<MyCustomMiddleware>();//�g�k1.����Extension method�����Τ��ت�

            app.UseHttpMiddleware();//�g�k2.��Extension method

            app.Run(async (context) => {
                await context.Response.WriteAsync("Hello World!");
            });

            //app.UseDeveloperExceptionPage();
        }
    }
}
