namespace TemporalTablesHostApp.Data
{
    using Microsoft.EntityFrameworkCore;
    using TemporalTablesHostApp.Models;

    public class BookContext : DbContext
    {
        #region constructors and destructors

        public BookContext(DbContextOptions options) : base(options)
        {
        }

        #endregion

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoModel>()
                            .ToTable(
                                tb => tb.IsTemporal(
                                    ttb =>
                                    {
                                        ttb.HasPeriodStart("BookCreation");
                                        ttb.HasPeriodEnd("BookRemoval");
                                    })).HasData(new TodoModel { Id = 1, Todo = "Do the grocery shopping"}, new TodoModel { Id = 2, Todo = "Do the ironing"});
        }
        #region properties
        public DbSet<TodoModel> Todos { get; set; }

        #endregion
    }
}