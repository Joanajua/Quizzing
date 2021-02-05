using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
        public async Task Index_action_result_method_should_return_an_empty_list_when_there_are_no_quizzes()
        {
            // Arrange
            var emptyList = new List<Quiz>();
            _quizRepository.Setup(repo => repo.GetAll()).ReturnsAsync(emptyList);
            
            var controller = new QuizzesController(_quizRepository.Object, _questionRepository.Object);

            // Act
            var result = await controller.Index();

            // Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<IEnumerable<Quiz>>(viewResult.ViewData.Model);
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
        public async Task Details_action_result_method_should_return_BadRequest_when_quiz_id_is_null()
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

        [Fact]
        public async Task Details_action_result_method_should_return_NotFound_when_no_quiz_found_for_id()
        {
            // Arrange
            var id = 0;
            var expectedQuiz = _testData.GetTestQuizzes().FirstOrDefault(q => q.QuizId == id);

            _quizRepository.Setup(repo => repo.GetById(id)).ReturnsAsync(expectedQuiz);
            var controller = new QuizzesController(_quizRepository.Object, _questionRepository.Object);

            // Act
            var result = await controller.Details(id);

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            var model = Assert.IsAssignableFrom<NotFoundObjectResult>(notFoundObjectResult);
            Assert.Equal(model.StatusCode, notFoundObjectResult.StatusCode);
            Assert.Equal(model.Value, notFoundObjectResult.Value);
            Assert.NotNull(result);
            
        }

        [Fact]
        public void Create_action_should_return_Create_View_with_correct_model()
        {
            // Arrange
            var controller = new QuizzesController(_quizRepository.Object, _questionRepository.Object);

            // act
            var result = controller.Create();

            // assert
            Assert.IsType<ViewResult>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_post_action_result_should_redirect_to_action_when_modelState_valid()
        {
            // Arrange
            var quiz = new Quiz
            {
                QuizId = 3,
                Title = "Test"
            };

            var httpContext = new DefaultHttpContext();

            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var controller = new QuizzesController(_quizRepository.Object, _questionRepository.Object)
            {
                TempData = tempData
            };

            // Act
            var result = await controller.Create(quiz);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Create_post_action_result_should_return_ViewResult_with_correct_Model_type_when_modelState_not_valid()
        {
            // Arrange
            var quiz = new Quiz
            {
                QuizId = 3
            };
            
            // Arrange
            var controller = new QuizzesController(_quizRepository.Object, _questionRepository.Object);

            controller.ModelState.AddModelError("Title", "Title is required");

            // Act
            var result = await controller.Create(quiz);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<Quiz>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Edit_action_result_method_should_return_ViewResult_with_correct_Model_type()
        {
            // Arrange
            var id = 1;

            var expectedQuiz = _testData.GetTestQuizzes().FirstOrDefault(q => q.QuizId == id);
            _quizRepository.Setup(repo => repo.GetById(id)).ReturnsAsync(expectedQuiz);

            var expectedQuestions = _testData.GetTestQuestions().Where(q => q.QuizId == id);
            _questionRepository.Setup(repo => repo.GetByQuizId(id)).ReturnsAsync(expectedQuestions);

            var controller = new QuizzesController(_quizRepository.Object, _questionRepository.Object);

            // Act
            var result = await controller.Edit(id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<EditQuizViewModel>(viewResult.ViewData.Model);
            Assert.NotNull(result);
            Assert.Equal(expectedQuiz.QuizId, model.Quiz.QuizId);
            Assert.Equal(2, model.Questions.Count());
        }

        [Fact]
        public async Task Edit_action_result_method_should_return_BadRequest_when_quiz_id_is_null()
        {
            // Arrange
            var controller = new QuizzesController(_quizRepository.Object, _questionRepository.Object);

            // Act
            var result = await controller.Edit(null);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            var model = Assert.IsAssignableFrom<BadRequestObjectResult>(badRequestObjectResult);
            Assert.Equal(model.StatusCode, badRequestObjectResult.StatusCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_action_result_method_should_return_NotFound_when_no_quiz_found_for_id()
        {
            // Arrange
            var id = 0;
            var expectedQuiz = _testData.GetTestQuizzes().FirstOrDefault(q => q.QuizId == id);

            _quizRepository.Setup(repo => repo.GetById(id)).ReturnsAsync(expectedQuiz);
            var controller = new QuizzesController(_quizRepository.Object, _questionRepository.Object);

            // Act
            var result = await controller.Edit(id);

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            var model = Assert.IsAssignableFrom<NotFoundObjectResult>(notFoundObjectResult);
            Assert.Equal(model.StatusCode, notFoundObjectResult.StatusCode);
            Assert.Equal(model.Value, notFoundObjectResult.Value);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_post_action_result_should_redirect_to_action_when_modelState_valid()
        {
            var id = 1;

            var quiz = _testData.GetTestQuizzes().FirstOrDefault(q => q.QuizId == id);
            _quizRepository.Setup(repo => repo.GetById(id)).ReturnsAsync(quiz);

            var httpContext = new DefaultHttpContext();

            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var controller = new QuizzesController(_quizRepository.Object, _questionRepository.Object)
            {
                TempData = tempData
            };

            // Act
            var result = await controller.Edit(quiz.QuizId, quiz);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Edit_post_action_result_should_return_ViewResult_with_correct_Model_type_when_modelState_not_valid()
        {
            // Arrange
            var quiz = new Quiz
            {
                QuizId = 3
            };

            // Arrange
            var controller = new QuizzesController(_quizRepository.Object, _questionRepository.Object);

            controller.ModelState.AddModelError("Title", "Title is required");

            // Act
            var result = await controller.Edit(quiz.QuizId, quiz);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Edit_post_action_result_method_should_return_BadRequest_when_id_received_different_to_quizId()
        {
            // Arrange
            var id = 0;
            var quiz = new Quiz
            {
                QuizId = 1,
                Title = "Test"
            };

            _quizRepository.Setup(repo => repo.GetById(id)).ReturnsAsync(quiz);

            var controller = new QuizzesController(_quizRepository.Object, _questionRepository.Object);

            // Act
            var result = await controller.Edit(id, quiz);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            var model = Assert.IsAssignableFrom<BadRequestObjectResult>(badRequestObjectResult);
            Assert.Equal(model.StatusCode, badRequestObjectResult.StatusCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_action_result_method_should_return_BadRequest_when_quiz_id_is_null()
        {
            // Arrange
            var controller = new QuizzesController(_quizRepository.Object, _questionRepository.Object);

            // Act
            var result = await controller.Delete(null);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            var model = Assert.IsAssignableFrom<BadRequestObjectResult>(badRequestObjectResult);
            Assert.Equal(model.StatusCode, badRequestObjectResult.StatusCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_action_result_method_should_return_NotFound_when_no_quiz_found_for_id()
        {
            // Arrange
            var id = 0;
            var expectedQuiz = _testData.GetTestQuizzes().FirstOrDefault(q => q.QuizId == id);

            _quizRepository.Setup(repo => repo.GetById(id)).ReturnsAsync(expectedQuiz);
            var controller = new QuizzesController(_quizRepository.Object, _questionRepository.Object);

            // Act
            var result = await controller.Delete(id);

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            var model = Assert.IsAssignableFrom<NotFoundObjectResult>(notFoundObjectResult);
            Assert.Equal(model.StatusCode, notFoundObjectResult.StatusCode);
            Assert.Equal(model.Value, notFoundObjectResult.Value);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_action_result_method_should_return_ViewResult_with_correct_Model_type()
        {
            // Arrange
            var id = 1;

            var expectedQuiz = _testData.GetTestQuizzes().FirstOrDefault(q => q.QuizId == id);
            _quizRepository.Setup(repo => repo.GetById(id)).ReturnsAsync(expectedQuiz);

            var controller = new QuizzesController(_quizRepository.Object, _questionRepository.Object);

            // Act
            var result = await controller.Delete(id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Quiz>(viewResult.ViewData.Model);
            Assert.NotNull(result);
            Assert.Equal(expectedQuiz.QuizId, model.QuizId);
        }
    }
}
