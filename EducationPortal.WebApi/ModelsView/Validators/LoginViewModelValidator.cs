using EducationPartal.WebApi.ModelsView;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.WebApi.ModelsView.Validators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(60)
                .WithMessage("Incorrect name length. Name length must be from 3 to 60 chars.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(30)
                .WithMessage("length must be from 4 to 30 chars");
        }
    }
}
