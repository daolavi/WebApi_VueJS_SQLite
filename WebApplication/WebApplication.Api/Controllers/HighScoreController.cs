using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Services.HighScoreService;
using WebApplication.Services.Models;

namespace WebApplication.Api.Controllers
{
    [Route("api/[controller]")]
    public class HighScoreController : ControllerBase
    {
        private IHighScoreService highScoreService;

        public HighScoreController(IHighScoreService highScoreService)
        {
            this.highScoreService = highScoreService;
        }

        [HttpGet, Authorize]
        public IActionResult GetAll()
        {
            var response = highScoreService.GetAll();
            return new JsonResult(response);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            var response = highScoreService.GetById(id);
            return new JsonResult(response);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add([FromBody]HighScore highScore)
        {
            var response = highScoreService.Add(highScore);
            return new JsonResult(response);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody]HighScore highScore)
        {
            var response = highScoreService.Update(highScore);
            return new JsonResult(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var response = highScoreService.Delete(id);
            return new JsonResult(response);
        }
    }
}
