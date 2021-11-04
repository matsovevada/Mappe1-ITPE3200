using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Mappe1_ITPE3200.Models
{
    [ExcludeFromCodeCoverage]
    public class Bruker
    {
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public string Brukernavn { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9\-. ]+$")]
        public string Passord { get; set; }
    }
}