using Tarker.Booking.Domain.Entities.Booking;

namespace Tarker.Booking.Domain.Entities.Customer
{
    public class CustomerEntity
    {
        public int CustomerId { get; set; }
        public required string FullName { get; set; }
        public required string DocumentNumber { get; set; }
        public required ICollection<BookingEntity> Bookings { get; set; }
    }
}