using HemDotNetWebApi.Common;
using HemDotNetWebApi.Constants;
using HemDotNetWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HemDotNetWebApi.Data
{
    // Allan
    public static class DbDataSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // These should be called in exactly this order
            await PopulateMunicipalities(context);
            await PopulateRealEstateAgencies(context);
            await SeedRoles(context);
            await SeedUsers(context);
            await PopulateMarketProperties(context);
            await PopulatePropertyImages(context);
        }

        // Author: Group
        public static async Task SeedRoles(ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roles = new List<IdentityRole>
                {
                    new IdentityRole
                    {
                        Name = ApiRoles.User,
                        NormalizedName = ApiRoles.User.ToUpper(),
                        Id = Guid.NewGuid().ToString()
                    },
                    new IdentityRole
                    {
                        Name = ApiRoles.Administrator,
                        NormalizedName = ApiRoles.Administrator.ToUpper(),
                        Id = Guid.NewGuid().ToString()
                    }
                };

                context.Roles.AddRange(roles);
                await context.SaveChangesAsync();
            }
        }

        // Allan
        public static async Task SeedUsers(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                var agencies = await context.RealEstateAgencies.ToListAsync();
                var nordicHomes = agencies.First(a => a.RealEstateAgencyName == "Nordic Homes");
                var nordhsMaklarbyra = agencies.First(a => a.RealEstateAgencyName == "Nordhs Mäklarbyrå");
                var agency3 = agencies.First(a => a.RealEstateAgencyName == "VästkustMäklarna");
                var agency4 = agencies.First(a => a.RealEstateAgencyName == "Östergötlands Fastighetsförmedling");
                var agency5 = agencies.First(a => a.RealEstateAgencyName == "NorrMäklare");
                var agency6 = agencies.First(a => a.RealEstateAgencyName == "Trygga Tak");
                var agency7 = agencies.First(a => a.RealEstateAgencyName == "Svenska Hem");

                /* Coder: Johan, Participants: All */

                var hasher = new PasswordHasher<RealEstateAgent>();

                var agents = new List<RealEstateAgent>
                {
                    new RealEstateAgent
                    {
                        Email = "admin@hem.net",
                        NormalizedEmail = "ADMIN@HEM.NET",
                        UserName = "admin@hem.net",
                        NormalizedUserName = "ADMIN@HEM.NET",
                        RealEstateAgentFirstName = "Admin",
                        RealEstateAgentLastName = "Admin",
                        PasswordHash = hasher.HashPassword(null, "password"),
                        EmailConfirmed = true,
                        RealEstateAgentEmail = "admin@hem.net",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "Images/RealEstateAgentMan.jpg",
                        RealEstateAgentAgency = nordhsMaklarbyra
                    },
                    new RealEstateAgent
                    {
                        Email = "anna@nordichomes.se",
                        NormalizedEmail = "ANNA@NORDICHOMES.SE",
                        UserName = "anna@nordichomes.se",
                        NormalizedUserName = "ANNA@NORDICHOMES.SE",
                        RealEstateAgentFirstName = "Anna",
                        RealEstateAgentLastName = "Svensson",
                        PasswordHash = hasher.HashPassword(null, "password"),
                        EmailConfirmed = true,
                        RealEstateAgentEmail = "anna@nordichomes.se",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "Images/RealEstateAgentWoman2.png",
                        RealEstateAgentAgency = nordicHomes
                    },
                    new RealEstateAgent
                    {
                        Email = "mikael@tryggatak.se",
                        NormalizedEmail = "MIKAEL@TRYGGATAK.SE",
                        UserName = "mikael@tryggatak.se",
                        NormalizedUserName = "MIKAEL@TRYGGATAK.SE",
                        RealEstateAgentFirstName = "Mikael",
                        RealEstateAgentLastName = "Strand",
                        PasswordHash = hasher.HashPassword(null, "password"),
                        EmailConfirmed = true,
                        RealEstateAgentEmail = "mikael@tryggatak.se",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "Images/RealEstateAgentMan3.png",
                        RealEstateAgentAgency = agency6
                    },
                    new RealEstateAgent
                    {
                        Email = "maria@ogf.se",
                        NormalizedEmail = "MARIA@OGF.SE",
                        UserName = "maria@ogf.se",
                        NormalizedUserName = "MARIA@OGF.SE",
                        RealEstateAgentFirstName = "Maria",
                        RealEstateAgentLastName = "Olsson",
                        PasswordHash = hasher.HashPassword(null, "password"),
                        EmailConfirmed = true,
                        RealEstateAgentEmail = "maria@ogf.se",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "Images/RealEstateAgentWoman4.png",
                        RealEstateAgentAgency = agency4
                    },
                    new RealEstateAgent
                    {
                        Email = "lars@norrmaklare.se",
                        NormalizedEmail = "LARS@NORRMAKLARE.SE",
                        UserName = "lars@norrmaklare.se",
                        NormalizedUserName = "LARS@NORRMAKLARE.SE",
                        RealEstateAgentFirstName = "Lars",
                        RealEstateAgentLastName = "Olofsson",
                        PasswordHash = hasher.HashPassword(null, "password"),
                        EmailConfirmed = true,
                        RealEstateAgentEmail = "lars@norrmaklare.se",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "Images/RealEstateAgentMan2.png",
                        RealEstateAgentAgency = agency5
                    },
                    new RealEstateAgent
                    {
                        Email = "vendela@nordhsmaklarbyra.com",
                        NormalizedEmail = "VENDELA@NORDHSMAKLARBYRA.COM",
                        UserName = "vendela@nordhsmaklarbyra.com",
                        NormalizedUserName = "VENDELA@NORDHSMAKLARBYRA.COM",
                        RealEstateAgentFirstName = "Vendela",
                        RealEstateAgentLastName = "Nordh",
                        PasswordHash = hasher.HashPassword(null, "password"),
                        EmailConfirmed = true,
                        RealEstateAgentEmail = "vendela@nordhsmaklarbyra.com",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "Images/RealEstateAgentWoman3.png",
                        RealEstateAgentAgency = nordhsMaklarbyra
                    },
                    new RealEstateAgent
                    {
                        Email = "erik@svenskahem.se",
                        NormalizedEmail = "ERIK@SVENSKAHEM.SE",
                        UserName = "erik@svenskahem.se",
                        NormalizedUserName = "ERIK@SVENSKAHEM.SE",
                        RealEstateAgentFirstName = "Erik",
                        RealEstateAgentLastName = "Åberg",
                        PasswordHash = hasher.HashPassword(null, "password"),
                        EmailConfirmed = true,
                        RealEstateAgentEmail = "erik@svenskahem.se",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "Images/RealEstateAgentMan6.png",
                        RealEstateAgentAgency = agency7
                    },
                    new RealEstateAgent
                    {
                        Email = "lisa@vastkustmaklarna.se",
                        NormalizedEmail = "LISA@VASTKUSTMAKLARNA.SE",
                        UserName = "lisa@vastkustmaklarna.se",
                        NormalizedUserName = "LISA@VASTKUSTMAKLARNA.SE",
                        RealEstateAgentFirstName = "Lisa",
                        RealEstateAgentLastName = "Karlsson",
                        PasswordHash = hasher.HashPassword(null, "password"),
                        EmailConfirmed = true,
                        RealEstateAgentEmail = "lisa@vastkustmaklarna.se",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "Images/RealEstateAgentWoman.jpg",
                        RealEstateAgentAgency = agency3
                    }
                };


                context.Users.AddRange(agents);
                await context.SaveChangesAsync();

                var userRoleId = context.Roles.First(r => r.Name == ApiRoles.User).Id;
                var adminRoleId = context.Roles.First(r => r.Name == ApiRoles.Administrator).Id;

                var userRoles = new List<IdentityUserRole<string>>();

                userRoles.Add(new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = agents.First(a => a.Email == "admin@hem.net").Id
                });

                foreach (var agent in agents.Where(a => a.Email != "admin@hem.net"))
                {
                    userRoles.Add(new IdentityUserRole<string>
                    {
                        RoleId = userRoleId,
                        UserId = agent.Id
                    });
                }

                context.UserRoles.AddRange(userRoles);
                await context.SaveChangesAsync();
            }
        }
        // Allan
        public static async Task PopulateMunicipalities(ApplicationDbContext? context)
        {
            string filePath = "SeedData/Cities.json";

            if (context != null && !context.Municipalities.Any())
            {

                if (File.Exists(filePath))
                {
                    var jsonData = File.ReadAllText(filePath);
                    var citiesFromJson = JsonSerializer.Deserialize<List<List<string>>>(jsonData);

                    if (citiesFromJson == null || !citiesFromJson.Any())
                        return;

                    var cityList = citiesFromJson
                        .Select(data => data[3])
                        .Distinct()
                        .ToList();

                    var municipalities = cityList
                        .Select(city => new Municipality { MunicipalityName = city })
                        .ToList();

                    context.Municipalities.AddRange(municipalities);
                    await context.SaveChangesAsync();
                }
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
                        RealEstateAgencyName = "Wait list",
                        RealEstateAgencyPresentation = "Vänta tills administrationen godkänner ditt konto.",
                        RealEstateAgencyLogoUrl = "Images/PlaceholderLogo2.png",
                        RealEstateAgencyMunicipality = "Stockholm"
                    },
                    new RealEstateAgency
                    {
                        RealEstateAgencyName = "Nordic Homes",
                        RealEstateAgencyPresentation = "Vi tar skandinavisk design till fastighetsbranschen.",
                        RealEstateAgencyLogoUrl = "Images/logoipsum-248.svg",
                        RealEstateAgencyMunicipality = "Göteborg"
                    },
                    new RealEstateAgency
                    {
                        RealEstateAgencyName = "Nordhs Mäklarbyrå",
                        RealEstateAgencyPresentation = "Letar du efter en pålitlig fastighetsmäklare? Våra mäklare levererar expertis och resultat för ditt bostadsköp eller -försäljning.",
                        RealEstateAgencyLogoUrl = "Images/logoipsum-283.svg",
                        RealEstateAgencyMunicipality = "Malmö"
                    },
                    new RealEstateAgency
                    {
                        RealEstateAgencyName = "Svenska Hem",
                        RealEstateAgencyPresentation = "Vi förverkligar dina bostadsdrömmar med personligt engagemang och lokalkännedom.",
                        RealEstateAgencyLogoUrl = "Images/logoipsum-293.svg",
                        RealEstateAgencyMunicipality = "Helsingborg"
                    },
                    new RealEstateAgency
                    {
                        RealEstateAgencyName = "VästkustMäklarna",
                        RealEstateAgencyPresentation = "Din lokala mäklare längs västkusten – vi hittar rätt köpare till ditt hem.",
                        RealEstateAgencyLogoUrl = "Images/logoipsum-355.svg",
                        RealEstateAgencyMunicipality = "Halmstad"
                    },
                    new RealEstateAgency
                    {
                        RealEstateAgencyName = "Trygga Tak",
                        RealEstateAgencyPresentation = "Trygghet, transparens och toppresultat – det är vår filosofi i varje bostadsaffär.",
                        RealEstateAgencyLogoUrl = "Images/logoipsum-370.svg",
                        RealEstateAgencyMunicipality = "Uppsala"
                    },
                    new RealEstateAgency
                    {
                        RealEstateAgencyName = "Östergötlands Fastighetsförmedling",
                        RealEstateAgencyPresentation = "Vi kan marknaden i Östergötland och hjälper dig från värdering till försäljning.",
                        RealEstateAgencyLogoUrl = "Images/logoipsum-249.svg",
                        RealEstateAgencyMunicipality = "Linköping"
                    },
                    new RealEstateAgency
                    {
                        RealEstateAgencyName = "NorrMäklare",
                        RealEstateAgencyPresentation = "Vi kombinerar lokal expertis med smart teknik för att maximera värdet på din bostad.",
                        RealEstateAgencyLogoUrl = "Images/logoipsum-298.svg",
                        RealEstateAgencyMunicipality = "Umeå"
                    }
                };

                context.RealEstateAgencies.AddRange(agencies);
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
                var agent1 = agents.First(a => a.RealEstateAgentEmail == "anna@nordichomes.se");
                var agent2 = agents.First(a => a.RealEstateAgentEmail == "mikael@tryggatak.se");
                var agent3 = agents.First(a => a.RealEstateAgentEmail == "maria@ogf.se");
                var agent4 = agents.First(a => a.RealEstateAgentEmail == "lars@norrmaklare.se");
                var agent5 = agents.First(a => a.RealEstateAgentEmail == "vendela@nordhsmaklarbyra.com");
                var agent6 = agents.First(a => a.RealEstateAgentEmail == "erik@svenskahem.se");

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
                    ConstructionYear = 1995,
                    RealEstateAgent = agent1,
                    IsActive = true,
                    IsSold = false,
                },

                new MarketProperty
                {
                    Municipality = stockholm,
                    Category = PropertyCategory.CondominiumApartment,
                    Price = 1400000,
                    LivingArea = 38.0,
                    AncillaryArea = 12.0,
                    LotArea = 38.0,
                    PropertyAddress = "Drottningatan 5",
                    Description = "Mysig liten lägenhet utan mögel på väggarna.",
                    AmountOfRooms = 1,
                    MonthlyFee = 4000,
                    YearlyMaintenanceCost = 10000,
                    ConstructionYear = 2003,
                    RealEstateAgent = agent6,
                    IsActive = true,
                    IsSold = false,
                },

                new MarketProperty
                {
                    Municipality = stockholm,
                    Category = PropertyCategory.CondominiumApartment,
                    Price = 1500000,
                    LivingArea = 43.0,
                    AncillaryArea = 11.0,
                    LotArea = 43.0,
                    PropertyAddress = "Drottningatan 6",
                    Description = "Mysig liten lägenhet mitt i stan.",
                    AmountOfRooms = 1,
                    MonthlyFee = 4100,
                    YearlyMaintenanceCost = 10100,
                    ConstructionYear = 2003,
                    RealEstateAgent = agent6,
                    IsActive = true,
                    IsSold = true,
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
                    ConstructionYear = 2010,
                    RealEstateAgent = agent2,
                    IsActive = true,
                    IsSold = false,
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
                    ConstructionYear = 2005,
                    RealEstateAgent = agent3,
                    IsActive = true,
                    IsSold = false,
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
                    ConstructionYear = 2018,
                    RealEstateAgent = agent4,
                    IsActive = true,
                    IsSold = false,
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
                    ConstructionYear = 1905,
                    RealEstateAgent = agent5,
                    IsActive = true,
                    IsSold = false,
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
                    ConstructionYear = 1985,
                    RealEstateAgent = agent6,
                    IsActive = true,
                    IsSold = false,
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
                    MarketPropertyId = property.MarketPropertyId,
                    PropertyImageUrl = "Images/BasePropertyImage.jpg"
                }).ToList();

                context.PropertyImages.AddRange(images);
                await context.SaveChangesAsync();
            }
        }
    }
}