using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniProjet.Models;
using MiniProjet.Services;
using System.Threading.Tasks;

namespace MiniProjet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InterventionController : ControllerBase
    {
        private readonly IInterventionService _interventionService;

        public InterventionController(IInterventionService interventionService)
        {
            _interventionService = interventionService;
        }

        /// <summary>Créer une intervention.</summary>
        [HttpPost]
        [Authorize(Roles = "ResponsableSAV")]
        public async Task<IActionResult> CreateInterventionAsync([FromBody] Intervention model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdIntervention = await _interventionService.CreateInterventionAsync(model);
            return CreatedAtAction(nameof(GetInterventionByIdAsync), new { id = createdIntervention.Id }, createdIntervention);
        }

        /// <summary>Récupérer toutes les interventions.</summary>
        [HttpGet]
        public async Task<IActionResult> GetInterventionsAsync()
        {
            var interventions = await _interventionService.GetInterventionsAsync();
            return Ok(interventions);
        }

        /// <summary>Récupérer une intervention par ID.</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInterventionByIdAsync(int id)
        {
            var intervention = await _interventionService.GetInterventionByIdAsync(id);
            if (intervention == null)
                return NotFound();
            return Ok(intervention);
        }

        /// <summary>Mettre à jour une intervention.</summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "ResponsableSAV")]
        public async Task<IActionResult> UpdateInterventionAsync(int id, [FromBody] Intervention model)
        {
            if (id != model.Id)
                return BadRequest("L'ID ne correspond pas.");

            var result = await _interventionService.UpdateInterventionAsync(model);
            if (!result)
                return NotFound("Intervention non trouvée.");

            return NoContent();
        }

        /// <summary>Supprimer une intervention par ID.</summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "ResponsableSAV")]
        public async Task<IActionResult> DeleteInterventionAsync(int id)
        {
            var result = await _interventionService.DeleteInterventionAsync(id);
            if (!result)
                return NotFound("Intervention non trouvée.");

            return NoContent();
        }
    }
}
