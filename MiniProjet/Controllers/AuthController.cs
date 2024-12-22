using Microsoft.AspNetCore.Mvc;
using MiniProjet.Models;
using MiniProjet.Services;
using System.Threading.Tasks;

namespace MiniProjet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        // Injection du service AuthService dans le contrôleur
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // Endpoint pour l'enregistrement d'un nouvel utilisateur
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message); // Si l'authentification échoue, retourne un message d'erreur

            return Ok(result); // Retourne un statut 200 avec les informations de l'utilisateur et du token
        }

        // Endpoint pour générer un token JWT pour l'utilisateur
        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message); // Si le token n'est pas généré, retourne un message d'erreur

            return Ok(result); // Retourne un statut 200 avec le token
        }

        // Endpoint pour ajouter un rôle à un utilisateur existant
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result); // Si l'ajout du rôle échoue, retourne un message d'erreur

            return Ok(model); // Retourne un statut 200 avec le modèle de rôle ajouté
        }
    }
}
