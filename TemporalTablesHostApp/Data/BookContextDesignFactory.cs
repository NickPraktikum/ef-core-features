namespace TemporalTablesHostApp.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    public class BookContextDesignFactory : IDesignTimeDbContextFactory<BookContext>
    {
        public BookContext CreateDbContext(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var connectionString = config.GetConnectionString("BookDb");
            var options = new DbContextOptionsBuilder<BookContext>()
                .UseSqlServer(connectionString, options =>
                {
                    options.MigrationsHistoryTable("MigrationHistory", "SystemData");
                    options.CommandTimeout(20);
                }).Options;
            return new BookContext(options);
        }
    }
}
