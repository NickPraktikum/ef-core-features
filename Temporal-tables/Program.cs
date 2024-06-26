﻿using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShadowProps.Models;

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
        // Return when the authors were last updated
        var authors = await _context.Authors.ToListAsync();
        foreach (var author in authors)
        {
            _logger.LogInformation($"{author.FirstName} {author.SecondName} was last updated {_context.Entry(author).Property("LastUpdated").CurrentValue}");
        }

        _logger.LogInformation("\v \v");

        //Retrieve authors that were created before a certain date(dateTimeStart).
        var dateTimeStart = new DateTime(2023, 12, 26, 13, 02, 51, 409);
        var dateTimeEnd = new DateTime(9999, 12, 31, 23, 59, 59, 999);
        var authorsHistory = await _context.Authors
        .TemporalFromTo(dateTimeEnd, dateTimeStart)
        .IgnoreAutoIncludes()
        .ToListAsync();
        _logger.LogInformation($"Last authorId is {authorsHistory[authorsHistory.Count-1].AuthorId}");
    }
}

class ShadowPropertiesDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ExperimentDbContext>
{
    /// <inheritdoc />
    public ExperimentDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<ExperimentDbContext>().UseSqlServer(
                 "Data Source=.;Initial Catalog=ShadowPropExperiments;Integrated Security=False;User ID=sa;Password=Sql-Server-Dev;Encrypt=True;TrustServerCertificate=True;Application Name=ShadowPropExperiments",
                o =>
                {
                    o.MigrationsHistoryTable("MigrationHistory", "SystemData");
                    o.CommandTimeout(3600);
                })
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
        modelBuilder.Entity<Author>()
            .Property<DateTime>("LastUpdated");
        modelBuilder.ApplyConfiguration(new AuthorEntityConfiguration());
    }
    #region properties

    public DbSet<Author> Authors { get; set; }


    #endregion
}

internal class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.Navigation(author => author.Books)
            .AutoInclude();
        builder.ToTable("Authors", o => o.IsTemporal());
    }
}