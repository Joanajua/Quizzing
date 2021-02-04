using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Quizzing.UnitTests.Utilities;
using Quizzing.Web.Controllers;
using Quizzing.Web.Data;
using Quizzing.Web.Models;
using Quizzing.Web.ViewModels.Quizzes;
using Xunit;

namespace Quizzing.UnitTests
{
    public class QuizzesControllerTest
    {
        private readonly SeedTestData _testData = new SeedTestData();
        private readonly Mock<IQuizRepository> _quizRepository = new Mock<IQuizRepository>();
        private readonly Mock<IQuestionRepository> _questionRepository = new Mock<IQuestionRepository>();

        [Fact]
        public async Task Index_action_result_get_method_should_return_ViewResult_with_correct_Model_type()
        {
                // Arrange
                _quizRepository.Setup(repo => repo.GetAll()).ReturnsAsync(_testData.GetTestQuizzes);

                var controller = new QuizzesController(_quizRepository.Object, _questionRepository.Object);

                // Act
                var result = await controller.Index();

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<Quiz>>(viewResult.ViewData.Model);
                Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Details_action_result_method_should_return_ViewResult_with_correct_Model_type()
        {
            // Arrange
                var id = 1;
                var expectedQuiz = _testData.GetTestQuizzes().FirstOrDefault(q => q.QuizId == id);

                _quizRepository.Setup(repo => repo.GetById(id)).ReturnsAsync(expectedQuiz);

                var controller = new QuizzesController(_quizRepository.Object, _questionRepository.Object);

                // Act
                var result = await controller.Details(id);

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<DetailsQuizViewModel>(viewResult.ViewData.Model);
                Assert.NotNull(result);
                Assert.Equal(expectedQuiz.QuizId, model.Quiz.QuizId);
        }

        [Fact]
        public async Task Details_action_result_method_should_return_BadRequest_when_id_is_null()
        {
            // Arrange
            var controller = new QuizzesController(_quizRepository.Object, _questionRepository.Object);

            // Act
            var result = await controller.Details(null);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            var model = Assert.IsAssignableFrom<BadRequestObjectResult>(badRequestObjectResult);
            Assert.Equal(model.StatusCode, badRequestObjectResult.StatusCode);
            Assert.NotNull(result);
        }
    }
}
