namespace Tarker.Booking.Application.Database.User.Querys.GetUserById
{
    public interface IGetUserByIdQuery
    {
        Task<GetUserByIdModel> Execute(int userId);
    }
}
