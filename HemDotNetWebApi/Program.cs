
using HemDotNetWebApi.Data;
using HemDotNetWebApi.Models;
using Microsoft.EntityFrameworkCore;
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
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });


            builder.Services.AddTransient<IMarketPropertyRepository, MarketPropertyRepository>();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(new ConfigurationBuilder()
                                                                                                    .AddJsonFile("appsettings.Development.json")
                                                                                                    .Build()
                                                                                                    .GetSection("ConnectionStrings")["HemDotNetDb"]));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
