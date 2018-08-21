using System.Collections.Generic;
using WebApplication.Services.Models;

namespace WebApplication.Services.HighScoreService
{
    public interface IHighScoreService
    {
        IEnumerable<HighScore> GetAll();

        HighScore GetById(int id);

        HighScore Add(HighScore highScore);

        HighScore Update(HighScore highScore);

        HighScore Delete(int id);
    }
}
