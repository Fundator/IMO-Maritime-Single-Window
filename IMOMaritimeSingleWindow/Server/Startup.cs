using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using AutoMapper;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

// Local namespaces
using IMOMaritimeSingleWindow.Auth;
using IMOMaritimeSingleWindow.Data;
using IMOMaritimeSingleWindow.Extensions;
using IMOMaritimeSingleWindow.Helpers;
using IMOMaritimeSingleWindow.Models;
using IMOMaritimeSingleWindow.ViewModels.Mappings;
using IMOMaritimeSingleWindow.Identity;
using IMOMaritimeSingleWindow.Identity.Models;
using IMOMaritimeSingleWindow.Repositories;
using IMOMaritimeSingleWindow.Identity.Stores;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using IMOMaritimeSingleWindow.Identity.Helpers;
using log4net;
using log4net.Config;
using System.Reflection;
using IMOMaritimeSingleWindow.Filters;

namespace IMOMaritimeSingleWindow
{
    public class Startup
    {
        static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public Startup(IHostingEnvironment env)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));



            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
            HostingEnvironment = env;

        }


        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }
        public AuthorizationPolicy AuthorizationPolicy { get; internal set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)

        {
            // Wrap the entire configuration routine in a try/catch since the logging filter won't catch any errors that occur here
            try
            {

                // Add exception logging
                services.AddMvc(opts =>
                    opts.Filters.Add(new Log4NetExceptionFilter())
                );

                services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();
                //Configure CORS with different policies
                //services.AddCors(options =>
                //{
                //    options.AddPolicy("AllowLocalhost",
                //        b => {
                //            b.WithOrigins(new string[] {
                //                "http://localhost:4200",
                //                "https://localhost:4200"
                //            });
                //            b.WithMethods(new string[]
                //            {
                //                "GET", "OPTIONS", "POST", "UPDATE"
                //            });
                //            b.AllowAnyHeader();
                //        });

                //    //Brute force policy if all else fails
                //    //NB: Only ever use in development!
                //    options.AddPolicy("AllowAllAny",
                //        b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                //});

                //Configure database context
                string connectionStringOpenSSN = "";
                if (HostingEnvironment.IsStaging())
                {
                    // Configure in-memory database
                }
                else
                {
                    // Use real database
                    connectionStringOpenSSN = Configuration.GetConnectionString("OpenSSN");
                }

                connectionStringOpenSSN = Configuration.GetConnectionString("OpenSSN");
                var dbOptions = new DbContextOptionsBuilder<open_ssnContext>().UseNpgsql(connectionStringOpenSSN).Options;
                services.AddEntityFrameworkNpgsql().AddDbContext<open_ssnContext>(options => options.UseNpgsql(connectionStringOpenSSN));

                // Configure email service
                services.ConfigureEmailSenderOptions(Configuration);
                services.ConfigureSendGridOptions(Configuration);

                services.AddEmailSender();


                //Configure identity services
                var builder = services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                // configure identity options
                options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 4;

                // Lockout options
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                    options.Lockout.MaxFailedAccessAttempts = 10;
                    options.Lockout.AllowedForNewUsers = true;

                //Email options
                options.SignIn.RequireConfirmedEmail = true;
                });

                builder.AddDefaultTokenProviders();

                //builder.AddSignInManager<SignInManager<ApplicationUser>>()
                //builder.AddUserManager<ApplicationUserManager>()
                //.AddRoleManager<ApplicationRoleManager>();


                var serviceProvider = services.BuildServiceProvider();
                var context = serviceProvider.GetService<open_ssnContext>();
                if (context == null) throw new Exception("no service for open_ssnContext found!");

                //services.AddSingleton<IUnitOfWork<Guid>, UnitOfWork>();
                // services.AddAutoMapper();
                //var config = new MapperConfiguration(cfg => cfg.AddProfiles(typeof(Startup)));

                //services.TryAddScoped<UserManager>();
                //services.TryAddScoped<ApplicationRoleManager>();

                // Tip from https://stackoverflow.com/a/42298278
                var config = new MapperConfiguration(cfg =>
               {
                   cfg.AddProfile<IdentityEntitiesToModelsMappingProfile>();
                   cfg.AddProfile<ViewModelToEntityMappingProfile>();
               });
                services.AddSingleton<IMapper>(s => config.CreateMapper());
                services.TryAddScoped<IUnitOfWork<Guid>>(ctx => new UnitOfWork(new open_ssnContext(dbOptions)));
                serviceProvider = services.BuildServiceProvider();

                var automapper = serviceProvider.GetService<IMapper>();

                services.AddSingleton<IUserStoreHelper>(ctx => new UserStoreHelper(automapper));
                services.TryAddScoped<IdentityErrorDescriber>();
                serviceProvider = services.BuildServiceProvider();

                var userStoreHelper = serviceProvider.GetService<IUserStoreHelper>();
                var unitofwork = (UnitOfWork)serviceProvider.GetService<IUnitOfWork<Guid>>();
                var identityErrorDescriber = serviceProvider.GetService<IdentityErrorDescriber>();

                builder.AddUserManager<UserManager>()
                .AddRoleManager<ApplicationRoleManager>();


                services.TryAddScoped<IUserStore<ApplicationUser>>(ctx =>
                    new UserStore
                    (
                        identityErrorDescriber,
                        new UnitOfWork(new open_ssnContext(dbOptions)),
                        new RoleStore(new UnitOfWork(new open_ssnContext(dbOptions)), automapper),
                        userStoreHelper,
                        automapper
                    )
                );

                services.TryAddScoped<IRoleStore<ApplicationRole>>(ctx => new RoleStore(
                    new UnitOfWork(new open_ssnContext(dbOptions)),
                    automapper)
                );

                services.TryAddScoped<IDbContext>(ctx => new open_ssnContext(dbOptions));

                //services.TryAddScoped<IRoleStore<ApplicationRole>>(ctx => new RoleStore(unitofwork, automapper));
                //var roleStore = serviceProvider.GetService<RoleStore>();
                //services.TryAddScoped<IUserStore<ApplicationUser>>(ctx => new UserStore(unitofwork, roleStore, automapper));

                serviceProvider = services.BuildServiceProvider();

                var myUserManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
                //var myRoleManager = serviceProvider.GetService<RoleManager<ApplicationRole>>();

                // Additional manager separate from ASP NET Identity
                //services.TryAddScoped(ctx => new UserRoleManager<ApplicationUser, Guid, ApplicationRole, Guid>(myUserManager, myRoleManager));


                //Overriding service
                services.Replace(ServiceDescriptor.Scoped<IUserValidator<ApplicationUser>, CustomUserValidator<ApplicationUser>>());
                //services.Replace(ServiceDescriptor.Scoped<IPasswordHasher<ApplicationUser>, CustomPasswordHasher>());
                // Additional manager separate from ASP NET Identity
                //services.TryAddScoped(ctx => new UserRoleManager<ApplicationUser, Guid, ApplicationRole, Guid>(myUserManager, myRoleManager));


                // Custom services

                //services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationClaimsPrincipalFactory>();

                services.AddSingleton<IJwtFactory, JwtFactory>();


                //See IMOMaritimeSingleWindow.Extensions.IServiceCollections.cs for implementation
                services.AddJWTOptions(Configuration);
                services.AddAuthorizationPolicies();

                if (HostingEnvironment.IsProduction())
                {
                    services.AddMvc(opts =>
                        opts.Filters.Add(new RequireHttpsAttribute())
                    );
                }



                services.AddMvc()
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());





                /*services.AddSingleton<IEmailSender, EmailSender>();
                services.Configure<AuthMessageSenderOptions>(Configuration);*/

                /*var b = services.AddMvc(
                    options => {
                        var defaultPolicy = new AuthorizationPolicyBuilder(new[] { JwtBearerDefaults.AuthenticationScheme, IdentityConstants.ApplicationScheme })
                        .RequireAuthenticatedUser().Build();
                        options.Filters.Add(new AuthorizeFilter(defaultPolicy));

                    });
                */

                // Fix for json self-referencing loop bug:
                services.AddMvc().AddJsonOptions(
              options => options.SerializerSettings.ReferenceLoopHandling =
              Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            }
            catch(Exception e)
            {
                Log.Error(e);
                if (e.InnerException != null)
                {
                    Log.Error(e.InnerException);
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 &&
                   !Path.HasExtension(context.Request.Path.Value) &&
                   !context.Request.Path.Value.StartsWith("/api/", StringComparison.Ordinal))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            if (!env.IsProduction())
            {
                app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            }

            //app.UseCors("AllowAllAny");
            // app.UseCors(policyName: "AllowLocalhost");

            // IMPORTANT! UseAuthentication() must be called before UseMvc()

            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
