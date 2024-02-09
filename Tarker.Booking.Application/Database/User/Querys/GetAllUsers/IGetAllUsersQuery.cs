namespace Tarker.Booking.Application.Database.User.Querys.GetAllUsers
{
    public interface IGetAllUsersQuery
    {
        Task<List<GetAllUsersModel>> Execute();
    }
}
