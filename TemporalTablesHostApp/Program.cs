using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using TemporalTablesHostApp;
using TemporalTablesHostApp.Data;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        // TODO add your service dependencies here
        services.AddSingleton<App>();
        services.AddDbContext<BookContext>(
            (s, b) =>
            {
                var config = s.GetRequiredService<IConfiguration>();
                var connectionString = config.GetConnectionString("BookDb");
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
return await app.Services.GetRequiredService<App>().StartAsync(Environment.GetCommandLineArgs());