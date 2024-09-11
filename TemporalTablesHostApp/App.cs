using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TemporalTablesHostApp.Data;

namespace TemporalTablesHostApp
{
    /// <summary>
    /// Contains the application code for the console app.
    /// </summary>
    public class App
    {
        private readonly ILogger<App> _logger;
        private readonly BookContext _context;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="logger">The logger to use.</param>
        public App(ILogger<App> logger, BookContext bookContext)
        {
            _logger = logger;
            _context = bookContext;
        }

        /// <summary>
        /// Represents the program logic of the console app.
        /// </summary>
        /// <param name="args">The command line arguments passed to the app at startup.</param>
        /// <returns>0 if the app ran succesfully otherwise 1.</returns>
        public async Task<int> StartAsync(string[] args)
        {
            using (var context = _context) {
                var todo = await context.Todos.FirstOrDefaultAsync(todo => todo.Id == 1);
                context.Todos.Remove(todo!);
                await context.SaveChangesAsync();
            }
            return 0;
        }
    }
}