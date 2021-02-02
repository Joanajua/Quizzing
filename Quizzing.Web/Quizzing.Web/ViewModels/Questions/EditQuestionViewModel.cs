using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quizzing.Web.Models;

namespace Quizzing.Web.ViewModels.Questions
{
    public class EditQuestionViewModel
    {
        //public int QuizId { get; set; }
        public Question Question { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
    }
}
