using EducationPartal.CoreMVC.ModelsView;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EducationPortal.CoreMVC.ModelsView.Validators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(60)
                .WithMessage("Incorrect name length. Name length must be from 3 to 60 chars.");

            RuleFor(x => x.Email).NotEmpty()
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Incorretct email");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(30)
                .WithMessage("length must be from 4 to 30 chars");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(30)
                .WithMessage("length must be from 4 to 30 chars")
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(new Regex(@"^[0-9]{10}$"))
                .WithMessage("Phone numbers must have only ten digits.");
        }
    }
}
