﻿using HemDotNetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HemDotNetWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<MarketProperty> MarketProperties { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<RealEstateAgency> RealEstateAgencies { get; set; }
        public DbSet<RealEstateAgent> RealEstateAgents { get; set; }

    }
}
