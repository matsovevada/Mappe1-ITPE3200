﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Mappe1_ITPE3200.Models
{
    [ExcludeFromCodeCoverage]
    public class Lugar
    {
       
        public string Navn { get; set; }
        public string Beskrivelse { get; set; }
        public int AntallSengeplasser { get; set; }
        public int Antall { get; set; }
        public int AntallLedige { get; set; }
        public int Pris { get; set; }
        
    }
}
