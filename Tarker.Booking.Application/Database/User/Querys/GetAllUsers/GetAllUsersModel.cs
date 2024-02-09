namespace Tarker.Booking.Application.Database.User.Querys.GetAllUsers
{
    public class GetAllUsersModel
    {
        public int UserId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
    }
}
