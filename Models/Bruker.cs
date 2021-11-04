using System.ComponentModel.DataAnnotations;

namespace Mappe1_ITPE3200.Models
{
    public class Bruker
    {
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public string Brukernavn { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9\-. ]+$")]
        public string Passord { get; set; }
    }
}