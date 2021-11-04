using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Mappe1_ITPE3200.Models
{
    [ExcludeFromCodeCoverage]
    public class Avgang
    {
        public int Id { get; set; }
        public Strekning Strekning { get; set; }
        public Baat Baat { get; set; }
        public string DatoTid { get; set; }
        public long DatoTidTicks { get; set; }
        public int AntallLedigeBilplasser { get; set; }
        public List<Lugar> LedigeLugarer { get; set; }
        public bool Aktiv { get; set; } 
    }
}
