using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quizzing.Web.Models;

namespace Quizzing.Web.ViewModels.Quizzes
{
    public class EditQuizViewModel
    {
        public Quiz Quiz { get; set; }
        public IEnumerable<Question> Questions { get; set; }
    }
}
