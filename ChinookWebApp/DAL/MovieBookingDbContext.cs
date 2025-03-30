using Microsoft.EntityFrameworkCore;
using ChinookWebApp.Models;

namespace ChinookWebApp.DAL
{
    public class MovieBookingDbContext : DbContext
    {
        public MovieBookingDbContext(DbContextOptions<MovieBookingDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Showtime> Showtimes { get; set; }
        public DbSet<Theater> Theaters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Showtimes)
                .WithOne(s => s.Movie)
                .HasForeignKey(s => s.MovieID);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Tickets)
                .WithOne(t => t.Customer)
                .HasForeignKey(t => t.CustomerID);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Movie)
                .WithMany()
                .HasForeignKey(t => t.MovieID);

            modelBuilder.Entity<Showtime>()
                .HasOne(s => s.Theater)
                .WithMany(t => t.Showtimes)
                .HasForeignKey(s => s.TheaterID);
        }
    }
}
