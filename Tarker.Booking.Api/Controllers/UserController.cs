﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Tarker.Booking.Application.Database.User.Commands.CreateUser;
using Tarker.Booking.Application.Database.User.Commands.DeleteUser;
using Tarker.Booking.Application.Database.User.Commands.UpdateUser;
using Tarker.Booking.Application.Database.User.Commands.UpdateUserPassword;
using Tarker.Booking.Application.Database.User.Querys.GetAllUsers;
using Tarker.Booking.Application.Database.User.Querys.GetUserById;
using Tarker.Booking.Application.Database.User.Querys.GetUserByUsernameAndPassword;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.Features;

namespace Tarker.Booking.Api.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class UserController : Controller
    {
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

        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdateUserPasswordModel model,
            [FromServices] IUpdateUserPasswordCommand updateUserPassowordCommand,
            [FromServices] IValidator<UpdateUserPasswordModel> validator)
        {
            var validate = await validator.ValidateAsync(model);

            if (validate.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    ResponseApiService.Response(StatusCodes.Status400BadRequest, validate.Errors));
            }

            var data = await updateUserPassowordCommand.Execute(model);

            return StatusCode(StatusCodes.Status200OK,
                ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

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

        [HttpGet("get-by-username-password/{username}/{password}")]
        public async Task<IActionResult> GetByUsernameAndPassword(string username, string password,
            [FromServices] IGetUserByUsernameAndPasswordQuery getUserByUsernameAndPasswordQuery,
            [FromServices] IValidator<(string, string)> validator)
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

                return StatusCode(StatusCodes.Status200OK,
                    ResponseApiService.Response(StatusCodes.Status200OK, data));
            }
        }
    }
}