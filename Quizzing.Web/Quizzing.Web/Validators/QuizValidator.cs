using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Quizzing.Web.Data;
using Quizzing.Web.Models;

namespace Quizzing.Web.Validators
{
    public class QuizValidator : AbstractValidator<Quiz>
    {
        public QuizValidator(AppDbContext context)
        {
            // Validation for 1 quiz - A white space counts as empty
            // and a max length of 100
            RuleFor(c => c.Title)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
