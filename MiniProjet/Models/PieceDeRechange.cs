using System.ComponentModel.DataAnnotations;

namespace MiniProjet.Models
{
    public class PieceDeRechange
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }

        // Initialisation de la collection des interventions si nécessaire
        public ICollection<Intervention> Interventions { get; set; } = new List<Intervention>();
    }
}
