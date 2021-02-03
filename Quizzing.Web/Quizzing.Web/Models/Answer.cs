using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzing.Web.Models
{
    public class Answer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }

        [DisplayName("Answer Text")]
        public string AnswerText { get; set; }

        [DisplayName("Correct answer")]
        [DefaultValue(false)]
        public bool IsCorrect { get; set; }
    }
}
