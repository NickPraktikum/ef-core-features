namespace Interceptors.Interceptors
{
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.EntityFrameworkCore;
    using global::Interceptors.Interfaces;

    /// <summary>
    /// The interceptor that provides the version change functionality on entities.
    /// </summary>
    public class VersionInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
       DbContextEventData eventData,
       InterceptionResult<int> result)
        {
            var modifiedEntries = eventData.Context!.ChangeTracker.Entries()
                .Where(entry => entry.State == EntityState.Modified | entry.State == EntityState.Deleted);

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is IVersion versionedEntity)
                {
                    versionedEntity.Version++;
                }
            }
            return result;
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            SavingChanges(eventData, result);
            return ValueTask.FromResult(result);
        }
    }
}
