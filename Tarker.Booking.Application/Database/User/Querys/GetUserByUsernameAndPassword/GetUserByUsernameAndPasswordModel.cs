namespace Tarker.Booking.Application.Database.User.Querys.GetUserByUsernameAndPassword
{
    public class GetUserByUsernameAndPasswordModel
    {
        public required int UserId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
