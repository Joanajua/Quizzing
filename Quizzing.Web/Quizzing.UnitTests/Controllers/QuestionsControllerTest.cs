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
using Quizzing.Web.ViewModels.Questions;
using Quizzing.Web.ViewModels.Quizzes;
using Xunit;

namespace Quizzing.UnitTests
{
    public class QuestionsControllerTest
    {
        private readonly SeedTestData _testData = new SeedTestData();
        private readonly Mock<IQuestionRepository> _questionRepository = new Mock<IQuestionRepository>();
        private readonly Mock<IAnswerRepository> _answerRepository = new Mock<IAnswerRepository>();

        [Fact]
        public async Task Details_action_result_method_should_return_ViewResult_with_correct_Model_type()
        {
            // Arrange
                var id = 1;
                var expectedQuestion = _testData.GetTestQuestions().FirstOrDefault(q => q.QuestionId == id);

                _questionRepository.Setup(repo => repo.GetByQuestionId(id)).ReturnsAsync(expectedQuestion);

                var controller = new QuestionsController(_questionRepository.Object, _answerRepository.Object);

                // Act
                var result = await controller.Details(id);

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<DetailsQuestionViewModel>(viewResult.ViewData.Model);
                Assert.NotNull(result);
                Assert.Equal(expectedQuestion.QuestionId, model.Question.QuestionId);
        }

