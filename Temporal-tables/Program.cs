using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using TemporalTable.Configurations;
using TemporalTable.Data;
using TemporalTable.Interceptors;
using TemporalTable.Models;

// create a hosted app and run it
var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddTransient<MyApp>();
        services.AddDbContext<BookStoreContext>(
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
    private BookStoreContext _context;

    public MyApp(ILogger<MyApp> logger, BookStoreContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task StartAsync()
    {
        var bookHistory = await _context.Books.TemporalAll().ToListAsync();
    }
}
