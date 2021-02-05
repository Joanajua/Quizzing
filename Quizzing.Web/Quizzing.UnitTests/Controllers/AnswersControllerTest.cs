using System.Collections.Generic;
using System.Linq;
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
using Xunit;

namespace Quizzing.UnitTests
{
    public class AnswersControllerTest
    {
        private readonly SeedTestData _testData = new SeedTestData();
        private readonly Mock<IAnswerRepository> _answerRepository = new Mock<IAnswerRepository>();

        [Fact]
        public void Create_action_should_return_Create_View_with_correct_model()
        {
            // Arrange
            var questionId = 1;

            var answersForQuestion = _testData.GetTestAnswers().Where(q => q.QuestionId == questionId);
            _answerRepository.Setup(repo => repo.GetByQuestionId(questionId)).ReturnsAsync(answersForQuestion.ToList);

            var controller = new AnswersController(_answerRepository.Object);

            // act
            var result = controller.Create(questionId);

            // assert
            Assert.IsType<ViewResult>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public void Create_action_should_return_BadRequest_when_questionId_is_not_valid()
        {
            // Arrange
            var controller = new AnswersController(_answerRepository.Object);

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
            var answer = new Answer
            {
                QuestionId = 1,
                AnswerId = 1,
                AnswerText = "Test"
            };

            var httpContext = new DefaultHttpContext();

            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var controller = new AnswersController(_answerRepository.Object)
            {
                TempData = tempData
            };

            // Act
            var result = await controller.Create(answer);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Create_post_action_result_should_return_ViewResult_with_correct_Model_type_when_modelState_not_valid()
        {
            // Arrange
            var answer = new Answer
            {
                AnswerId = 20,
            };

            // Arrange
            var controller = new AnswersController( _answerRepository.Object);

            controller.ModelState.AddModelError("Title", "Title is required");

            // Act
            var result = await controller.Create(answer);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<Answer>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Edit_action_result_method_should_return_ViewResult_with_correct_Model_type()
        {
            // Arrange
            var id = 1;

            var expectedAnswer = _testData.GetTestAnswers().FirstOrDefault(q => q.AnswerId == id);
            _answerRepository.Setup(repo => repo.GetByAnswerId(id)).ReturnsAsync(expectedAnswer);

            var controller = new AnswersController( _answerRepository.Object);

            // Act
            var result = await controller.Edit(id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Answer>(viewResult.ViewData.Model);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_action_result_method_should_return_BadRequest_when_answer_id_is_null()
        {
            // Arrange
            var controller = new AnswersController(_answerRepository.Object);

            // Act
            var result = await controller.Edit(null);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            var model = Assert.IsAssignableFrom<BadRequestObjectResult>(badRequestObjectResult);
            Assert.Equal(model.StatusCode, badRequestObjectResult.StatusCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_action_result_method_should_return_NotFound_when_no_answer_found_for_id()
        {
            // Arrange
            var id = 0;
            var expectedAnswer = _testData.GetTestAnswers().FirstOrDefault(q => q.AnswerId == id);

            _answerRepository.Setup(repo => repo.GetByAnswerId(id)).ReturnsAsync(expectedAnswer);
            var controller = new AnswersController(_answerRepository.Object);

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

            var answer = _testData.GetTestAnswers().FirstOrDefault(q => q.AnswerId == id);
            _answerRepository.Setup(repo => repo.GetByAnswerId(id)).ReturnsAsync(answer);

            var httpContext = new DefaultHttpContext();

            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var controller = new AnswersController(_answerRepository.Object)
            {
                TempData = tempData
            };

            // Act
            var result = await controller.Edit(answer.AnswerId, answer);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Edit_post_action_result_should_return_ViewResult_with_correct_Model_type_when_modelState_not_valid()
        {
            // Arrange
            var answer = new Answer()
            {
                AnswerId = 20,
            };

            // Arrange
            var controller = new AnswersController(_answerRepository.Object);

            controller.ModelState.AddModelError("Title", "Title is required");

            // Act
            var result = await controller.Edit(answer.AnswerId, answer);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Edit_post_action_result_method_should_return_BadRequest_when_id_received_different_to_answerId()
        {
            // Arrange
            var id = 0;
            var answer = new Answer()
            {
                AnswerId = 1,
                AnswerText = "Test"
            };

            _answerRepository.Setup(repo => repo.GetByAnswerId(id)).ReturnsAsync(answer);

            var controller = new AnswersController(_answerRepository.Object);

            // Act
            var result = await controller.Edit(id, answer);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            var model = Assert.IsAssignableFrom<BadRequestObjectResult>(badRequestObjectResult);
            Assert.Equal(model.StatusCode, badRequestObjectResult.StatusCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_action_result_method_should_return_BadRequest_when_answer_id_is_null()
        {
            // Arrange
            var controller = new AnswersController(_answerRepository.Object);

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
            var expecteAnswer = _testData.GetTestAnswers().FirstOrDefault(q => q.AnswerId == id);

            _answerRepository.Setup(repo => repo.GetByAnswerId(id)).ReturnsAsync(expecteAnswer);
            var controller = new AnswersController(_answerRepository.Object);

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

            var expectedAnswer = _testData.GetTestAnswers().FirstOrDefault(q => q.AnswerId == id);
            _answerRepository.Setup(repo => repo.GetByAnswerId(id)).ReturnsAsync(expectedAnswer);

            var controller = new AnswersController(_answerRepository.Object);

            // Act
            var result = await controller.Delete(id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Answer>(viewResult.ViewData.Model);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteConfirmed_post_action_result_should_redirect_to_action_when_modelState_valid()
        {
            //Arrange
            var id = 1;

            var expectedAnswer = _testData.GetTestAnswers().FirstOrDefault(q => q.AnswerId == id);
            _answerRepository.Setup(repo => repo.GetByAnswerId(id)).ReturnsAsync(expectedAnswer);

            var httpContext = new DefaultHttpContext();

            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var controller = new AnswersController(_answerRepository.Object)
            {
                TempData = tempData
            };

            // Act
            var result = await controller.DeleteConfirmed(expectedAnswer.QuestionId);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
