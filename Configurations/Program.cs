using Configurations.Configurations;
using Experiments.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// Create a hosted app and run it
var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddTransient<MyApp>();
        services.AddDbContext<ExperimentDbContext>(
            (s, b) =>
            {
                var config = s.GetRequiredService<IConfiguration>();
                var connectionString = config.GetConnectionString("Authors");
                b.UseSqlServer(
                    connectionString,
                    a =>
                    {
                        a.MigrationsHistoryTable("MigrationHistory", "SystemData");
                        a.CommandTimeout(20);
                    });
            });
    });
var app = builder.Build();
await app.Services.CreateScope().ServiceProvider.GetRequiredService<MyApp>().StartAsync();
Console.WriteLine("Done!");
Console.ReadKey();

/// <summary>
/// Represents my app logic.
/// </summary>
class MyApp
{
    private ILogger<MyApp> _logger;
    private ExperimentDbContext _context;

    public MyApp(ILogger<MyApp> logger, ExperimentDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task StartAsync()
    {
        // Retrieve all authors including related entities (books).
        var authorsWithBooks = await _context.Authors.ToListAsync();
        var authorsWithoutBooks = await _context.Authors.IgnoreAutoIncludes().AsNoTracking().ToListAsync();
        
  
        // Retrieve filtered books, which price is higher than 30 dollars.
        var booksWithPriceFiltation = await _context.Books.IgnoreAutoIncludes().ToListAsync();
        var booksWithoutPriceFiltation = await _context.Books.IgnoreAutoIncludes().IgnoreQueryFilters().ToListAsync();
    }
}

class ExperimentDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ExperimentDbContext>
{
    /// <inheritdoc />
    public ExperimentDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<ExperimentDbContext>().UseSqlServer(
                "Data Source=.;Initial Catalog=Experiments;Integrated Security=False;User ID=sa;Password=Sql-Server-Dev;Encrypt=True;TrustServerCertificate=True;Application Name=EfExperiments",
                o =>
                {
                    o.MigrationsHistoryTable("MigrationHistory", "SystemData");
                    o.CommandTimeout(3600);
                }).UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            .Options;
        return new ExperimentDbContext(options);
    }
}

/// <summary>
/// The central type which "talks" to the database.
/// </summary>
internal class ExperimentDbContext : DbContext
{
    #region constructors and destructors

    public ExperimentDbContext(DbContextOptions options) : base(options)
    {
    }

    #endregion


    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(x =>
        {
            x.HasIndex(i => i.Isbn, "UX_Books_Isbn")
                .IsUnique();
            x.HasIndex(i => i.Title, "IX_Books_Title");
        });
        modelBuilder.Entity<Author>(x =>
        {
            x.HasIndex(i => i.FirstName, "IX_Author_FirstName");
        });
        modelBuilder.ApplyConfiguration(new AuthorEntityConfiguration());
        modelBuilder.ApplyConfiguration(new BookEntityConfiguration());
    }
    #region properties

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    #endregion
}