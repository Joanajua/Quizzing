using Microsoft.EntityFrameworkCore;
using Quizzing.Web.Models;

namespace Quizzing.Web.Data
{
    public static class SeedData
    {
        private static readonly Quiz[] _mockQuizzes = new[]
        {
            new Quiz {QuizId = 1, Title = "Quiz 1"},
            new Quiz {QuizId = 2, Title = "Quiz 2"},
            new Quiz {QuizId = 3, Title = "Quiz 3"},
        };

        private static readonly Question[] _mockQuestions = new[]
        {
            new Question {QuestionId = 1, QuizId = 1, QuestionText = "Question 1"},
            new Question {QuestionId = 2, QuizId = 1, QuestionText = "Question 2"},
            new Question {QuestionId = 3, QuizId = 1, QuestionText = "Question 3"},

            new Question {QuestionId = 4, QuizId = 2, QuestionText = "Question 1"},
            new Question {QuestionId = 5, QuizId = 2, QuestionText = "Question 2"},
            new Question {QuestionId = 6, QuizId = 2, QuestionText = "Question 3"},

            new Question {QuestionId = 7, QuizId = 3, QuestionText = "Question 1"},
            new Question {QuestionId = 8, QuizId = 3, QuestionText = "Question 2"},
            new Question {QuestionId = 9, QuizId = 3, QuestionText = "Question 3"},
        };

        private static readonly Answer[] _mockAnswers = new[]
        {
            new Answer {AnswerId = 1,  QuestionId = 1, AnswerText = "Answer 1"},
            new Answer {AnswerId = 2,  QuestionId = 1, AnswerText = "Answer 2"},
            new Answer {AnswerId = 3,  QuestionId = 1, AnswerText = "Answer 3"},
            new Answer {AnswerId = 4,  QuestionId = 1, AnswerText = "Answer 4"},

            new Answer {AnswerId = 5,  QuestionId = 2, AnswerText = "Answer 1"},
            new Answer {AnswerId = 6,  QuestionId = 2, AnswerText = "Answer 2"},
            new Answer {AnswerId = 7,  QuestionId = 2, AnswerText = "Answer 3"},
            new Answer {AnswerId = 8,  QuestionId = 2, AnswerText = "Answer 4"},

            new Answer {AnswerId = 9,  QuestionId = 3, AnswerText = "Answer 1"},
            new Answer {AnswerId = 10,  QuestionId = 3, AnswerText = "Answer 2"},
            new Answer {AnswerId = 11,  QuestionId = 3, AnswerText = "Answer 3"},
            new Answer {AnswerId = 12,  QuestionId = 3, AnswerText = "Answer 4"},

            new Answer {AnswerId = 13,  QuestionId = 4, AnswerText = "Answer 1"},
            new Answer {AnswerId = 14,  QuestionId = 4, AnswerText = "Answer 2"},
            new Answer {AnswerId = 15,  QuestionId = 4, AnswerText = "Answer 3"},
            new Answer {AnswerId = 16,  QuestionId = 4, AnswerText = "Answer 4"},

            new Answer {AnswerId = 17,  QuestionId = 5, AnswerText = "Answer 1"},
            new Answer {AnswerId = 18,  QuestionId = 5, AnswerText = "Answer 2"},
            new Answer {AnswerId = 19,  QuestionId = 5, AnswerText = "Answer 3"},
            new Answer {AnswerId = 20,  QuestionId = 5, AnswerText = "Answer 4"},

            new Answer {AnswerId = 21,  QuestionId = 6, AnswerText = "Answer 1"},
            new Answer {AnswerId = 22,  QuestionId = 6, AnswerText = "Answer 2"},
            new Answer {AnswerId = 23,  QuestionId = 6, AnswerText = "Answer 3"},
            new Answer {AnswerId = 24,  QuestionId = 6, AnswerText = "Answer 4"},

            new Answer {AnswerId = 25,  QuestionId = 7, AnswerText = "Answer 1"},
            new Answer {AnswerId = 26,  QuestionId = 7, AnswerText = "Answer 2"},
            new Answer {AnswerId = 27,  QuestionId = 7, AnswerText = "Answer 3"},
            new Answer {AnswerId = 28,  QuestionId = 7, AnswerText = "Answer 4"},

            new Answer {AnswerId = 29,  QuestionId = 8, AnswerText = "Answer 1"},
            new Answer {AnswerId = 30,  QuestionId = 8, AnswerText = "Answer 2"},
            new Answer {AnswerId = 31,  QuestionId = 8, AnswerText = "Answer 3"},
            new Answer {AnswerId = 32,  QuestionId = 8, AnswerText = "Answer 4"},

            new Answer {AnswerId = 33,  QuestionId = 9, AnswerText = "Answer 1"},
            new Answer {AnswerId = 34,  QuestionId = 9, AnswerText = "Answer 2"},
            new Answer {AnswerId = 35,  QuestionId = 9, AnswerText = "Answer 3"},
            new Answer {AnswerId = 36,  QuestionId = 9, AnswerText = "Answer 4"}
        };

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quiz>().HasData(_mockQuizzes);
            modelBuilder.Entity<Question>().HasData(_mockQuestions);
            modelBuilder.Entity<Answer>().HasData(_mockAnswers);
        }
    }
}
