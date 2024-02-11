using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tarker.Booking.Application.Database.User.Commands.CreateUser;
using Tarker.Booking.Application.Database.User.Commands.DeleteUser;
using Tarker.Booking.Application.Database.User.Commands.UpdateUser;
using Tarker.Booking.Application.Database.User.Commands.UpdateUserPassword;
using Tarker.Booking.Application.Database.User.Querys.GetAllUsers;
using Tarker.Booking.Application.Database.User.Querys.GetUserById;
using Tarker.Booking.Application.Database.User.Querys.GetUserByUsernameAndPassword;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.External.GetTokenJWT;
using Tarker.Booking.Application.Features;

namespace Tarker.Booking.Api.Controllers
{
    /// <summary>
    /// Controller for managing user-related actions.
    /// </summary>
    [Authorize]
    [Route("api/v1/user")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class UserController : Controller
    {
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="model">The model containing user data.</param>
        /// <param name="createUserCommand">The command for creating a user.</param>
        /// <param name="validator">The validator for the user creation model.</param>
        [HttpPost("create")]
        public async Task<IActionResult> Create(
            [FromBody] CreateUserModel model,
            [FromServices] ICreateUserCommand createUserCommand,
            [FromServices] IValidator<CreateUserModel> validator
            )
        {
            var validate = await validator.ValidateAsync(model);

            if (validate.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    ResponseApiService.Response(StatusCodes.Status400BadRequest, validate.Errors));
            }

            var data = await createUserCommand.Execute(model);

            return StatusCode(StatusCodes.Status201Created,
                ResponseApiService.Response(StatusCodes.Status201Created, data));
        }

        /// <summary>
        /// Updates user information.
        /// </summary>
        /// <param name="model">The model containing updated user data.</param>
        /// <param name="updateUserCommand">The command for updating a user.</param>
        /// <param name="validator">The validator for the user update model.</param>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserModel model,
            [FromServices] IUpdateUserCommand updateUserCommand,
            [FromServices] IValidator<UpdateUserModel> validator)
        {
            var validate = await validator.ValidateAsync(model);

            if (validate.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    ResponseApiService.Response(StatusCodes.Status400BadRequest, validate.Errors));
            }

            var data = await updateUserCommand.Execute(model);

            return StatusCode(StatusCodes.Status200OK,
                ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        /// <summary>
        /// Updates user password.
        /// </summary>
        /// <param name="model">The model containing updated user password data.</param>
        /// <param name="updateUserPasswordCommand">The command for updating user password.</param>
        /// <param name="validator">The validator for the user password update model.</param>
        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdateUserPasswordModel model,
            [FromServices] IUpdateUserPasswordCommand updateUserPasswordCommand,
            [FromServices] IValidator<UpdateUserPasswordModel> validator)
        {
            var validate = await validator.ValidateAsync(model);

            if (validate.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    ResponseApiService.Response(StatusCodes.Status400BadRequest, validate.Errors));
            }

            var data = await updateUserPasswordCommand.Execute(model);

            return StatusCode(StatusCodes.Status200OK,
                ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <param name="deleteUserCommand">The command for deleting a user.</param>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int userId,
            [FromServices] IDeleteUserCommand deleteUserCommand)
        {
            if (userId == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    ResponseApiService.Response(StatusCodes.Status400BadRequest));
            }

            var data = await deleteUserCommand.Execute(userId);

            if (!data)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    ResponseApiService.Response(StatusCodes.Status404NotFound, data));
            }

            return StatusCode(StatusCodes.Status200OK,
                ResponseApiService.Response(StatusCodes.Status200OK));
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <param name="getAllUsersQuery">The query for retrieving all users.</param>
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromServices] IGetAllUsersQuery getAllUsersQuery)
        {
            var data = await getAllUsersQuery.Execute();

            if (data == null || data.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    ResponseApiService.Response(StatusCodes.Status404NotFound));
            }

            return StatusCode(StatusCodes.Status200OK,
                ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <param name="getUserByIdQuery">The query for retrieving a user by ID.</param>
        [HttpGet("get-by-id/{userId}")]
        public async Task<IActionResult> GetById(int userId,
            [FromServices] IGetUserByIdQuery getUserByIdQuery)
        {
            if (userId == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    ResponseApiService.Response(StatusCodes.Status400BadRequest));
            }

            var data = await getUserByIdQuery.Execute(userId);

            if (data == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    ResponseApiService.Response(StatusCodes.Status404NotFound));
            }

            return StatusCode(StatusCodes.Status200OK,
                ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        /// <summary>
        /// Retrieves a user by username and password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="getUserByUsernameAndPasswordQuery">The query for retrieving a user by username and password.</param>
        /// <param name="validator">The validator for the username and password.</param>
        /// <param name="getTokenJWTService">The service for generating JWT tokens.</param>
        [AllowAnonymous]
        [HttpGet("get-by-username-password/{username}/{password}")]
        public async Task<IActionResult> GetByUsernameAndPassword(string username, string password,
            [FromServices] IGetUserByUsernameAndPasswordQuery getUserByUsernameAndPasswordQuery,
            [FromServices] IValidator<(string, string)> validator,
            [FromServices] IGetTokenJWTService getTokenJWTService)
        {
            {
                var validate = await validator.ValidateAsync((username, password));

                if (validate.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        ResponseApiService.Response(StatusCodes.Status400BadRequest, validate.Errors));
                }

                var data = await getUserByUsernameAndPasswordQuery.Execute(username, password);

                if (data == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                        ResponseApiService.Response(StatusCodes.Status404NotFound));
                }

                data.Token = getTokenJWTService.Execute(data.UserId.ToString());

                return StatusCode(StatusCodes.Status200OK,
                    ResponseApiService.Response(StatusCodes.Status200OK, data));
            }
        }
    }
}