using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzing.Web.Models
{
    public class Quiz
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuizId { get; set; }

        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        public List<Question> Questions { get; set; }
    }
}
