using Microsoft.AspNetCore.Mvc;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.Features;

namespace Tarker.Booking.Api.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class UserController : Controller
    {
        public UserController()
        {
        }

        [HttpPost]
        public async Task<IActionResult> test()
        {
            var number = int.Parse("Hola");
            return StatusCode(StatusCodes.Status200OK,
                ResponseApiService.Response(StatusCodes.Status200OK, "{}", "Correct test"));
        }
    }
}
