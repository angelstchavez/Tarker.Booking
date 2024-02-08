using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tarker.Booking.Domain.Entities.User;

namespace Tarker.Booking.Persistence.Configuration
{
    public class UserConfiguration
    {
        public UserConfiguration(EntityTypeBuilder<UserEntity> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.UserId);
            entityTypeBuilder.Property(x => x.FirstName).IsRequired();
            entityTypeBuilder.Property(x => x.LastName).IsRequired();
            entityTypeBuilder.Property(x => x.Username).IsRequired();
            entityTypeBuilder.Property(x => x.Password).IsRequired();
        }
    }
}
