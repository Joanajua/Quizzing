using System.Collections.Generic;
using Quizzing.Web.Models;

namespace Quizzing.Web.ViewModels.Questions
{
    public class DetailsQuestionViewModel
    {
        public Question Question { get; set; }

        public IEnumerable<Answer> Answers { get; set; }
    }
}
