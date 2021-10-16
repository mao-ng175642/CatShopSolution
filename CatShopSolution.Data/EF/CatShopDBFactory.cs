using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CatShopSolution.Data.EF
{
    public class CatShopDBFactory : IDesignTimeDbContextFactory<CatShopDbContext>
    {
        public CatShopDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

            var connectionString = configuration.GetConnectionString("CatShopSolutionDb");

            var optionsBuilder = new DbContextOptionsBuilder<CatShopDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new CatShopDbContext(optionsBuilder.Options);
        }
    }
}
