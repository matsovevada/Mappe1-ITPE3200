using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


namespace Mappe1_ITPE3200.Models
{
    [ExcludeFromCodeCoverage]
	public class Billett
	{
		public int Id { get; set; }
		public int AvgangId { get; set; }
		public int AvgangIdRetur { get; set; }
		public int KundeId { get; set; }
		public int AntallPersoner { get; set; }
		public int AntallPersonerRetur { get; set; }
		public double TotalPris { get; set; }
		public bool Bilplass { get; set; }
		public bool BilplassRetur { get; set; }
		public List<Lugar> lugarer { get; set; }
		public List<Lugar> lugarerRetur { get; set; }
	}
}
