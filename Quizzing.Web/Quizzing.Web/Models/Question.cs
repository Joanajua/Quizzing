using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzing.Web.Models
{
    public class Question
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }

        public int QuizId { get; set; }

        [DisplayName("Question Text")]
        [MaxLength(150, ErrorMessage = "Title cannot exceed 150 characters.")]
        public string QuestionText { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
