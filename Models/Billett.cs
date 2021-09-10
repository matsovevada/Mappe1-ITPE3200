using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Mappe1_ITPE3200.Models.DatabaseContext;

namespace Mappe1_ITPE3200.Models
{
	public class Billett
	{
		[Key]
		public int Id { get; set; }
		public int AvgangId { get; set; }
		public int KundeId { get; set; }
		public double TotalPris { get; set; }
		public bool Bilplass { get; set; }
		public virtual List<Lugar> lugarer { get; set; }
	}
}
