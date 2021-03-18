using EducationPartal.WebApi.ModelsView;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.WebApi.ModelsView.Validators
{
    public class BookViewModelValidation : AbstractValidator<BookViewModel>
    {
        public BookViewModelValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(100)
                .WithMessage("Incorrect name length. Name length must be from 2 to 100 chars.");

            RuleFor(x => x.CountOfPages)
                .NotEmpty()
                .LessThan(3000)
                .GreaterThan(178)
                .WithMessage("Incorrect count of pages. Count must be from 178 to 3000 pages.");

            RuleFor(x => x.Author)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100)
                .WithMessage("Incorrect author name length. Name length must be from 2 to 100 chars.");
        }
    }
}
