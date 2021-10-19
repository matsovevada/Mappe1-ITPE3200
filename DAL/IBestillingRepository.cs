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
    Task<Baater> HentBaat(int id);
    Task<int> LagreKunde(Kunde kunde);
    Task<int> LagreBillett(Billett billett);
    Task<Billetter> HentBillett(int id);
    Task<bool> DecrementBilplass(int id);
    Task<bool> OppdaterAntallLedigeLugarer(int id, List<Lugar> lugarer);
    Task<Kunder> HentKunde(int id);
    Task<List<Baater>> HentAlleBaater();
    Task<bool> slettBaat(int id);
    Task<bool> endreBaat(int id, String navn);
    Task<bool> lagreBaat(String navn);
    Task<List<Kunder>> HentAlleKunder();
  }
}
