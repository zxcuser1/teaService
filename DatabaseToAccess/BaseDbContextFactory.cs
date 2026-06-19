using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DatabaseToAccess
{
    public class BaseDbContextFactory : IDesignTimeDbContextFactory<BaseDbContext>
    {
        public BaseDbContext CreateDbContext(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var basePath = Path.Combine(currentDirectory, "GatewayApi");

            if (!Directory.Exists(basePath))
            {
                basePath = Path.GetFullPath(Path.Combine(currentDirectory, "..", "GatewayApi"));
            }

            if (!Directory.Exists(basePath))
            {
                throw new DirectoryNotFoundException($"GatewayApi directory was not found. Current directory: {currentDirectory}");
            }

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BaseDbContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

            optionsBuilder.UseNpgsql(connectionString);

            return new BaseDbContext(optionsBuilder.Options);
        }
    }
}
