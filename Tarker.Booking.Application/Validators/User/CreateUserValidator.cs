﻿using FluentValidation;
using Tarker.Booking.Application.Database.User.Commands.CreateUser;

namespace Tarker.Booking.Application.Validators.User
{
    public class CreateUserValidator : AbstractValidator<CreateUserModel>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.Username).NotNull().NotEmpty().MaximumLength(20);
            RuleFor(x => x.Password).NotNull().NotEmpty().MaximumLength(10);
        }
    }
}
