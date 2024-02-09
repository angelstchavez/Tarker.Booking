namespace Tarker.Booking.Application.Database.User.Querys.GetUserById
{
    public class GetUserByIdModel
    {
        public required int UserId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
    }
}
