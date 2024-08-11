using Interceptors.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// Create a hosted app and run it
var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddTransient<MyApp>();
        services.AddDbContext<InterceptorDbContext>(
            (s, b) =>
            {
                var config = s.GetRequiredService<IConfiguration>();
                var connectionString = config.GetConnectionString("Interceptor");
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
    private InterceptorDbContext _context;

    public MyApp(ILogger<MyApp> logger, InterceptorDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task StartAsync()
    {

        var books = await _context.Books.ToListAsync();
        var authors = await _context.Authors.ToListAsync();
        await _context.Authors.AddAsync(new AuthorEntity() { FirstName="John", SecondName="Doe", BirthDate = new DateTimeOffset(), Books = new List<BookEntity>()});
        await _context.Books.AddAsync(new BookEntity() { AuthorId = 1, Isbn = "fdjkgfsg", Pages = 10, Title = "New Book", Price = 100});
        await _context.SaveChangesAsync();

    }

    class InterceptorDesignTimeDbContextFactory : IDesignTimeDbContextFactory<InterceptorDbContext>
    {
        /// <inheritdoc />
        public InterceptorDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<InterceptorDbContext>().UseSqlServer(
                    "Data Source=.;Initial Catalog=Interceptors;Integrated Security=False;User ID=sa;Password=Sql-Server-Dev;Encrypt=True;TrustServerCertificate=True;Application Name=EfInterceptors",
                    o =>
                    {
                        o.MigrationsHistoryTable("MigrationHistory", "SystemData");
                        o.CommandTimeout(3600);
                    }).UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .Options;
            return new InterceptorDbContext(options);
        }
    }
}
/// <summary>
/// The central type which "talks" to the database.
/// </summary>
public class InterceptorDbContext : DbContext
{
    #region constructors and destructors

    public InterceptorDbContext(DbContextOptions options) : base(options)
    {
    }

    #endregion


    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookEntity>(x =>
        {
            x.HasIndex(i => i.Isbn, "UX_BookEntitys_Isbn")
                .IsUnique();
            x.HasIndex(i => i.Title, "IX_BookEntitys_Title");
        });
        modelBuilder.Entity<AuthorEntity>(x =>
        {
            x.HasIndex(i => i.FirstName, "IX_AuthorEntity_FirstName");
        });
    }
    #region properties

    public DbSet<AuthorEntity> Authors { get; set; }
    public DbSet<BookEntity> Books { get; set; }

    #endregion
}