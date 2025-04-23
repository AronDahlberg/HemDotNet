using HemDotNetWebApi.Constants;
using HemDotNetWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HemDotNetWebApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<RealEstateAgent>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<MarketProperty> MarketProperties { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<RealEstateAgency> RealEstateAgencies { get; set; }
        public DbSet<RealEstateAgent> RealEstateAgents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //if (this.RealEstateAgencies != null && this.RealEstateAgencies.Any())
            //{
            //    var agencies = new List<RealEstateAgency>
            //    {
            //        new RealEstateAgency
            //        {
            //            RealEstateAgencyName = "Nordic Homes",
            //            RealEstateAgencyPresentation = "Vi tar skandinavisk design till fastighetsbranschen.",
            //            RealEstateAgencyLogoUrl = "/images/PlaceholderLogo.png"
            //        },
            //        new RealEstateAgency
            //        {
            //            RealEstateAgencyName = "Nordhs Mäklarbyrå",
            //            RealEstateAgencyPresentation = "Letar du efter en pålitlig fastighetsmäklare? Våra mäklare levererar expertis och resultat för ditt bostadsköp eller -försäljning.",
            //            RealEstateAgencyLogoUrl = "/images/PlaceholderLogo.png"
            //        }
            //    };

            //    this.RealEstateAgencies.AddRange(agencies);
            //    this.SaveChanges();
            //}

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = ApiRoles.User,
                    NormalizedName = ApiRoles.User,
                    Id = "c0ef899a-9033-4d98-9851-cf7c051cc51d"
                },
                new IdentityRole
                {
                    Name = ApiRoles.Administrator,
                    NormalizedName = ApiRoles.Administrator,
                    Id = "24f60ffc-3f16-4815-83b0-bf191748018c"
                }
            );

            var hasher = new PasswordHasher<RealEstateAgent>();
            builder.Entity<RealEstateAgent>().HasData(
                new RealEstateAgent
                {
                    Id = "a7d64e4d-a8e6-40da-a431-e75fd59ecbdb",
                    Email = "user@hemdotnet.se",
                    NormalizedEmail = "USER@HEMDOTNET.SE",
                    UserName = "user@hemdotnet.se",
                    NormalizedUserName = "USER@HEMDOTNET.SE",
                    RealEstateAgentFirstName = "Anna",
                    RealEstateAgentLastName = "Svensson",
                    PasswordHash = hasher.HashPassword(null, "password"),
                    EmailConfirmed = true,
                    RealEstateAgentEmail = "anna@nordichomes.com",
                    RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                    RealEstateAgentImageUrl = "/images/RealEstateAgentWoman.jpg",
                    RealEstateAgentAgency = null
                    //RealEstateAgentAgency = RealEstateAgencies.First(a => a.RealEstateAgencyName == "Nordic Homes")
                },
                new RealEstateAgent
                {
                    Id = "bca74173-1e33-41e8-88df-5a6454c4f900",
                    Email = "admin@hemdotnet.se",
                    NormalizedEmail = "ADMIN@HEMDOTNET.SE",
                    UserName = "admin@hemdotnet.se",
                    NormalizedUserName = "ADMIN@HEMDOTNET.SE",
                    RealEstateAgentFirstName = "Mikael",
                    RealEstateAgentLastName = "Strand",
                    PasswordHash = hasher.HashPassword(null, "password"),
                    EmailConfirmed = true,
                    RealEstateAgentEmail = "mikael@nordichomes.com",
                    RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                    RealEstateAgentImageUrl = "/images/RealEstateAgentMan.jpg",
                    RealEstateAgentAgency = null
                    //RealEstateAgentAgency = RealEstateAgencies.First(a => a.RealEstateAgencyName == "Nordhs Mäklarbyrå")
                }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = "c0ef899a-9033-4d98-9851-cf7c051cc51d",
                    UserId = "a7d64e4d-a8e6-40da-a431-e75fd59ecbdb"
                },
                new IdentityUserRole<string>()
                {
                    RoleId = "24f60ffc-3f16-4815-83b0-bf191748018c",
                    UserId = "bca74173-1e33-41e8-88df-5a6454c4f900"
                }
                );


        }

    }
}
