using Gol.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Gol.Infrastructure
{
    public class GolContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public GolContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Airplane> Airplanes { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airplane>(entity =>
            {
                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.AirplaneModel)
                    .HasColumnName("AirplaneModel")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumberOfPassengers)
                    .HasColumnName("NumberOfPassengers")
                    .HasColumnType("int")
                    .IsUnicode(false);

                entity.Property(e => e.RegistryCreationDate)
                   .HasColumnName("RegistryCreationDate")
                   .HasColumnType("datetime");
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.AirplaneID).HasColumnName("AirplaneID");

                entity.Property(e => e.RegistryCreationDate)
                    .HasColumnName("RegistryCreationDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Airplane)
                    .WithMany(p => p.Passengers)
                    .HasForeignKey(d => d.AirplaneID)
                    .HasConstraintName("FK__Passenger__AIRPLANE_I__1273C1CD");
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetSection("ConnectionStrings:GolDB")?.Value;

            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
        
    }
}

