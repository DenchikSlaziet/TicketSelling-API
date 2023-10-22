using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TicketSelling.Context.Contracts;
using TicketSelling.Context.Contracts.Configuration.Configurations;
using TicketSelling.Context.Contracts.Models;


namespace TicketSelling.Context
{
    public class TicketSellingContext : DbContext, ITicketSellingContext
    {
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Staff> Staffs { get; set; }

        public TicketSellingContext(DbContextOptions<TicketSellingContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CinemaEntityTypeConfiguration).Assembly);
        }
    }
}
