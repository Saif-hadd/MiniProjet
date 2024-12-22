using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniProjet.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsUnderWarranty { get; set; }

        // Initialisation de la collection des réclamations
        public ICollection<ReclamationModel> Reclamations { get; set; } = new List<ReclamationModel>();

        // Relations avec les interventions et pièces de rechange
        public ICollection<Intervention> Interventions { get; set; } = new List<Intervention>();

        public ICollection<PieceDeRechange> PiecesDeRechange { get; set; } = new List<PieceDeRechange>();
    }
}
