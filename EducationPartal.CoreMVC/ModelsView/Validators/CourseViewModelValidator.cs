using EducationPartal.CoreMVC.ModelsView;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.CoreMVC.ModelsView.Validators
{
    public class CourseViewModelValidator : AbstractValidator<CourseViewModel>
    {
        public CourseViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100)
                .WithMessage("Incorrect name length. Name length must be from 2 to 100 chars.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100)
                .WithMessage("Incorrect description length. Name length must be from 5 to 100 chars.");
        }
    }
}
