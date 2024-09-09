using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TemporalTable.Models;

// create a hosted app and run it
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
        var author = new AuthorEntity { FirstName = "John", SecondName = "Doe", Age = 25,  };
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
        modelBuilder.Entity<BookEntity>(x =>
        {
            x.HasIndex(i => i.Isbn, "UX_Books_Isbn")
                .IsUnique();
            x.HasIndex(i => i.Title, "IX_Books_Title");

        });
        modelBuilder.Entity<AuthorEntity>(x =>
        {
            x.HasIndex(i => i.FirstName, "IX_Author_FirstName");
        });
        modelBuilder.Entity<BookEntity>()
                        .ToTable(
                            tb => tb.IsTemporal(
                                ttb =>
                                {
                                    ttb.HasPeriodStart("BookCreation");
                                    ttb.HasPeriodEnd("BookRemoval");
                                }));
        modelBuilder.Entity<AuthorEntity>()
            .ToTable(
                tb => tb.IsTemporal(
                    ttb =>
                    {
                        ttb.HasPeriodStart("AuthorCreation");
                        ttb.HasPeriodEnd("AuthorRemoval");
                    }));
        modelBuilder.ApplyConfiguration(new AuthorEntityConfiguration());
    }
    #region properties

    public DbSet<AuthorEntity> Authors { get; set; }


    #endregion
}

internal class AuthorEntityConfiguration : IEntityTypeConfiguration<AuthorEntity>
{
    public void Configure(EntityTypeBuilder<AuthorEntity> builder)
    {
        builder.Navigation(author => author.Books)
            .AutoInclude();
        builder.ToTable("Authors", o => o.IsTemporal());
    }
}