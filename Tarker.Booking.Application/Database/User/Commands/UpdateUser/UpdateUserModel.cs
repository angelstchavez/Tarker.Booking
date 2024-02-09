namespace Tarker.Booking.Application.Database.User.Commands.UpdateUser
{
    public class UpdateUserModel
    {
        public int UserId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
