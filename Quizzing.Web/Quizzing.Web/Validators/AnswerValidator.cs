using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Quizzing.Web.Models;

namespace Quizzing.Web.Validators
{
    public class AnswerValidator : AbstractValidator<Answer>
    {
        public AnswerValidator()
        {
            RuleFor(q => q.AnswerText)
                .NotEmpty()
                .MaximumLength(150);
        }
    }
}
