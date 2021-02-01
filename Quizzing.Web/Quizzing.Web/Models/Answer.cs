﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        //[Required(ErrorMessage = "Please enter an answer.")]
        [DisplayName("Answer")]
        //[MaxLength(150, ErrorMessage = "Answer cannot exceed 150 characters.")]
        public string AnswerText { get; set; }

        [DisplayName("Correct answer")]
        [DefaultValue(false)]
        public bool IsCorrect { get; set; }
    }
}
