using System;
using System.ComponentModel.DataAnnotations;

namespace MiniProjet.Models
{
    public class Facture
    {
        public int Id { get; set; }

        [Required]
        public int InterventionId { get; set; }
        public Intervention Intervention { get; set; }

        public decimal MontantTotal { get; set; }

        public DateTime DateEmission { get; set; } = DateTime.Now;
    }
}
