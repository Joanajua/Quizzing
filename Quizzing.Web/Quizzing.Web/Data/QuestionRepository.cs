using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quizzing.Web.Models;

namespace Quizzing.Web.Data
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext _context;

        public QuestionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Question>> GetByQuizId(int? quizId)
        {
            return await _context.Questions.Where(q => q.QuizId == quizId).ToListAsync();
        }

        public async Task<Question> GetByQuestionId(int? questionId)
        {
            return await _context.Questions
                .FirstOrDefaultAsync(q => q.QuestionId == questionId);
        }

        public bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.QuestionId == id);
        }

        public void Add(Question question) => _context.Questions.Add(question);
        public void Update(Question question) => _context.Questions.Update(question);
        public void Remove(Question question) => _context.Questions.Remove(question);
        public async Task Save() => await _context.SaveChangesAsync();
    }
}
