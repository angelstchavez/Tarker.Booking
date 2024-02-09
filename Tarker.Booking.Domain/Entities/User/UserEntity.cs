using Tarker.Booking.Domain.Entities.Booking;

namespace Tarker.Booking.Domain.Entities.User
{
    public class UserEntity
    {
        public int UserId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public ICollection<BookingEntity>? Bookings { get; set; }
    }
}