        [Fact]
        public async Task Details_action_result_method_should_return_BadRequest_when_question_id_is_null()
        {
            // Arrange
            var controller = new QuestionsController(_questionRepository.Object, _answerRepository.Object);

            // Act
            var result = await controller.Details(null);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            var model = Assert.IsAssignableFrom<BadRequestObjectResult>(badRequestObjectResult);
            Assert.Equal(model.StatusCode, badRequestObjectResult.StatusCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_action_result_method_should_return_NotFound_when_no_question_found_for_id()
        {
            // Arrange
            var id = 0;
            var expectedQuestions = _testData.GetTestQuestions().Where(q => q.QuizId == id);

            _questionRepository.Setup(repo => repo.GetByQuizId(id)).ReturnsAsync(expectedQuestions);
            var controller = new QuestionsController(_questionRepository.Object, _answerRepository.Object);

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
            var quizId = 1;
            var controller = new QuestionsController(_questionRepository.Object, _answerRepository.Object);

            // act
            var result = controller.Create(quizId);

            // assert
            Assert.IsType<ViewResult>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public void Create_action_should_return_BadRequest_when_quizId_is_not_valid()
        {
            // Arrange
            var controller = new QuestionsController(_questionRepository.Object, _answerRepository.Object);

            // Act
            var result = controller.Create(id: null);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            var model = Assert.IsAssignableFrom<BadRequestObjectResult>(badRequestObjectResult);
            Assert.Equal(model.StatusCode, badRequestObjectResult.StatusCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_post_action_result_should_redirect_to_action_when_modelState_valid()
        {
            // Arrange
            var question = new Question
            {
                QuestionId = 1,
                QuizId = 1,
                QuestionText = "Test"
            };

            var httpContext = new DefaultHttpContext();

            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var controller = new QuestionsController(_questionRepository.Object, _answerRepository.Object)
            {
                TempData = tempData
            };

            // Act
            var result = await controller.Create(question);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Create_post_action_result_should_return_ViewResult_with_correct_Model_type_when_modelState_not_valid()
        {
            // Arrange
            var question = new Question
            {
                QuestionId = 6,
            };

            // Arrange
            var controller = new QuestionsController(_questionRepository.Object, _answerRepository.Object);

            controller.ModelState.AddModelError("Title", "Title is required");

            // Act
            var result = await controller.Create(question);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<Question>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Edit_action_result_method_should_return_ViewResult_with_correct_Model_type()
        {
            // Arrange
            var id = 1;

            var expectedQuestion = _testData.GetTestQuestions().FirstOrDefault(q => q.QuestionId == id);
            _questionRepository.Setup(repo => repo.GetByQuestionId(id)).ReturnsAsync(expectedQuestion);

            var controller = new QuestionsController(_questionRepository.Object, _answerRepository.Object);

            // Act
            var result = await controller.Edit(id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<EditQuestionViewModel>(viewResult.ViewData.Model);
            Assert.NotNull(result);
            Assert.Equal(expectedQuestion, model.Question);
        }

        [Fact]
        public async Task Edit_action_result_method_should_return_BadRequest_when_quiz_id_is_null()
        {
            // Arrange
            var controller = new QuestionsController(_questionRepository.Object, _answerRepository.Object);

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
            var expectedQuestions = _testData.GetTestQuestions().Where(q => q.QuizId == id);

            _questionRepository.Setup(repo => repo.GetByQuizId(id)).ReturnsAsync(expectedQuestions);
            var controller = new QuestionsController(_questionRepository.Object, _answerRepository.Object);

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

            var question = _testData.GetTestQuestions().FirstOrDefault(q => q.QuestionId == id);
            _questionRepository.Setup(repo => repo.GetByQuestionId(id)).ReturnsAsync(question);

            var httpContext = new DefaultHttpContext();

            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var controller = new QuestionsController(_questionRepository.Object, _answerRepository.Object)
            {
                TempData = tempData
            };

            // Act
            var result = await controller.Edit(question.QuestionId, question);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Edit_post_action_result_should_return_ViewResult_with_correct_Model_type_when_modelState_not_valid()
        {
            // Arrange
            var question = new Question
            {
                QuestionId = 6,
            };

            // Arrange
            var controller = new QuestionsController(_questionRepository.Object, _answerRepository.Object);

            controller.ModelState.AddModelError("Title", "Title is required");

            // Act
            var result = await controller.Edit(question.QuestionId, question);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Edit_post_action_result_method_should_return_BadRequest_when_id_received_different_to_quizId()
        {
            // Arrange
            var id = 0;
            var question = new Question()
            {
                QuestionId = 1,
                QuestionText = "Test"
            };

            _questionRepository.Setup(repo => repo.GetByQuestionId(id)).ReturnsAsync(question);

            var controller = new QuestionsController(_questionRepository.Object, _answerRepository.Object);

            // Act
            var result = await controller.Edit(id, question);

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
            var controller = new QuestionsController(_questionRepository.Object, _answerRepository.Object);

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
            var expectedQuestion = _testData.GetTestQuestions().FirstOrDefault(q => q.QuestionId == id);

            _questionRepository.Setup(repo => repo.GetByQuestionId(id)).ReturnsAsync(expectedQuestion);
            var controller = new QuestionsController(_questionRepository.Object, _answerRepository.Object);

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

            var expectedQuestion = _testData.GetTestQuestions().FirstOrDefault(q => q.QuestionId == id);
            _questionRepository.Setup(repo => repo.GetByQuestionId(id)).ReturnsAsync(expectedQuestion);

            var controller = new QuestionsController(_questionRepository.Object, _answerRepository.Object);

            // Act
            var result = await controller.Delete(id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Question>(viewResult.ViewData.Model);
            Assert.NotNull(result);
            Assert.Equal(expectedQuestion.QuestionId, model.QuestionId);
        }

        [Fact]
        public async Task DeleteConfirmed_post_action_result_should_redirect_to_action_when_modelState_valid()
        {
            //Arrange
            var questionId = 1;

            var expectedQuestion = _testData.GetTestQuestions().FirstOrDefault(q => q.QuestionId == questionId);
            _questionRepository.Setup(repo => repo.GetByQuestionId(questionId)).ReturnsAsync(expectedQuestion);

            var httpContext = new DefaultHttpContext();

            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var controller = new QuestionsController(_questionRepository.Object, _answerRepository.Object)
            {
                TempData = tempData
            };

            // Act
            var result = await controller.DeleteConfirmed(expectedQuestion.QuestionId);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
