using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniProjet.Models
{
    public class Client : ApplicationUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public ICollection<ReclamationModel> Reclamations { get; set; }
    }
}
