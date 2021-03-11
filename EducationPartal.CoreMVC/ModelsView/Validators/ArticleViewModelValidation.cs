using EducationPartal.CoreMVC.ModelsView;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EducationPortal.CoreMVC.ModelsView.Validators
{
    public class ArticleViewModelValidation : AbstractValidator<ArticleViewModel>
    {
        public ArticleViewModelValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(100)
                .WithMessage("Incorrect name length. Name length must be from 10 to 100 chars.");

            RuleFor(x => x.PublicationDate)
                .NotEmpty()
                .GreaterThan(new DateTime(1900, 1, 1))
                .LessThan(DateTime.Now)
                .WithMessage("Date time nust be from 1.1.1900 to today date");

            RuleFor(x => x.Site)
                .NotEmpty()
                .Matches(new Regex(@"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$", RegexOptions.IgnoreCase))
                .WithMessage("Incorrect web site");
        }
    }
}
