using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Login.source;
using TUI.Places.Source;
using TUI.Report.Source;
using TUI.Transportations.Air;
using TUI.Transportations.Air.Source;

namespace TUI.Ado.Entity.Source
{
    public class TuiContext: DbContext
    {
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<PlaneKind> PlaneKinds { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<HistoryLine> History { get; set; }

        public TuiContext() : base()
        {
        }

        public TuiContext(string connection) : base(connection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airport>().HasOptional(c => c.City);
            modelBuilder.Entity<Flight>().Ignore(b => b.Arrival);
            modelBuilder.Entity<Flight>().Ignore(b => b.Departure);

            modelBuilder.Entity<Plane>().HasRequired(c => c.GasKind);

            modelBuilder.Entity<Flight>()   
                .HasRequired(c => c.DepartureAirport)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Flight>()
                .HasRequired(c => c.ArrivalAirport)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Properties<DateTime>()
                .Configure(c => c.HasColumnType("datetime2"));
        }

    }
}
