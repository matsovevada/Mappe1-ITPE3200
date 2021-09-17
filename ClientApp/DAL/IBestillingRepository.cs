using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mappe1_ITPE3200.Models;


namespace Mappe1_ITPE3200.ClientApp.DAL
{
  public interface IBestillingRepository
  {
    Task<List<Strekning>> HentAlleStrekninger();
    Task<List<Avganger>> HentAlleAvganger(Strekning valgtStrekning);
    Task<Avganger> HentValgtAvgang(int id);
    Task<Baater> hentBaat(int id);
  }
}
