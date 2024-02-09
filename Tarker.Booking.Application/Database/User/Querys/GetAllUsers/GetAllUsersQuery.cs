using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Tarker.Booking.Application.Database.User.Querys.GetAllUsers
{
    public class GetAllUsersQuery : IGetAllUsersQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetAllUsersQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<List<GetAllUsersModel>> Execute()
        {
            var listEntity = await _databaseService.User.ToListAsync();

            return _mapper.Map<List<GetAllUsersModel>>(listEntity);
        }
    }
}
