using System;
using System.Diagnostics.CodeAnalysis;

namespace Mappe1_ITPE3200.Models
{
    [ExcludeFromCodeCoverage]
    public class Strekning
    {      
        public int Id { get; set; }
        public string Fra { get; set; }
        public string Til { get; set; }
    
    }
}
