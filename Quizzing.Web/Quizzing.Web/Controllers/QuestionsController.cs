using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Quizzing.Web.Data;
using Quizzing.Web.Models;
using Quizzing.Web.ViewModels.Questions;
using Quizzing.Web.Utilities.Constants;

namespace Quizzing.Web.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly AppDbContext _context;

        public QuestionsController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "edit, view")]
        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(Constants.ErrorMessages.BadRequest);
            }

            var question = await _context.Questions
                .FirstOrDefaultAsync(m => m.QuestionId == id);

            if (question == null)
            {
                return NotFound(Constants.ErrorMessages.NotFoundQuestion);
            }

            var answers = await _context.Answers
                .Where(m => m.QuestionId == id).ToListAsync();

            var model = new DetailsQuestionViewModel
            {
                Question = question,
                Answers = answers,
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

        [Authorize(Policy = "edit")]
        // POST: Questions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionId,QuizId,QuestionText")] Quiz quiz, Question question)
        {
            if (ModelState.IsValid)
            {
                _context.Add(question);
                await _context.SaveChangesAsync();
                question.QuizId = quiz.QuizId;
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

            var question = await _context.Questions.FindAsync(id);

            if (question == null)
            {
                return NotFound(Constants.ErrorMessages.NotFoundQuestion);
            }

            var answers = await _context.Answers.Where(a => a.QuestionId == id).ToListAsync();

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
                    _context.Update(question);
                    await _context.SaveChangesAsync();
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
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .FirstOrDefaultAsync(m => m.QuestionId == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [Authorize(Policy = "edit")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Edit), "Quizzes", new {id = question.QuizId});
        }

        [Authorize(Policy = "edit")]
        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.QuestionId == id);
        }
    }
}
