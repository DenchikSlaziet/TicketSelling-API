using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TicketSelling.Context.Contracts.Configuration.Configurations;
using TicketSelling.Context.Contracts.Interfaces;


namespace TicketSelling.Context
{
    public class TicketSellingContext : DbContext, IReader, IWriter, IUnitOfWork
    {
        public TicketSellingContext(DbContextOptions<TicketSellingContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CinemaEntityTypeConfiguration).Assembly);
        }

        IQueryable<TEntity> IReader.Read<TEntity>()
            => base.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

        void IWriter.Add<TEntity>([NotNull] TEntity entity)
            => base.Entry(entity).State = EntityState.Added;

        void IWriter.Update<TEntity>([NotNull] TEntity entity)
            => base.Entry(entity).State = EntityState.Modified;

        void IWriter.Delete<TEntity>([NotNull] TEntity entity)
            => base.Entry(entity).State = EntityState.Deleted;

        async void IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            await base.SaveChangesAsync(cancellationToken);
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
