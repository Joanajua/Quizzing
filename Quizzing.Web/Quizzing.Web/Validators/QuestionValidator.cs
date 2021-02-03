using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Quizzing.Web.Models;

namespace Quizzing.Web.Validators
{
    public class QuestionValidator : AbstractValidator<Question>
    {
        public QuestionValidator()
        {
            // Validation for 1 question - A white space counts as empty
            // and a max length of 150
            RuleFor(q => q.QuestionText)
                .NotEmpty()
                .MaximumLength(150);
        }
    }
}
