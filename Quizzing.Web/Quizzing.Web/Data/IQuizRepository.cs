using System.Collections.Generic;
using System.Threading.Tasks;
using Quizzing.Web.Models;

namespace Quizzing.Web.Data
{
    public interface IQuizRepository
    {
        Task<IEnumerable<Quiz>> GetAll();

        Task<Quiz> GetById(int? quizId);

        bool QuizExists(int id);

        void Add(Quiz quiz);
        void Update(Quiz quiz);
        void Remove(Quiz quiz);
        Task Save();
    }
}
