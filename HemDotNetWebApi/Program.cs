
using HemDotNetWebApi.Data;
using HemDotNetWebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace HemDotNetWebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers()
                .AddJsonOptions(opt =>
                {
                    //Author: Johan Ek
                    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    //Author: Allan Crépin
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });


            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json")
                .Build()
                .GetSection("ConnectionStrings")["HemDotNetDb"])
            );

            /* Coder: Johan, Participants: All */
            builder.Services.AddIdentityCore<RealEstateAgent>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddTransient<IMarketPropertyRepository, MarketPropertyRepository>();
            builder.Services.AddTransient<IPropertyImageRepository, PropertyImageRepository>();

            // Allan
            builder.Services.AddTransient<IMunicipalityRepository, MunicipalityRepository>();
            builder.Services.AddTransient<IRealEstateAgencyRepository, RealEstateAgencyRepository>();

            // Author: CHRIS
            builder.Services.AddTransient<IRealEstateAgentRepository, RealEstateAgentRepository>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            /* Coder: Johan, Participants: All */
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    b => b.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin());
            });

            /* Coder: Allan, Participants: All */
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
                };
            });

            /* */

            var app = builder.Build();

            // Populates the database when the program is ran, and checks that the
            // tables are empty beforehand. To repopulate the database with clean data,
            // empty your tables.
            // Allan
            await DbDataSeeder.SeedAsync(app.Services);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            // If you need to serve files from outside the web root
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Images")),
                RequestPath = "/images"
            });

            app.UseHttpsRedirection();

            /* Coder: Johan, Participants: All */
            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
