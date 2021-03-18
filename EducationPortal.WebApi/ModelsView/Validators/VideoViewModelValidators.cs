using EducationPartal.WebApi.ModelsView;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EducationPortal.WebApi.ModelsView.Validators
{
    public class VideoViewModelValidators : AbstractValidator<VideoViewModel>
    {
        public VideoViewModelValidators()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100)
                .WithMessage("Incorrect name length. Name length must be from 2 to 100 chars.");

            RuleFor(x => x.Quality)
                .NotEmpty();

            RuleFor(x => x.Link)
                .NotEmpty()
                .Matches(new Regex(@"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$", RegexOptions.IgnoreCase))
                .WithMessage("Incorrect web site");

        }
    }
}
