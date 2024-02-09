namespace Tarker.Booking.Application.Database.User.Querys.GetUserByUsernameAndPassword
{
    public interface IGetUserByUsernameAndPasswordQuery
    {
        Task<GetUserByUsernameAndPasswordModel> Execute(string username, string password);
    }
}
