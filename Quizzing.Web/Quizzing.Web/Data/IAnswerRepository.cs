using System.Collections.Generic;
using System.Threading.Tasks;
using Quizzing.Web.Models;

namespace Quizzing.Web.Data
{
    public interface IAnswerRepository
    {
        Task<IEnumerable<Answer>> GetByQuestionId(int? questionId);
        Task<Answer> GetByAnswerId(int? answerId);

        bool AnswerExists(int id);

        void Add(Answer answer);
        void Update(Answer answer);
        void Remove(Answer answer);
        Task Save();
    }
}
