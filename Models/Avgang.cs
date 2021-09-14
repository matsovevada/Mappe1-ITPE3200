using System;
using System.Collections.Generic;

namespace Mappe1_ITPE3200.Models
{
    public class Avgang
    {
        public int Id { get; set; }
        public Strekning Strekning { get; set; }
        public Baat Baat { get; set; }
        public string DatoTid { get; set; }
        public int AntallLedigeBilplasser { get; set; }
        public List<Lugar> LedigeLugarer { get; set; }
        
    }
}
