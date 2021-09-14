using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mappe1_ITPE3200.Models
{
    public class Kunde
    {        
        public int Id { get; set; }
        public String Fornavn { get; set; }
        public String Etternavn { get; set; }
        public String Adresse { get; set; }
        public String Postnr { get; set; }
        public String Possted { get; set; }
        public String Telefonnummer { get; set; }
        public String Epost { get; set; }
    }
}
