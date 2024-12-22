using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniProjet.Models;
using MiniProjet.Services;
using System.Threading.Tasks;

namespace MiniProjet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Protection par défaut
    public class ReclamationController : ControllerBase
    {
        private readonly IReclamationService _reclamationService;

        public ReclamationController(IReclamationService reclamationService)
        {
            _reclamationService = reclamationService;
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> CreateReclamationAsync([FromBody] ReclamationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdReclamation = await _reclamationService.CreateReclamationAsync(model);
            return CreatedAtAction(nameof(GetReclamationByIdAsync), new { id = createdReclamation.Id }, createdReclamation);
        }

        [HttpGet]
        [Authorize(Roles = "ResponsableSAV")]
        public async Task<IActionResult> GetReclamationsAsync()
        {
            var reclamations = await _reclamationService.GetReclamationsAsync();
            return Ok(reclamations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReclamationByIdAsync(int id)
        {
            var reclamation = await _reclamationService.GetReclamationByIdAsync(id);
            if (reclamation == null)
                return NotFound();
            return Ok(reclamation);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ResponsableSAV")]
        public async Task<IActionResult> UpdateReclamationAsync(int id, [FromBody] ReclamationModel model)
        {
            if (id != model.Id)
                return BadRequest();

            var result = await _reclamationService.UpdateReclamationAsync(model);
            if (!result)
                return NotFound("Réclamation non trouvée.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ResponsableSAV")]
        public async Task<IActionResult> DeleteReclamationAsync(int id)
        {
            var result = await _reclamationService.DeleteReclamationAsync(id);
            if (!result)
                return NotFound("Réclamation non trouvée.");
            return NoContent();
        }
    }
}
