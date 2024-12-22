using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniProjet.Models;
using MiniProjet.Services;
using System.Threading.Tasks;

namespace MiniProjet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Protéger tous les endpoints par défaut
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost]
        [Authorize(Roles = "ResponsableSAV")]
        public async Task<IActionResult> CreateArticleAsync([FromBody] Article model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdArticle = await _articleService.CreateArticleAsync(model);
            return CreatedAtAction(nameof(GetArticleByIdAsync), new { id = createdArticle.Id }, createdArticle);
        }

        [HttpGet]
        public async Task<IActionResult> GetArticlesAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var articles = await _articleService.GetArticlesAsync();
            return Ok(articles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticleByIdAsync(int id)
        {
            var article = await _articleService.GetArticleByIdAsync(id);
            if (article == null)
                return NotFound();
            return Ok(article);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ResponsableSAV")]
        public async Task<IActionResult> UpdateArticleAsync(int id, [FromBody] Article model)
        {
            if (id != model.Id)
                return BadRequest("L'ID de l'article ne correspond pas.");

            var result = await _articleService.UpdateArticleAsync(model);
            if (!result)
                return NotFound("Article non trouvé.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ResponsableSAV")]
        public async Task<IActionResult> DeleteArticleAsync(int id)
        {
            var result = await _articleService.DeleteArticleAsync(id);
            if (!result)
                return NotFound("Article non trouvé.");
            return NoContent();
        }
    }
}
