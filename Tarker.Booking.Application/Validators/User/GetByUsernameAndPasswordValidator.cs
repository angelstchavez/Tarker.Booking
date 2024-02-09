﻿using FluentValidation;

namespace Tarker.Booking.Application.Validators.User
{
    public class GetByUsernameAndPasswordValidator : AbstractValidator<(string, string)>
    {
        public GetByUsernameAndPasswordValidator()
        {
            RuleFor(x => x.Item1).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.Item2).NotNull().NotEmpty().MaximumLength(50);
        }
    }
}
