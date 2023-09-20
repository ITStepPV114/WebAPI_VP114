using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using FluentValidation;

namespace Core.Validators
{
    public class MovieValidators : AbstractValidator<Movie>
    {
        public MovieValidators()
        {
            RuleFor(m => m.Title).NotEmpty()
                .MaximumLength(500)
                .MinimumLength(50);

            RuleFor(m => m.Year).NotEmpty()
                .LessThanOrEqualTo(DateTime.Now.Year)
                .GreaterThanOrEqualTo(0)
                .WithMessage($"Year must be less then {DateTime.Now.Year} or greater then 0");
        }
    }
}
