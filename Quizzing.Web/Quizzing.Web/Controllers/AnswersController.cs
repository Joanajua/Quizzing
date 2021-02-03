using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quizzing.Web.Data;
using Quizzing.Web.Models;
using Quizzing.Web.Utilities.Constants;


namespace Quizzing.Web.Controllers
{
    public class AnswersController : Controller
    {
        private readonly IAnswerRepository _answerRepository;

        public AnswersController(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        // GET: Answers/Create
        [Authorize(Policy = "edit")]
        public IActionResult Create(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(Constants.ErrorMessages.BadRequest);
            }

            var answersInQuestion = _answerRepository.GetByQuestionId(id);

            var listOfAnswers = answersInQuestion.Result;

            if (listOfAnswers.Count() <= 4)
            {
                var answer = new Answer
                {
                    QuestionId = (int)id,
                    AnswerText = ""
                };

                return View(answer);
            }

            return RedirectToAction(nameof(Edit), "Questions", new{ id = id });
        }

        // POST: Answers/Create
        [Authorize(Policy = "edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnswerId,QuestionId,AnswerText,IsCorrect")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                _answerRepository.Add(answer);

                await _answerRepository.Save();
                
                return RedirectToAction(nameof(Edit), "Questions", new { id = answer.QuestionId });
            }
            return View(answer);
        }

        // GET: Answers/Edit/5
        [Authorize(Policy = "edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(Constants.ErrorMessages.BadRequest);
            }

            var answer = await _answerRepository.GetByAnswerId(id); 

            if (answer == null)
            {
                return NotFound(Constants.ErrorMessages.NotFoundAnswer);
            }
            return View(answer);
        }

        // POST: Answers/Edit/5
        [Authorize(Policy = "edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnswerId,QuestionId,AnswerText,IsCorrect")] Answer answer)
        {
            if (id != answer.AnswerId)
            {
                return BadRequest(Constants.ErrorMessages.BadRequest);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _answerRepository.Update(answer);
                    _answerRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(answer.AnswerId))
                    {
                        return NotFound(Constants.ErrorMessages.NotFoundAnswer);
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit), "Questions",new {id = answer.QuestionId});
            }
            return View();
        }

        // GET: Answers/Delete/5
        [Authorize(Policy = "edit")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(Constants.ErrorMessages.BadRequest);
            }

            var answer = await _answerRepository.GetByAnswerId(id);
                
            if (answer == null)
            {
                return NotFound(Constants.ErrorMessages.NotFoundQuestion);
            }

            return View(answer);
        }

        // POST: Answers/Delete/5
        [Authorize(Policy = "edit")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answer = await _answerRepository.GetByAnswerId(id);

            _answerRepository.Remove(answer);

            await _answerRepository.Save();

            return RedirectToAction(nameof(Edit), "Questions", new { id = answer.QuestionId });
        }

        [Authorize(Policy = "edit")]
        private bool AnswerExists(int id)
        {
            return _answerRepository.AnswerExists(id);
        }
    }
}
