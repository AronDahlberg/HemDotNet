using HemDotNetWebApi.Common;
using HemDotNetWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            // These should be called in exactly this order
            await PopulateMunicipalities(context);
            await PopulateRealEstateAgencies(context);
            await PopulateRealEstateAgents(context);
            await PopulateMarketProperties(context);
            await PopulatePropertyImages(context);
        }

        // Allan
        public static async Task PopulateMunicipalities(ApplicationDbContext? context)
        {
            if (context != null && !context.Municipalities.Any())
            {
                var municipalities = new List<Municipality>
                {
                    new Municipality { MunicipalityName = "Stockholm" },
                    new Municipality { MunicipalityName = "Örebro" },
                    new Municipality { MunicipalityName = "Göteborg" },
                    new Municipality { MunicipalityName = "Västerås" },
                    new Municipality { MunicipalityName = "Umeå" },
                    new Municipality { MunicipalityName = "Lund" },
                    new Municipality { MunicipalityName = "Uppsala" }
                };

                context.Municipalities.AddRange(municipalities);
                await context.SaveChangesAsync();
            }
        }

        // Allan
        public static async Task PopulateRealEstateAgencies(ApplicationDbContext? context)
        {
            if (context != null && !context.RealEstateAgencies.Any())
            {
                var agencies = new List<RealEstateAgency>
                {
                    new RealEstateAgency
                    {
                        RealEstateAgencyName = "Nordic Homes",
                        RealEstateAgencyPresentation = "Vi tar skandinavisk design till fastighetsbranschen.",
                        RealEstateAgencyLogoUrl = "/images/PlaceholderLogo.png"
                    },
                    new RealEstateAgency
                    {
                        RealEstateAgencyName = "Nordhs Mäklarbyrå",
                        RealEstateAgencyPresentation = "Letar du efter en pålitlig fastighetsmäklare? Våra mäklare levererar expertis och resultat för ditt bostadsköp eller -försäljning.",
                        RealEstateAgencyLogoUrl = "/images/PlaceholderLogo.png"
                    }
                };

                context.RealEstateAgencies.AddRange(agencies);
                await context.SaveChangesAsync();
            }
        }



        // Allan
        public static async Task PopulateRealEstateAgents(ApplicationDbContext? context)
        {
            if (context != null && !context.RealEstateAgents.Any())
            {
                var agencies = await context.RealEstateAgencies.ToListAsync();
                var nordicHomes = agencies.First(a => a.RealEstateAgencyName == "Nordic Homes");
                var nordhsMaklarbyra = agencies.First(a => a.RealEstateAgencyName == "Nordhs Mäklarbyrå");

                var agents = new List<RealEstateAgent>
                {
                    new RealEstateAgent
                    {
                        RealEstateAgentFirstName = "Anna",
                        RealEstateAgentLastName = "Svensson",
                        RealEstateAgentEmail = "anna@nordichomes.com",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "/images/RealEstateAgentWoman.jpg",
                        RealEstateAgentAgency = nordicHomes
                    },
                    new RealEstateAgent
                    {
                        RealEstateAgentFirstName = "Mikael",
                        RealEstateAgentLastName = "Strand",
                        RealEstateAgentEmail = "mikael@nordichomes.com",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "/images/RealEstateAgentMan.jpg",
                        RealEstateAgentAgency = nordicHomes
                    },
                    new RealEstateAgent
                    {
                        RealEstateAgentFirstName = "Maria",
                        RealEstateAgentLastName = "Olsson",
                        RealEstateAgentEmail = "maria@nordichomes.com",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "/images/RealEstateAgentWoman.jpg",
                        RealEstateAgentAgency = nordicHomes
                    },
                    new RealEstateAgent
                    {
                        RealEstateAgentFirstName = "Lars",
                        RealEstateAgentLastName = "Olofsson",
                        RealEstateAgentEmail = "lars@nordhsmaklarbyra.com",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "/images/RealEstateAgentMan.jpg",
                        RealEstateAgentAgency = nordhsMaklarbyra
                    },
                    new RealEstateAgent
                    {
                        RealEstateAgentFirstName = "Vendela",
                        RealEstateAgentLastName = "Nordh",
                        RealEstateAgentEmail = "vendela@nordhsmaklarbyra.com",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "/images/RealEstateAgentWoman.jpg",
                        RealEstateAgentAgency = nordhsMaklarbyra
                    },
                    new RealEstateAgent
                    {
                        RealEstateAgentFirstName = "Erik",
                        RealEstateAgentLastName = "Åberg",
                        RealEstateAgentEmail = "erik@nordhsmaklarbyra.com",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "/images/RealEstateAgentMan.jpg",
                        RealEstateAgentAgency = nordhsMaklarbyra
                    },
                    new RealEstateAgent
                    {
                        RealEstateAgentFirstName = "Lisa",
                        RealEstateAgentLastName = "Karlsson",
                        RealEstateAgentEmail = "lisa@nordhsmaklarbyra.com",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "/images/RealEstateAgentWoman.jpg",
                        RealEstateAgentAgency = nordhsMaklarbyra
                    }
                };

                context.RealEstateAgents.AddRange(agents);
                await context.SaveChangesAsync();
            }
        }

        // Allan
        public static async Task PopulateMarketProperties(ApplicationDbContext? context)
        {
            if (context != null && !context.MarketProperties.Any())
            {
                var municipalities = await context.Municipalities.ToListAsync();
                var stockholm = municipalities.First(m => m.MunicipalityName == "Stockholm");
                var goteborg = municipalities.First(m => m.MunicipalityName == "Göteborg");
                var orebro = municipalities.First(m => m.MunicipalityName == "Örebro");
                var vasteras = municipalities.First(m => m.MunicipalityName == "Västerås");
                var uppsala = municipalities.First(m => m.MunicipalityName == "Uppsala");
                var umea = municipalities.First(m => m.MunicipalityName == "Umeå");

                var agents = await context.RealEstateAgents.Include(a => a.RealEstateAgentAgency).ToListAsync();
                var agent1 = agents[0]; // Anna Svensson
                var agent2 = agents[1]; // Mikael Strand
                var agent3 = agents[2]; // Maria Olsson
                var agent4 = agents[3]; // Lars Olofsson
                var agent5 = agents[4]; // Vendela Nordh
                var agent6 = agents[5]; // Erik Åberg

                var properties = new List<MarketProperty>
                {
                    new MarketProperty
                    {
                        Municipality = stockholm,
                        Category = PropertyCategory.Villa,
                        Price = 4500000,
                        LivingArea = 120.5,
                        AncillaryArea = 20.0,
                        LotArea = 600.0,
                        PropertyAddress = "Vasagatan 10",
                        Description = "En mysig villa med modern inredning i hjärtat av staden.",
                        AmountOfRooms = 5,
                        MonthlyFee = 1500,
                        YearlyMaintenanceCost = 18000,
                        ContructionYear = 1995,
                        RealEstateAgent = agent1
                    },
                    
                    new MarketProperty
                    {
                        Municipality = goteborg,
                        Category = PropertyCategory.CondominiumApartment,
                        Price = 2850000,
                        LivingArea = 85.0,
                        AncillaryArea = 5.0,
                        LotArea = 0,
                        PropertyAddress = "Bredgatan 15",
                        Description = "Ljus och luftig lägenhet med balkong och underbar utsikt över hamnen.",
                        AmountOfRooms = 3,
                        MonthlyFee = 3900,
                        YearlyMaintenanceCost = 8500,
                        ContructionYear = 2010,
                        RealEstateAgent = agent2
                    },

                    new MarketProperty
                    {
                        Municipality = orebro,
                        Category = PropertyCategory.CondominiumApartment,
                        Price = 3250000,
                        LivingArea = 110.0,
                        AncillaryArea = 15.0,
                        LotArea = 300.0,
                        PropertyAddress = "Tallvägen 8",
                        Description = "Välplanerat radhus i två plan med egen trädgård och uteplatser i både fram- och baksida.",
                        AmountOfRooms = 4,
                        MonthlyFee = 2700,
                        YearlyMaintenanceCost = 15000,
                        ContructionYear = 2005,
                        RealEstateAgent = agent3
                    },

                    new MarketProperty
                    {
                        Municipality = vasteras,
                        Category = PropertyCategory.Villa,
                        Price = 5100000,
                        LivingArea = 155.0,
                        AncillaryArea = 30.0,
                        LotArea = 850.0,
                        PropertyAddress = "Sjövägen 22",
                        Description = "Exklusiv villa med sjöutsikt och stort trädäck. Genomgående hög standard med moderna material.",
                        AmountOfRooms = 6,
                        MonthlyFee = 0,
                        YearlyMaintenanceCost = 22000,
                        ContructionYear = 2018,
                        RealEstateAgent = agent4
                    },

                    new MarketProperty
                    {
                        Municipality = uppsala,
                        Category = PropertyCategory.CondominiumApartment,
                        Price = 1950000,
                        LivingArea = 65.0,
                        AncillaryArea = 3.0,
                        LotArea = 0,
                        PropertyAddress = "Kyrkogatan 7B",
                        Description = "Charmig lägenhet med bevarade originaldetaljer från sekelskiftet i centrala Uppsala.",
                        AmountOfRooms = 2,
                        MonthlyFee = 3200,
                        YearlyMaintenanceCost = 5000,
                        ContructionYear = 1905,
                        RealEstateAgent = agent5
                    },

                    new MarketProperty
                    {
                        Municipality = umea,
                        Category = PropertyCategory.VacationHome,
                        Price = 1750000,
                        LivingArea = 78.0,
                        AncillaryArea = 12.0,
                        LotArea = 1200.0,
                        PropertyAddress = "Skogsvägen 103",
                        Description = "Mysigt fritidshus med bastu och närhet till sjö. Perfekt för familjen som vill komma nära naturen.",
                        AmountOfRooms = 3,
                        MonthlyFee = 0,
                        YearlyMaintenanceCost = 10000,
                        ContructionYear = 1985,
                        RealEstateAgent = agent6
                    }
                };

                context.MarketProperties.AddRange(properties);
                await context.SaveChangesAsync();
            }
        }



        // Allan
        public static async Task PopulatePropertyImages(ApplicationDbContext? context)
        {
            if (context != null && !context.PropertyImages.Any())
            {
                var properties = await context.MarketProperties.ToListAsync();

                var images = properties.Select(property => new PropertyImage
                {
                    PropertyImageMarketProperty = property,
                    PropertyImageUrl = "images/BasePropertyImage.jpg"
                }).ToList();

                context.PropertyImages.AddRange(images);
                await context.SaveChangesAsync();
            }
        }
    }
}