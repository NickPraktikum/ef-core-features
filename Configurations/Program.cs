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
        var authorsWithBooks = await _context.Authors.IgnoreAutoIncludes().Include(b=>b.Books).IgnoreQueryFilters().ToListAsync();
        // var authorsWithBooks = await _context.Authors.IgnoreAutoIncludes()IgnoreQueryFilters().ToListAsync();
        foreach (var author in authorsWithBooks)
        {
            if (author.Books.Count > 0)
            {
                _logger.LogInformation($"{author.FirstName} {author.SecondName} has these books: ");
                foreach (var book in author.Books)
                {
                    _logger.LogInformation($"Book {book.BookId}: {book.Title}");
                }
            }
        }

        // Retrieve all authors including related entities (books). The books with will be fetched in a split query
        var authorsWithBooksAsSplitQuery = await _context.Authors.AsSplitQuery().IgnoreQueryFilters().ToListAsync();
        // var authorsWithBooks = await _context.Authors.IgnoreAutoIncludes().ToListAsync();

        // Retrieve filtered authors. The filter is set in the entity configuration (authors whose age is bigger 5)
        var authorsWithAge = await _context.Authors.IgnoreAutoIncludes().ToListAsync();
        foreach (var author in authorsWithAge)
        {
            _logger.LogInformation($"{author.FirstName} {author.SecondName} is {author.Age} years old");
        }

        // The usage of Sql method SoundEx
        var soughtForTitle = "Title";
        var detectedBooks = await _context.Books.Where(book => ExperimentDbContext.SoundEx(book.Title) == ExperimentDbContext.SoundEx(soughtForTitle)).ToListAsync();
        if(detectedBooks.Count > 0)
        {
            foreach (var book in detectedBooks)
            {
                _logger.LogInformation($"Detected titles({book.Title}) that are phonetically alike to {soughtForTitle}");
            }
        }
        _logger.LogInformation($"Didn't detect titles that are phonetically alike to {soughtForTitle}");
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

    #region DbFunctions
    [DbFunction(Name = "SoundEx", Schema = "dbo", IsBuiltIn = true, IsNullable = false)]
    public static string SoundEx(string input)
    {
        throw new NotImplementedException();
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
    }
    #region properties

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    #endregion
}

internal class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.Navigation(author => author.Books)
            .AutoInclude();
        builder.HasQueryFilter(author => author.Age>5);
    }
}