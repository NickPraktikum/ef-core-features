using Interceptors.Configurations;
using Interceptors.Entities;
using Interceptors.Interceptors;
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
        services.AddSingleton<MyApp>();
        services.AddDbContext<InterceptorDbContext>(
            (services, options) =>
            {
                var config = services.GetRequiredService<IConfiguration>();
                var connectionString = config.GetConnectionString("Interceptor");
                options.UseSqlServer(
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
        var book = new BookEntity { Isbn = "12345abcdef", Title = "Ivanhoe: A Romance", Pages = 458, Price = 12.56f, AuthorId=2 };
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        var books = await _context.Books.ToListAsync();
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
        optionsBuilder.AddInterceptors(new VersionInterceptor());
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
        modelBuilder.ApplyConfiguration(new BookEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AuthorEntityConfiguration());
    }
    #region properties

    public DbSet<AuthorEntity> Authors { get; set; }
    public DbSet<BookEntity> Books { get; set; }

    #endregion
}