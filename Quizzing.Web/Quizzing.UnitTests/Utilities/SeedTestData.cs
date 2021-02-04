using System.Collections.Generic;
using Quizzing.Web.Models;

namespace Quizzing.UnitTests.Utilities
{
    class SeedTestData
    {
        public IEnumerable<Quiz> GetTestQuizzes()
        {
            var quizzes = new List<Quiz>
            {
                new Quiz()
                {
                    QuizId = 1,
                    Title = "Quiz 1",
                },
                new Quiz()
                {
                    QuizId = 2,
                    Title = "Quiz 2",
                }
            };

            return quizzes;
        }

        public IEnumerable<Question> GetTestQuestions()
        {
            var questions = new List<Question>
            {
                new Question()
                {
                    QuestionId = 1,
                    QuestionText = "Quiz1 - Question 1",
                    QuizId = 1
                },
                new Question()
                {
                    QuestionId = 2,
                    QuestionText = "Quiz1 - Question 2",
                    QuizId = 1
                },
                new Question()
                {
                    QuestionId = 3,
                    QuestionText = "Quiz2 - Question 1",
                    QuizId = 2
                },
                new Question()
                {
                    QuestionId = 4,
                    QuestionText = "Quiz2 - Question 2",
                    QuizId = 2
                },
            };

            return questions;
        }

        public IEnumerable<Answer> GetTestAnswers()
        {
            var answers = new List<Answer>
            {
                new Answer()
                {
                    AnswerId = 1,
                    AnswerText = "Quiz1 - Question 1 - Answer 1",
                    QuestionId = 1
                },
                new Answer()
                {
                    AnswerId = 2,
                    AnswerText = "Quiz1 - Question 1 - Answer 2",
                    QuestionId = 1
                },
                new Answer()
                {
                    AnswerId = 3,
                    AnswerText = "Quiz1 - Question 2 - Answer 1",
                    QuestionId = 2
                },
                new Answer()
                {
                    AnswerId = 4,
                    AnswerText = "Quiz1 - Question 2 - Answer 1",
                    QuestionId = 2
                },
                new Answer()
                {
                    AnswerId = 5,
                    AnswerText = "Quiz2 - Question 1 - Answer 1",
                    QuestionId = 3
                },
                new Answer()
                {
                    AnswerId = 6,
                    AnswerText = "Quiz2 - Question 1 - Answer 2",
                    QuestionId = 3
                },
                new Answer()
                {
                    AnswerId = 7,
                    AnswerText = "Quiz2 - Question 2 - Answer 1",
                    QuestionId = 4
                },
                new Answer()
                {
                    AnswerId = 8,
                    AnswerText = "Quiz2 - Question 2 - Answer 2",
                    QuestionId = 4
                },
            };

            return answers;
        }
    }
}
