using HemDotNetWebApi.Common;
using HemDotNetWebApi.Constants;
using HemDotNetWebApi.Models;
using Microsoft.AspNetCore.Identity;
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

                int agentIdCounter = 1;

                /* Coder: Johan, Participants: All */

                var hasher = new PasswordHasher<RealEstateAgent>();

                var agents = new List<RealEstateAgent>
                {
                    new RealEstateAgent
                    {
                        //Id = Guid.NewGuid().ToString(),
                        //RealEstateAgentId = agentIdCounter++,
                        Email = "admin@hemdotnet.se",
                        NormalizedEmail = "ADMIN@HEMDOTNET.SE",
                        UserName = "admin@hemdotnet.se",
                        NormalizedUserName = "ADMIN@HEMDOTNET.SE",
                        RealEstateAgentFirstName = "Admin",
                        RealEstateAgentLastName = "Adminsson",
                        PasswordHash = hasher.HashPassword(null, "password"),
                        EmailConfirmed = true,
                        RealEstateAgentEmail = "admin@hemdotnet.se",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "Images/RealEstateAgentMan.jpg",
                        RealEstateAgentAgency = nordhsMaklarbyra
                    },
                    new RealEstateAgent
                    {
                        //Id = Guid.NewGuid().ToString(),
                        //RealEstateAgentId = agentIdCounter++,
                        Email = "anna@nordichomes.se",
                        NormalizedEmail = "ANNA@NORDICHOMES.SE",
                        UserName = "anna@nordichomes.se",
                        NormalizedUserName = "ANNA@NORDICHOMES.SE",
                        RealEstateAgentFirstName = "Anna",
                        RealEstateAgentLastName = "Svensson",
                        PasswordHash = hasher.HashPassword(null, "password"),
                        EmailConfirmed = true,
                        RealEstateAgentEmail = "anna@nordichomes.com",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "Images/RealEstateAgentWoman.jpg",
                        RealEstateAgentAgency = nordicHomes
                    },
                    new RealEstateAgent
                    {
                        //Id = Guid.NewGuid().ToString(),
                        //RealEstateAgentId = agentIdCounter++,
                        Email = "mikael@nordichomes.se",
                        NormalizedEmail = "MIKAEL@NORDICHOMES.SE",
                        UserName = "mikael@nordichomes.se",
                        NormalizedUserName = "MIKAEL@NORDICHOMES.SE",
                        RealEstateAgentFirstName = "Mikael",
                        RealEstateAgentLastName = "Strand",
                        PasswordHash = hasher.HashPassword(null, "password"),
                        EmailConfirmed = true,
                        RealEstateAgentEmail = "mikael@nordichomes.com",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "Images/RealEstateAgentMan.jpg",
                        RealEstateAgentAgency = nordicHomes
                    },
                    new RealEstateAgent
                    {
                        //Id = Guid.NewGuid().ToString(),
                        //RealEstateAgentId = agentIdCounter++,
                        Email = "maria@nordichomes.com",
                        NormalizedEmail = "MARIA@NORDICHOMES.COM",
                        UserName = "maria@nordichomes.com",
                        NormalizedUserName = "MARIA@NORDICHOMES.COM",
                        RealEstateAgentFirstName = "Maria",
                        RealEstateAgentLastName = "Olsson",
                        PasswordHash = hasher.HashPassword(null, "password"),
                        EmailConfirmed = true,
                        RealEstateAgentEmail = "maria@nordichomes.com",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "Images/RealEstateAgentWoman.jpg",
                        RealEstateAgentAgency = nordicHomes
                    },
                    new RealEstateAgent
                    {
                        //Id = Guid.NewGuid().ToString(),
                        //RealEstateAgentId = agentIdCounter++,
                        Email = "lars@nordhsmaklarbyra.com",
                        NormalizedEmail = "LARS@NORDHSMAKLARBYRA.COM",
                        UserName = "lars@nordhsmaklarbyra.com",
                        NormalizedUserName = "LARS@NORDHSMAKLARBYRA.COM",
                        RealEstateAgentFirstName = "Lars",
                        RealEstateAgentLastName = "Olofsson",
                        PasswordHash = hasher.HashPassword(null, "password"),
                        EmailConfirmed = true,
                        RealEstateAgentEmail = "lars@nordhsmaklarbyra.com",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "Images/RealEstateAgentMan.jpg",
                        RealEstateAgentAgency = nordhsMaklarbyra
                    },
                    new RealEstateAgent
                    {
                        //Id = Guid.NewGuid().ToString(),
                        //RealEstateAgentId = agentIdCounter++,
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
                        RealEstateAgentImageUrl = "Images/RealEstateAgentWoman.jpg",
                        RealEstateAgentAgency = nordhsMaklarbyra
                    },
                    new RealEstateAgent
                    {
                        //Id = Guid.NewGuid().ToString(),
                        //RealEstateAgentId = agentIdCounter++,
                        Email = "erik@nordhsmaklarbyra.com",
                        NormalizedEmail = "ERIK@NORDHSMAKLARBYRA.COM",
                        UserName = "erik@nordhsmaklarbyra.com",
                        NormalizedUserName = "ERIK@NORDHSMAKLARBYRA.COM",
                        RealEstateAgentFirstName = "Erik",
                        RealEstateAgentLastName = "Åberg",
                        PasswordHash = hasher.HashPassword(null, "password"),
                        EmailConfirmed = true,
                        RealEstateAgentEmail = "erik@nordhsmaklarbyra.com",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "Images/RealEstateAgentMan.jpg",
                        RealEstateAgentAgency = nordhsMaklarbyra
                    },
                    new RealEstateAgent
                    {
                        //Id = Guid.NewGuid().ToString(),
                        //RealEstateAgentId = agentIdCounter++,
                        Email = "lisa@nordhsmaklarbyra.com",
                        NormalizedEmail = "LISA@NORDHSMAKLARBYRA.COM",
                        UserName = "lisa@nordhsmaklarbyra.com",
                        NormalizedUserName = "LISA@NORDHSMAKLARBYRA.COM",
                        RealEstateAgentFirstName = "Lisa",
                        RealEstateAgentLastName = "Karlsson",
                        PasswordHash = hasher.HashPassword(null, "password"),
                        EmailConfirmed = true,
                        RealEstateAgentEmail = "lisa@nordhsmaklarbyra.com",
                        RealEstateAgentPhoneNumber = "+46 70 123 45 67",
                        RealEstateAgentImageUrl = "Images/RealEstateAgentWoman.jpg",
                        RealEstateAgentAgency = nordhsMaklarbyra
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
                    UserId = agents.First(a => a.Email == "admin@hemdotnet.se").Id
                });

                foreach (var agent in agents.Where(a => a.Email != "admin@hemdotnet.se"))
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
                    RealEstateAgencyLogoUrl = "Images/PlaceholderLogo.png"
                },
                new RealEstateAgency
                {
                    RealEstateAgencyName = "Nordhs Mäklarbyrå",
                    RealEstateAgencyPresentation = "Letar du efter en pålitlig fastighetsmäklare? Våra mäklare levererar expertis och resultat för ditt bostadsköp eller -försäljning.",
                    RealEstateAgencyLogoUrl = "Images/PlaceholderLogo.png"
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
                var agent1 = agents.First(a => a.RealEstateAgentEmail == "anna@nordichomes.com");
                var agent2 = agents.First(a => a.RealEstateAgentEmail == "mikael@nordichomes.com");
                var agent3 = agents.First(a => a.RealEstateAgentEmail == "maria@nordichomes.com");
                var agent4 = agents.First(a => a.RealEstateAgentEmail == "lars@nordhsmaklarbyra.com");
                var agent5 = agents.First(a => a.RealEstateAgentEmail == "vendela@nordhsmaklarbyra.com");
                var agent6 = agents.First(a => a.RealEstateAgentEmail == "erik@nordhsmaklarbyra.com");

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
                    ContructionYear = 2003,
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
                    ContructionYear = 2003,
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
                    ContructionYear = 2010,
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
                    ContructionYear = 2005,
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
                    ContructionYear = 2018,
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
                    ContructionYear = 1905,
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
                    ContructionYear = 1985,
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