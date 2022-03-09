// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OtripleS.Portal.Web.Brokers.Apis;
using OtripleS.Portal.Web.Brokers.DateTimes;
using OtripleS.Portal.Web.Brokers.Loggings;
using OtripleS.Portal.Web.Brokers.Navigations;
using OtripleS.Portal.Web.Models.Configurations;
using OtripleS.Portal.Web.Services.Foundations.Courses;
using OtripleS.Portal.Web.Services.Foundations.Students;
using OtripleS.Portal.Web.Services.Foundations.Teachers;
using OtripleS.Portal.Web.Services.Foundations.Users;
using OtripleS.Portal.Web.Services.Views.StudentViews;
using OtripleS.Portal.Web.Services.Views.TeacherViews;
using RESTFulSense.Clients;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

namespace OtripleS.Portal.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSyncfusionBlazor();
            AddRootDirectory(services);
            services.AddScoped<IApiBroker, ApiBroker>();
            services.AddScoped<ILogger, Logger<LoggingBroker>>();
            services.AddScoped<ILoggingBroker, LoggingBroker>();
            services.AddScoped<IDateTimeBroker, DateTimeBroker>();
            services.AddScoped<INavigationBroker, NavigationBroker>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentViewService, StudentViewService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ITeacherViewService, TeacherViewService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            SyncfusionLicenseProvider.RegisterLicense(
                licenseKey: "ADD_LICENSE_HERE");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private static void AddRootDirectory(IServiceCollection services)
        {
            services.AddRazorPages(options =>
            {
                options.RootDirectory = "/Views/Pages";
            });
        }
    }
}
