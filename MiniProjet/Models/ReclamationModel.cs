using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniProjet.Models
{
    public class ReclamationModel
    {
        public int Id { get; set; }

        [Required]
        public string ClientId { get; set; }
        public Client Client { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public bool IsResolved { get; set; }

        public ICollection<Intervention> Interventions { get; set; }
    }
}
