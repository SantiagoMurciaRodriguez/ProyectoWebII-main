using Microsoft.EntityFrameworkCore;
using ProyectoAerolineaWeb.Models;
using ProyectoAerolineaWeb.Views.Vuelos;

namespace ProyectoAerolineaWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Vuelo> Vuelos { get; set; }
        public DbSet<Tarifa> Tarifas { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Pasajeros> Pasajeros { get; set; }
        public DbSet<Servicios> Servicios { get; set; }
        public DbSet<ConfirmacionReserva> ConfirmacionesReserva { get; set; }
        public DbSet<DetallePasajero> DetallesPasajero { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vuelo>()
                .HasOne(v => v.CiudadOrigen)
                .WithMany()
                .HasForeignKey(v => v.CiudadOrigenId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vuelo>()
                .HasOne(v => v.CiudadDestino)
                .WithMany()
                .HasForeignKey(v => v.CiudadDestinoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tarifa>()
                .Property(t => t.Precio)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Pasajeros>()
                .HasKey(p => new { p.Id });
        }

        public void Seed()
        {
            if (!Ciudades.Any())
            {
                Ciudades.AddRange(
                    new Ciudad { Nombre = "Ciudad A" },
                    new Ciudad { Nombre = "Ciudad B" },
                    new Ciudad { Nombre = "Ciudad C" }
                );
                SaveChanges();
            }

            if (!Vuelos.Any())
            {
                Vuelos.AddRange(
                    new Vuelo
                    {
                        NumeroVuelo = "A123",
                        CiudadOrigenId = Ciudades.First(c => c.Nombre == "Ciudad A").Id,
                        CiudadDestinoId = Ciudades.First(c => c.Nombre == "Ciudad B").Id,
                        Fecha = DateTime.Now.AddDays(1),
                        AsientosDisponibles = 100
                    },
                    new Vuelo
                    {
                        NumeroVuelo = "B456",
                        CiudadOrigenId = Ciudades.First(c => c.Nombre == "Ciudad B").Id,
                        CiudadDestinoId = Ciudades.First(c => c.Nombre == "Ciudad C").Id,
                        Fecha = DateTime.Now.AddDays(2),
                        AsientosDisponibles = 100
                    },
                    new Vuelo
                    {
                        NumeroVuelo = "C789",
                        CiudadOrigenId = Ciudades.First(c => c.Nombre == "Ciudad C").Id,
                        CiudadDestinoId = Ciudades.First(c => c.Nombre == "Ciudad A").Id,
                        Fecha = DateTime.Now.AddDays(3),
                        AsientosDisponibles = 0
                    },
                    new Vuelo
                    {
                        NumeroVuelo = "D001",
                        CiudadOrigenId = Ciudades.First(c => c.Nombre == "Ciudad A").Id,
                        CiudadDestinoId = Ciudades.First(c => c.Nombre == "Ciudad C").Id,
                        Fecha = DateTime.Now.AddDays(4),
                        AsientosDisponibles = 0
                    },
                    new Vuelo
                    {
                        NumeroVuelo = "E002",
                        CiudadOrigenId = Ciudades.First(c => c.Nombre == "Ciudad B").Id,
                        CiudadDestinoId = Ciudades.First(c => c.Nombre == "Ciudad A").Id,
                        Fecha = DateTime.Now.AddDays(5),
                        AsientosDisponibles = 0
                    },
                    new Vuelo
                    {
                        NumeroVuelo = "F003",
                        CiudadOrigenId = Ciudades.First(c => c.Nombre == "Ciudad C").Id,
                        CiudadDestinoId = Ciudades.First(c => c.Nombre == "Ciudad B").Id,
                        Fecha = DateTime.Now.AddDays(6),
                        AsientosDisponibles = 0
                    }
                );
                SaveChanges();
            }

            if (!Tarifas.Any())
            {
                Tarifas.AddRange(
                    new Tarifa
                    {
                        Nombre = "Económica",
                        Precio = 100,
                        VueloId = Vuelos.First(v => v.NumeroVuelo == "A123").Id
                    },
                    new Tarifa
                    {
                        Nombre = "Business",
                        Precio = 200,
                        VueloId = Vuelos.First(v => v.NumeroVuelo == "A123").Id
                    },
                    new Tarifa
                    {
                        Nombre = "Primera Clase",
                        Precio = 300,
                        VueloId = Vuelos.First(v => v.NumeroVuelo == "B456").Id
                    }
                );
                SaveChanges();
            }

            if (!Users.Any())
            {
                Users.Add(new User
                {
                    Name = "Admin",
                    Email = "admin@vuela.com",
                    Password = "admin123"
                });
                SaveChanges();
            }
        }
    }
}


