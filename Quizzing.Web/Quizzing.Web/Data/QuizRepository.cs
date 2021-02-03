using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quizzing.Web.Models;

namespace Quizzing.Web.Data
{
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _context;

        public QuizRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Quiz>> GetAll()
        {
            return await _context.Quizzes.ToListAsync();
        }

        public async Task<Quiz> GetById(int? quizId)
        {
            return await _context.Quizzes
                .FirstOrDefaultAsync(q => q.QuizId == quizId);
        }

        public bool QuizExists(int id)
        {
            return _context.Quizzes.Any(e => e.QuizId == id);
        }

        public void Add(Quiz quiz) => _context.Quizzes.Add(quiz);
        public void Update(Quiz quiz) => _context.Quizzes.Update(quiz);
        public void Remove(Quiz quiz) => _context.Quizzes.Remove(quiz);
        public async Task Save() => await _context.SaveChangesAsync();
    }
}
