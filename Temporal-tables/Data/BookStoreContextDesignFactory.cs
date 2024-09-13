namespace TemporalTable.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    public class BookStoreContextDesignFactory : IDesignTimeDbContextFactory<BookStoreContext>
    {
        public BookStoreContext CreateDbContext(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var connectionString = config.GetConnectionString("Authors");
            var options = new DbContextOptionsBuilder<BookStoreContext>()
                .UseSqlServer(connectionString, options =>
                {
                    options.MigrationsHistoryTable("MigrationHistory", "SystemData");
                    options.CommandTimeout(20);
                }).Options;
            return new BookStoreContext(options);
        }
    }
}
