using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quizzing.Web.Data;
using Quizzing.Web.Models;
using Quizzing.Web.Utilities.Constants;
using Quizzing.Web.ViewModels.Quizzes;

namespace Quizzing.Web.Controllers
{
    public class QuizzesController : Controller
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IQuestionRepository _questionRepository;

        public QuizzesController(IQuizRepository quizRepository, IQuestionRepository questionRepository)
        {
            _quizRepository = quizRepository;
            _questionRepository = questionRepository;
        }

        // GET: Quizzes
        public async Task<IActionResult> Index()
        {
            return View(await _quizRepository.GetAll());
        }

        // GET: Quizzes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(Constants.ErrorMessages.BadRequest);
            }

            var quiz = await _quizRepository.GetById(id);

            if (quiz == null)
            {
                return NotFound(Constants.ErrorMessages.NotFound);
            }

            var questions = await _questionRepository.GetByQuizId(id);

            if (questions == null)
            {
                return NotFound(Constants.ErrorMessages.NotFoundQuestion);
            }

            var model = new DetailsQuizViewModel
            {
                Quiz = quiz,
                Questions = questions,
            };

            return View(model);
        }

        // GET: Quizzes/Create
        [Authorize(Policy = "edit")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quizzes/Create
        [Authorize(Policy = "edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuizId,Title")] Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                _quizRepository.Add(quiz);

                await _quizRepository.Save();

                return RedirectToAction(nameof(Create), "Questions", new {id = quiz.QuizId} );
            }

            return View(quiz);
        }

        // GET: Quizzes/Edit/5
        [Authorize(Policy = "edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(Constants.ErrorMessages.BadRequest);
            }

            var quiz = await _quizRepository.GetById(id);

            if (quiz == null)
            {
                return NotFound(Constants.ErrorMessages.NotFound);
            }

            var questions = await _questionRepository.GetByQuizId(id);

            var model = new EditQuizViewModel
            {
                Quiz = quiz,
                Questions = questions
            };

            return View(model);
        }

        // POST: Quizzes/Edit/5
        [Authorize(Policy = "edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuizId,Title")] Quiz quiz)
        {
            if (id != quiz.QuizId)
            {
                return NotFound(Constants.ErrorMessages.BadRequest);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _quizRepository.Update(quiz);

                    await _quizRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(quiz.QuizId))
                    {
                        return NotFound(Constants.ErrorMessages.NotFound);
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Edit));
            }
            return View();
        }

        // GET: Quizzes/Delete/5
        [Authorize(Policy = "edit")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _quizRepository.GetById(id);
                
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // POST: Quizzes/Delete/5
        [Authorize(Policy = "edit")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quiz = await _quizRepository.GetById(id);

            _quizRepository.Remove(quiz);

            await _quizRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "edit")]
        private bool QuizExists(int id)
        {
            return _quizRepository.QuizExists(id);
        }
    }
}
