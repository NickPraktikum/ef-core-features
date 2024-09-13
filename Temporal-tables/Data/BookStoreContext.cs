namespace TemporalTable.Data
{
    using Microsoft.EntityFrameworkCore;
    using TemporalTable.Configurations;
    using TemporalTable.Interceptors;
    using TemporalTable.Models;

    public class BookStoreContext : DbContext
    {
        #region constructors and destructors

        public BookStoreContext(DbContextOptions options) : base(options)
        {
        }

        #endregion

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new VersionInterceptor(), new SoftDeleteInterceptor());
        }
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
            modelBuilder.ApplyConfiguration(new BookEntityConfiguration());
        }
        #region properties

        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<BookEntity> Books { get; set; }

        #endregion
    }
}
