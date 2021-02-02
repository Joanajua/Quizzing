using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Quizzing.Web.Data;
using Quizzing.Web.Models;
using Quizzing.Web.Utilities.Constants;


namespace Quizzing.Web.Controllers
{
    public class AnswersController : Controller
    {
        private readonly AppDbContext _context;

        public AnswersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Answers
        public async Task<IActionResult> Index(int questionId)
        {
            return View(await _context.Answers.Where(a=>a.QuestionId == questionId).ToListAsync());
        }

        // GET: Answers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers
                .FirstOrDefaultAsync(m => m.AnswerId == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        //// GET: Answers/Create
        //public IActionResult Create(Question question)
        //{
        //    var answersInQuestion = _context.Answers.Where(a => a.QuestionId == question.QuestionId);

        //    if (answersInQuestion.Count() <= 5)
        //    {
        //        var answer = new Answer
        //        {
        //            QuestionId = question.QuestionId,
        //            AnswerText = ""
        //        };

        //        return View(answer);
        //    }

        //    var modelState = new ModelStateDictionary();
        //    modelState.AddModelError(string.Empty, "Is not possible to add more than 5 answers for one question.");
        //    return RedirectToAction(nameof(Edit), "Questions", question);
        //}

        // GET: Answers/Create
        public IActionResult Create(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(Constants.ErrorMessages.BadRequest);
            }

            var answersInQuestion = _context.Answers.Where(a => a.QuestionId == id);

            if (answersInQuestion.Count() <= 4)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnswerId,QuestionId,AnswerText,IsCorrect")] Question question, Answer answer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(answer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Edit), "Questions", new { id = question.QuestionId });
            }
            return View(answer);
        }

        // GET: Answers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }
            return View(answer);
        }

        // POST: Answers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnswerId,QuestionId,AnswerText,IsCorrect")] Answer answer)
        {
            if (id != answer.AnswerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(answer.AnswerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(answer);
        }

        // GET: Answers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers
                .FirstOrDefaultAsync(m => m.AnswerId == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answer = await _context.Answers.FindAsync(id);
            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Edit), "Questions", new { id = answer.QuestionId });
        }

        private bool AnswerExists(int id)
        {
            return _context.Answers.Any(e => e.AnswerId == id);
        }
    }
}
