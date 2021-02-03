using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quizzing.Web.Data;
using Quizzing.Web.Models;
using Quizzing.Web.ViewModels.Questions;
using Quizzing.Web.Utilities.Constants;

namespace Quizzing.Web.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        
        public QuestionsController(IQuestionRepository questionRepository, IAnswerRepository answerRepository)
        {
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
        }

        [Authorize(Roles = "edit, view")]
        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(Constants.ErrorMessages.BadRequest);
            }

            var question = await _questionRepository.GetByQuestionId(id);
                
            if (question == null)
            {
                return NotFound(Constants.ErrorMessages.NotFoundQuestion);
            }

            var answers = await _answerRepository.GetByQuestionId(id);
                
            var model = new DetailsQuestionViewModel
            {
                Question = question,
                Answers = answers
            };

            return View(model);
        }

        [Authorize(Policy = "edit")]
        // GET: Questions/Create
        public IActionResult Create(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(Constants.ErrorMessages.BadRequest);
            }
            var question = new Question
            {
                QuizId = (int)id,
                QuestionText = ""
            };
            return View(question);
        }

        // POST: Questions/Create
        [Authorize(Policy = "edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionId,QuizId,QuestionText")] Question question)
        {
            if (ModelState.IsValid)
            {
                _questionRepository.Add(question);

                await _questionRepository.Save();

                //question.QuizId = quiz.QuizId;

                return RedirectToAction(nameof(Create), "Answers", new { id = question.QuestionId });
            }
            return View(question);
        }

        [Authorize(Policy = "edit")]
        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(Constants.ErrorMessages.BadRequest);
            }

            var question = await _questionRepository.GetByQuestionId(id);

            if (question == null)
            {
                return NotFound(Constants.ErrorMessages.NotFoundQuestion);
            }

            var answers = await _answerRepository.GetByQuestionId(id);

            var model = new EditQuestionViewModel
            {
                Question = question,
                Answers = answers
            };

            return View(model);
        }

        // POST: Questions/Edit/5
        [Authorize(Policy = "edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestionId,QuizId,QuestionText")] Question question)
        {
            if (id != question.QuestionId)
            {
                return BadRequest(Constants.ErrorMessages.BadRequest);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _questionRepository.Update(question);
                    await _questionRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.QuestionId))
                    {
                        return NotFound(Constants.ErrorMessages.NotFoundQuestion);
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit),new {id = question.QuestionId});
            }
            return View();
        }

        // GET: Questions/Delete/5
        [Authorize(Policy = "edit")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(Constants.ErrorMessages.BadRequest);
            }

            var question = await _questionRepository.GetByQuestionId(id);

            if (question == null)
            {
                return NotFound(Constants.ErrorMessages.NotFoundQuestion);
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [Authorize(Policy = "edit")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _questionRepository.GetByQuestionId(id);
            _questionRepository.Remove(question);

            await _questionRepository.Save();

            return RedirectToAction(nameof(Edit), "Quizzes", new {id = question.QuizId});
        }

        [Authorize(Policy = "edit")]
        private bool QuestionExists(int id)
        {
            return _questionRepository.QuestionExists(id);
        }
    }
}
