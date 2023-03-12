using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json.Serialization;
using Autofac;
using Gos.Core;
using Gos.Infrastructure.CompositionRoot;
using Gos.Services.CompositionRoot;
using Gos.Services.Framework;
using Gos.Web.CompositionRoot;
using Gos.Web.Framework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Gos.Web
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
            services.AddLocalization();

            services.AddSession(
                opts =>
                {
                    opts.IdleTimeout = TimeSpan.FromMinutes(60);
                    opts.Cookie.HttpOnly = true;
                    opts.Cookie.IsEssential = true;
                    opts.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    opts.Cookie.SameSite = SameSiteMode.Strict;
                });

            services.AddControllersWithViews()
                .AddJsonOptions(
                    opts =>
                    {
                        // Bind empty strings to null
                        opts.JsonSerializerOptions.Converters.Add(new NullableConverterFactory());

                        // Bind strings to enums
                        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    })
                .AddRazorRuntimeCompilation()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new[]
                    {
                        new CultureInfo("sl"),
                        new CultureInfo("en")
                    };

                    options.DefaultRequestCulture = new RequestCulture("sl");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                    options.RequestCultureProviders = new List<IRequestCultureProvider>()
                    {
                        new QueryStringRequestCultureProvider(),
                        new CookieRequestCultureProvider()
                    };
                });

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new ServicesModule());
            builder.RegisterModule(new WebModule());
            builder.RegisterBuildCallback(
                c =>
                {
                    c.Resolve<CacheWarmUp>().WarmUp();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();

            // Configure settings if used in proxy
            var proxyBasePath = configuration[ConfigurationKey.Web.ProxyBasePath] ?? string.Empty;
            if (!string.IsNullOrEmpty(proxyBasePath))
            {
                app.Use(
                    (context, next) =>
                    {
                        context.Request.PathBase = new PathString(proxyBasePath);
                        return next();
                    });
            }

            app.UseForwardedHeaders(
                new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });

            // Localization
            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizationOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/error/{0}");
                app.UseExceptionHandler("/error/500");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Concordance}/{action=Index}/{id?}");
                });
        }
    }
}