using System;
using System.Collections.Generic;

namespace Mappe1_ITPE3200.Models
{
    public class Baat
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public virtual List<Lugar> Lugarer { get; set; }
        public int AntallBilplasser { get; set; }
    }
}
