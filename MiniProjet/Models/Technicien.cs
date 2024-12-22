using System.Collections.Generic;

namespace MiniProjet.Models
{
    public class Technicien : ApplicationUser
    {
        public ICollection<Intervention> Interventions { get; set; }
    }
}
