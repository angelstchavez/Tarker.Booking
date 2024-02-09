using AutoMapper;
using Tarker.Booking.Application.Database.User.Commands.CreateUser;
using Tarker.Booking.Application.Database.User.Commands.UpdateUser;
using Tarker.Booking.Application.Database.User.Querys.GetAllUsers;
using Tarker.Booking.Application.Database.User.Querys.GetUserById;
using Tarker.Booking.Domain.Entities.User;

namespace Tarker.Booking.Application.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserEntity, CreateUserModel>().ReverseMap();
            CreateMap<UserEntity, UpdateUserModel>().ReverseMap();
            CreateMap<UserEntity, GetAllUsersModel>().ReverseMap();
            CreateMap<UserEntity, GetUserByIdModel>().ReverseMap();
        }
    }
}
