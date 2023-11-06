using Microsoft.EntityFrameworkCore;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts;
using TicketSelling.Context.Contracts.Configuration.Configurations;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Context
{
    public class TicketSellingContext : DbContext, ITicketSellingContext, IDbRead, IDbWriter, IUnitOfWork
    {
        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Film> Films { get; set; }

        public DbSet<Hall> Halls { get; set; }

        public DbSet<Staff> Staffs { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public TicketSellingContext(DbContextOptions<TicketSellingContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CinemaEntityTypeConfiguration).Assembly);
        }

        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var count = await base.SaveChangesAsync(cancellationToken);
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
            return count;
        }

        IQueryable<TEntity> IDbRead.Read<TEntity>()
            => base.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

        void IDbWriter.Add<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Added;

        void IDbWriter.Update<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Modified;

        void IDbWriter.Delete<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Deleted;
    }
}
