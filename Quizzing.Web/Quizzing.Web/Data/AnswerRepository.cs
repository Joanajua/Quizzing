using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quizzing.Web.Models;

namespace Quizzing.Web.Data
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly AppDbContext _context;

        public AnswerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Answer>> GetByQuestionId(int? questionId)
        {
            return await _context.Answers.Where(q => q.QuestionId == questionId).ToListAsync();
        }

        public async Task<Answer> GetByAnswerId(int? answerId)
        {
            return await _context.Answers
                .FirstOrDefaultAsync(q => q.AnswerId == answerId);
        }

        public bool AnswerExists(int id)
        {
            return _context.Answers.Any(e => e.AnswerId == id);
        }

        public void Add(Answer answer) => _context.Answers.Add(answer);
        public void Update(Answer answer) => _context.Answers.Update(answer);
        public void Remove(Answer answer) => _context.Answers.Remove(answer);
        public async Task Save() => await _context.SaveChangesAsync();
    }
}
