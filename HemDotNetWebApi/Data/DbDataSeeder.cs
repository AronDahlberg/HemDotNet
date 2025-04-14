using HemDotNetWebApi.Common;
using HemDotNetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HemDotNetWebApi.Data
{
    // Allan
    public static class DbDataSeeder
    {
        // Allan
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            //await context.Database.MigrateAsync();

            // --- Municipality ---
            if (!context.Municipalities.Any())
            {
                context.Municipalities.Add(new Municipality
                {
                    MunicipalityName = "Stockholm"
                });
                context.Municipalities.Add(new Municipality
                {
                    MunicipalityName = "Örebro"
                });
                context.Municipalities.Add(new Municipality
                {
                    MunicipalityName = "Göteborg"
                });
                context.Municipalities.Add(new Municipality
                {
                    MunicipalityName = "Västerås"
                });
                context.Municipalities.Add(new Municipality
                {
                    MunicipalityName = "Umeå"
                });
                context.Municipalities.Add(new Municipality
                {
                    MunicipalityName = "Lund"
                });
                context.Municipalities.Add(new Municipality
                {
                    MunicipalityName = "Uppsala"
                });

                await context.SaveChangesAsync();
            }

            // --- RealEstateAgency ---
            if (!context.RealEstateAgencies.Any())
            {
                context.RealEstateAgencies.Add(new RealEstateAgency
                {
                    RealEstateAgencyName = "Nordic Homes",
                    RealEstateAgencyPresentation = "We bring Scandinavian design to real estate.",
                    RealEstateAgencyLogoUrl = "/images/PlaceholderLogo.png"
                });

                context.RealEstateAgencies.Add(new RealEstateAgency
                {
                    RealEstateAgencyName = "Nordhs Mäklarbyrå",
                    RealEstateAgencyPresentation = "Letar du efter en pålitlig fastighetsmäklare? Våra mäklare levererar expertis och resultat för ditt bostadsköp eller -försäljning.",
                    RealEstateAgencyLogoUrl = "/images/PlaceholderLogo.png"
                });

                await context.SaveChangesAsync();
            }

            // --- RealEstateAgent ---
            if (!context.RealEstateAgents.Any())
            {
                var agency = await context.RealEstateAgencies.FirstAsync();

                context.RealEstateAgents.Add(new RealEstateAgent
                {
                    RealEstateAgentFirstName = "Anna",
                    RealEstateAgentLastName = "Svensson",
                    RealEstateAgentEmail = "anna@nordichomes.com",
                    RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                    RealEstateAgentImageUrl = "/images/RealEstateAgentWoman.jpg",
                    RealEstateAgentAgency = agency
                });

                context.RealEstateAgents.Add(new RealEstateAgent
                {
                    RealEstateAgentFirstName = "Mikael",
                    RealEstateAgentLastName = "Strand",
                    RealEstateAgentEmail = "mikael@nordichomes.com",
                    RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                    RealEstateAgentImageUrl = "/images/RealEstateAgentMan.jpg",
                    RealEstateAgentAgency = agency
                });

                context.RealEstateAgents.Add(new RealEstateAgent
                {
                    RealEstateAgentFirstName = "Maria",
                    RealEstateAgentLastName = "Olsson",
                    RealEstateAgentEmail = "maria@nordichomes.com",
                    RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                    RealEstateAgentImageUrl = "/images/RealEstateAgentWoman.jpg",
                    RealEstateAgentAgency = agency
                });

                agency = await context.RealEstateAgencies.Skip(1).FirstAsync();

                await context.SaveChangesAsync();
            }

            // --- MarketProperty ---
            if (!context.MarketProperties.Any())
            {
                var municipality = await context.Municipalities.FirstAsync();
                var agent = await context.RealEstateAgents
                    .Include(a => a.RealEstateAgentAgency)
                    .FirstAsync();

                context.MarketProperties.Add(new MarketProperty
                {
                    Municipality = municipality,
                    Category = PropertyCategory.Villa, // Use your enum here
                    Price = 4500000,
                    LivingArea = 120.5,
                    AncillaryArea = 20.0,
                    LotArea = 600.0,
                    PropertyAddress = "Vasagatan 10",
                    Description = "A cozy villa with modern interior in the heart of the city.",
                    AmountOfRooms = 5,
                    MonthlyFee = 1500,
                    YearlyMaintenanceCost = 18000,
                    ContructionYear = 1995,
                    RealEstateAgent = agent
                });

                await context.SaveChangesAsync();
            }

            // --- PropertyImage ---
            if (!context.PropertyImages.Any())
            {
                var property = await context.MarketProperties.FirstAsync();

                context.PropertyImages.Add(new PropertyImage
                {
                    PropertyImageMarketProperty = property,
                    PropertyImageUrl = "https://example.com/images/villa_front.jpg"
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
