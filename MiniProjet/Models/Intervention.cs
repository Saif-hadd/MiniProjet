using System;
using System.ComponentModel.DataAnnotations;

namespace MiniProjet.Models
{
    public class Intervention
    {
        public int Id { get; set; }

        [Required]
        public int ReclamationId { get; set; }
        public ReclamationModel Reclamation { get; set; }

        [Required]
        public string TechnicienId { get; set; }
        public Technicien Technicien { get; set; }

        public bool IsUnderWarranty { get; set; }

        // TotalCost est calculé si l'article est hors garantie
        public decimal? TotalCost { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        // Ajout de la relation avec Article
        public int ArticleId { get; set; }  // Clé étrangère pour Article
        public Article Article { get; set; } // Propriété de navigation vers Article

        public ICollection<PieceDeRechange> PiecesDeRechange { get; set; }
    }
}
