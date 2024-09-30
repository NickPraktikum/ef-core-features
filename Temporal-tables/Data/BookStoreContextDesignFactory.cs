namespace TemporalTable.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    public class BookStoreContextDesignFactory : IDesignTimeDbContextFactory<BookStoreContext>
    {
        public BookStoreContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<BookStoreContext>()
                .UseSqlServer("Data Source=.;Initial Catalog=TemporalTables;Integrated Security=False;User ID=sa;Password=Sql-Server-Dev;Encrypt=True;TrustServerCertificate=True;Application Name=EfTemporalTables", options =>
                {
                    options.MigrationsHistoryTable("MigrationHistory", "SystemData");
                    options.CommandTimeout(20);
                }).Options;
            return new BookStoreContext(options);
        }
    }
}
