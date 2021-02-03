using System.Threading.Tasks;
using Quizzing.Web.Models;

namespace Quizzing.Web.Data
{
    public interface IQuestionRepository
    {
        Task<Question> GetByQuizId(int? quizId);
        Task<Question> GetByQuestionId(int? questionId);

        bool QuestionExists(int id);

        void Add(Question question);
        void Update(Question question);
        void Remove(Question question);
        Task Save();
    }
}
