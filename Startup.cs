using System;
using System.IO;
using System.Reflection;
using System.Text;
using ConferenceMonitorApi.Data;
using ConferenceMonitorApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ConferenceMonitorApi
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<DatabaseContext>();
            services.AddScoped<IConferenceRepository, ConferenceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddAuthorization(option =>
            {
                option.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = _configuration.GetSection("Credentials").GetSection("Issuer").Value,
                    ValidAudience = _configuration.GetSection("Credentials").GetSection("Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Credentials").GetSection("SecretKey").Value))
                };
            });
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Conference Monitor API",
                    Version = "v1",
                    Description = "A back-end RESTful Web API for publishing and managing conferences.",
                    Contact = new OpenApiContact{
                        Name = "Placide IRANDORA",
                        Url = new Uri("https://twitter.com/placideirandora")
                    },
                    License = new OpenApiLicense{
                        Name = "GNU GENERAL PUBLIC LICENCE v3",
                        Url = new Uri("https://www.gnu.org/licenses/gpl-3.0.en.html")
                    }
                });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                        In = ParameterLocation.Header, 
                        Description = "Insert your JWT (format: [Bearer][token value] ex: Bearer eyJh.eyJVc2.eyJVc2 ) into the value field below, and click on Authorize to be able to access the protected endpoints.",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey 
                    });
                
                option.AddSecurityRequirement(new OpenApiSecurityRequirement {
                { 
                    new OpenApiSecurityScheme 
                    { 
                    Reference = new OpenApiReference 
                    { 
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer" 
                    } 
                    },
                    new string[] { } 
                    } 
                });

                    // Set the comments path for the Swagger JSON and UI.
                    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    option.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseAuthentication();

            // Enable and configure Swagger middleware to serve API Documentation on a specific endpoint
            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "Conference Monitor API v1");
            });
        }
    }
}
