using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using ObserverPattern.Services.Dapper;
using ObserverPattern.Services.Formate;
using ObserverPattern.Services.TableData;
using Panda.DynamicWebApi;
using Swashbuckle.AspNetCore.Swagger;

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
            services.AddControllersWithViews();

            services.AddDirectoryBrowser();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvcCore();

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
            string cnStrSetting;
            switch (Configuration.GetSection("Environment").Value)
            {
                case "Debug":
                    cnStrSetting = "ConnectionStrings:DebugConnection";
                    break;
                case "Demo":
                    cnStrSetting = "ConnectionStrings:DemoConnection";
                    break;
                case "Local":
                    cnStrSetting = "ConnectionStrings:LocalhostConnection";
                    break;
                default:
                    cnStrSetting = "ConnectionStrings:DefaultConnection";
                    break;
            }
            //���U�s�u�r��
            var dBConnection = Configuration.GetSection(cnStrSetting);
            services.Configure<DapperService>(dBConnection);
            DapperService._defaultConnection = dBConnection.Value;

            
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

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "�p��O�ƥ� WebApi v1"));

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //---ADD
            app.UseFileServer(new FileServerOptions
            {
                EnableDirectoryBrowsing = true
            });

            app.UseDeveloperExceptionPage();
        }
    }
}
