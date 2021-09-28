﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mappe1_ITPE3200.Models
{
    public class Kunde
    {        
        public int Id { get; set; }
        [RegularExpression(@"[a-zA-ZøæåØÆÅ. \-]{2,30}")]
        // [a-zA-ZæøåÆØÅ. \-]{2,20}
        public String Fornavn { get; set; }
        [RegularExpression(@"[a-zA-ZøæåØÆÅ. \-]{2,30}")]
        public String Etternavn { get; set; }
        [RegularExpression(@"[0-9a-zA-ZøæåØÆÅ. \-]{2,30}")]
        public String Adresse { get; set; }
        [RegularExpression(@"[0-9]{4}")]
        public String Postnr { get; set; }
        [RegularExpression(@"[a-zA-ZøæåØÆÅ. \-]{2,30}")]
        public String Poststed { get; set; }
        [RegularExpression(@"[0-9]{8}")]
        public String Telefonnummer { get; set; }
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?")]
        public String Epost { get; set; }
    }
}
