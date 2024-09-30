namespace TemporalTablesHostApp.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    public class BookContextDesignFactory : IDesignTimeDbContextFactory<BookContext>
    {
        public BookContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<BookContext>()
                .UseSqlServer("Data Source=.;Initial Catalog=HistoryTablesSample;Integrated Security=False;User ID=sa;Password=Sql-Server-Dev;Encrypt=True;TrustServerCertificate=True;Application Name=EfHistoryTablesSample", options =>
                {
                    options.MigrationsHistoryTable("MigrationHistory", "SystemData");
                    options.CommandTimeout(20);
                }).Options;
            return new BookContext(options);
        }
    }
}
