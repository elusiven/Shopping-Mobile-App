using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using ShoppingApp.Api.API.Common.Attributes;
using ShoppingApp.Api.API.Common.Settings;
using ShoppingApp.Api.API.Data;
using ShoppingApp.Api.API.Data.Entities;
using ShoppingApp.Api.API.Swagger;
using ShoppingApp.Api.IoC.Configuration.DI;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

#pragma warning disable CS1591

namespace ShoppingApp.Api.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; private set; }

        private AppSettings _appSettings;

        private readonly ILogger _logger;
        private IServiceProvider _serviceProvider;

        public Startup(IConfiguration configuration, IWebHostEnvironment env, IServiceProvider serviceProvider, ILogger<Startup> logger)
        {
            HostingEnvironment = env;
            Configuration = configuration;
            _serviceProvider = serviceProvider;
            _logger = logger;

            //AppSettings
            _appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();

            _logger.LogDebug("Startup::Constructor::Settings loaded");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _logger.LogTrace("Startup::ConfigureServices");

            try
            {
                if (_appSettings.IsValid())
                {
                    _logger.LogDebug("Startup::ConfigureServices::valid AppSettings");

                    ConfigureAPIVersioning(services);
                    ConfigureSwagger(services);
                    ConfigureIdentity(services);

                    services.AddControllers(
                        opt =>
                        {
                            //Custom filters, if needed
                            //opt.Filters.Add(typeof(CustomFilterAttribute));
                            opt.Filters.Add(new ProducesAttribute("application/json"));
                        }
                        ).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

                    // Add Database
                    services.AddDbContext<ShoppingDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlDbContext"), x => x.MigrationsAssembly("ShoppingApp.Api.API.Data")));

                    // Add JWT Authentication
                    services.AddAuthentication(options =>
                        {
                            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        })
                        .AddJwtBearer("Bearer", options =>
                        {
                            // base-address of your identityserver
                            options.Authority = "http://localhost:44343/";

                            // name of the API resource
                            options.Audience = "app.api.shopping";

                            options.RequireHttpsMetadata = false;
                        });

                    services.AddAuthorization();

                    //Mappings
                    services.ConfigureMappings();

                    //Business settings
                    services.ConfigureBusinessServices(Configuration);

                    _logger.LogDebug("Startup::ConfigureServices::ApiVersioning, Swagger and DI settings");
                }
                else
                    _logger.LogDebug("Startup::ConfigureServices::invalid AppSettings");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            _logger.LogTrace("Startup::Configure");
            _logger.LogDebug($"Startup::Configure::Environment:{env.EnvironmentName}");

            try
            {
                app.UseExceptionHandler("/error");

                app.UseHsts();

                app.UseHttpsRedirection();
                app.UseRouting();

                app.UseIdentityServer();
                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
                app.UseRequestLocalization();

                //SWAGGER
                if (_appSettings.IsValid())
                {
                    if (_appSettings.Swagger.Enabled)
                    {
                        app.UseSwagger();
                        app.UseSwaggerUI(options =>
                        {
                            foreach (var description in provider.ApiVersionDescriptions)
                            {
                                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                                //options.RoutePrefix = string.Empty;
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private void ConfigureIdentity(IServiceCollection services)
        {
            // Add Identity
            services.AddIdentity<UserEntity, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ShoppingDbContext>()
                .AddDefaultTokenProviders();
            // Add Identity Server
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<UserEntity>();
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            //SWAGGER
            if (_appSettings.Swagger.Enabled)
            {
                services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

                services.AddSwaggerGen(options =>
                {
                    options.OperationFilter<SwaggerDefaultValues>();
                    options.IncludeXmlComments(XmlCommentsFilePath);
                });
            }
        }

        private void ConfigureAPIVersioning(IServiceCollection services)
        {
            //API versioning
            services.AddApiVersioning(
                o =>
                {
                    //o.Conventions.Controller<UserController>().HasApiVersion(1, 0);
                    o.ReportApiVersions = true;
                    o.AssumeDefaultVersionWhenUnspecified = true;
                    o.DefaultApiVersion = new ApiVersion(1, 0);
                    o.ApiVersionReader = new UrlSegmentApiVersionReader();
                }
            );

            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });
        }

        private string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }
    }
}