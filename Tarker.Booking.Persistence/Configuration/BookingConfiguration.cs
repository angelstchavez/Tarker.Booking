﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tarker.Booking.Domain.Entities.Booking;

namespace Tarker.Booking.Persistence.Configuration
{
    public class BookingConfiguration
    {
        public BookingConfiguration(EntityTypeBuilder<BookingEntity> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(e => e.BookingId);
            entityTypeBuilder.Property(e => e.RegisterDate).IsRequired();
            entityTypeBuilder.Property(e => e.Code).IsRequired();           
            entityTypeBuilder.Property(e => e.Type).IsRequired();           
            entityTypeBuilder.Property(e => e.UserId).IsRequired();           
            entityTypeBuilder.Property(e => e.CustomerId).IsRequired();
            entityTypeBuilder.HasOne(e => e.User)
                .WithMany(x => x.Bookings).HasForeignKey(e => e.UserId);
            entityTypeBuilder.HasOne(e => e.Customer)
                .WithMany(x => x.Bookings).HasForeignKey(e => e.CustomerId);
        }   
    }
}
