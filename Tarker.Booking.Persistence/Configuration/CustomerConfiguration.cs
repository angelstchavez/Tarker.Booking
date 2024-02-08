using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tarker.Booking.Domain.Entities.Customer;

namespace Tarker.Booking.Persistence.Configuration
{
    public class CustomerConfiguration
    {
        public CustomerConfiguration(EntityTypeBuilder<CustomerEntity> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(e => e.CustomerId);
            entityTypeBuilder.Property(e => e.FullName).IsRequired();
            entityTypeBuilder.Property(e => e.DocumentNumber).IsRequired();
        }
    }
}
