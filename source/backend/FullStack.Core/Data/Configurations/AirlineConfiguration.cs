using FullStack.Domain.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullStack.Core.Data.Configurations
{
    internal sealed class AirlineConfiguration : EntityConfiguration<Airline>
    {
        #region Overriden Methods

        public override void Configure(EntityTypeBuilder<Airline> builder)
        {
            builder.ToTable("Airlines");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).UseIdentityColumn(seed: 100000, increment: 1000);
            builder.Property(p => p.Name).HasMaxLength(240).IsRequired();
            builder.Property(p => p.ICAO).HasMaxLength(4);
            builder.Property(p => p.IATA).HasMaxLength(4);
            builder.Property(p => p.Image).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Callsign).HasMaxLength(50).IsRequired();
        }

        public override IEnumerable<Airline> Seed()
        {
            return new Airline[]
            {
                new Airline {Id = 100000, Name = "ASTA Linhas Aéreas",               ICAO = "",      IATA = "",      Image = "airline_001.png", Callsign = "SUL AMÉRICA"},
                new Airline {Id = 101000, Name = "Azul Brazilian Airlines",          ICAO = "AD",    IATA = "AZU",   Image = "airline_002.png", Callsign = "AZUL"},
                new Airline {Id = 102000, Name = "GOL Linhas Aéreas Inteligentes",   ICAO = "G3",    IATA = "GLO",   Image = "airline_003.png", Callsign = "GOL"},
                new Airline {Id = 103000, Name = "MAP Linhas Aéreas",                ICAO = "",      IATA = "PAM",   Image = "airline_004.png", Callsign = "MAP AIR"},
                new Airline {Id = 104000, Name = "Passaredo Linhas Aéreas",          ICAO = "P3",    IATA = "PTB",   Image = "airline_005.png", Callsign = "PASSAREDO"},
                new Airline {Id = 105000, Name = "Piquiatuba Transportes Aéreos",    ICAO = "",      IATA = "",      Image = "airline_006.png", Callsign = "PIQUIATUBA"},
                new Airline {Id = 106000, Name = "Sideral",                          ICAO = "",      IATA = "SID",   Image = "airline_007.png", Callsign = "SIDERAL"},
                new Airline {Id = 107000, Name = "LATAM Airlines",                   ICAO = "JJ",    IATA = "TAM",   Image = "airline_008.png", Callsign = "TAM"}
            };
        }

        #endregion
    }
}
