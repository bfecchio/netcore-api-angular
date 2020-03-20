using FullStack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullStack.Core.Data.Configurations
{
    internal sealed class TicketConfiguration : EntityConfiguration<Ticket>
    {
        #region Overriden Methods

        public override void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.AirlineId).IsRequired();
            builder.Property(p => p.Flight).HasMaxLength(10).IsRequired();
            builder.Property(p => p.Gate).HasMaxLength(10).IsRequired();
            builder.Property(p => p.OriginId).IsRequired();
            builder.Property(p => p.DestinationId).IsRequired();
            builder.Property(p => p.Scheduled).IsRequired();
            builder.Property(p => p.Passenger).HasMaxLength(180).IsRequired();

            builder.HasOne(p => p.Airline)
                .WithMany()
                .HasForeignKey(p => p.AirlineId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Origin)
                .WithMany()
                .HasForeignKey(p => p.OriginId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Destination)
                .WithMany()
                .HasForeignKey(p => p.DestinationId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Creator)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy).OnDelete(DeleteBehavior.Restrict);
        }

        #endregion
    }
}
