using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Repository;
using WebApplication.Repository.Entities;

namespace WebApplication.Services.HighScoreService
{
    public class HighScoreService : IHighScoreService
    {
        private WebApplicationDbContext dbContext;

        private IMapper mapper;

        public HighScoreService(WebApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public IEnumerable<Models.HighScore> GetAll()
        {
            var highScores = dbContext.HighScores.Select(x => mapper.Map<Models.HighScore>(x));
            return highScores;
        }

        public Models.HighScore GetById(int id)
        {
            var entity = dbContext.HighScores.FirstOrDefault(x => x.Id == id);
            return mapper.Map<Models.HighScore>(entity);
        }

        public Models.HighScore Add(Models.HighScore highScore)
        {
            var entity = mapper.Map<HighScore>(highScore);
            entity = dbContext.HighScores.Add(entity).Entity;
            dbContext.SaveChanges();
            return mapper.Map<Models.HighScore>(entity);
        }

        public Models.HighScore Update(Models.HighScore highScore)
        {
            var entity = dbContext.HighScores.FirstOrDefault(x => x.Id == highScore.Id);
            if (entity != null)
            {
                mapper.Map(highScore, entity);
                entity = dbContext.HighScores.Update(entity).Entity;
                dbContext.SaveChanges();
                return mapper.Map<Models.HighScore>(entity);
            }

            return null;
        }

        public Models.HighScore Delete(int id)
        {
            var entity = dbContext.HighScores.FirstOrDefault(x => x.Id == id);
            if (entity != null)
            {
                entity = dbContext.HighScores.Remove(entity).Entity;
                dbContext.SaveChanges();
                return mapper.Map<Models.HighScore>(entity);
            }

            return null;
        }
    }
}
