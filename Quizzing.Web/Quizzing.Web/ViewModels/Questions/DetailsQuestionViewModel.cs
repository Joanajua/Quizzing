using System.Collections.Generic;
using Quizzing.Web.Models;

namespace Quizzing.Web.ViewModels.Questions
{
    public class DetailsQuestionViewModel
    {
        public Question Question { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
