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
    Task<List<Avganger>> HentAktiveAvganger(Strekning valgtStrekning);
    Task<Avganger> HentValgtAvgang(int id);
    Task<Baater> HentBaat(int id);
    Task<int> LagreKunde(Kunde kunde);
    Task<int> LagreBillett(Billett billett);
    Task<Billetter> HentBillett(int id);
    Task<bool> DecrementBilplass(int id);
    Task<bool> OppdaterAntallLedigeLugarer(int id, List<Lugar> lugarer);
    Task<Kunde> HentKunde(int id);
    Task<bool> SlettStrekning(int id);
    Task<bool> SlettLugar(int id);
    Task<bool> EndreStrekning(int id, string nyStrekningFra, string nyStrekningTil);
    Task<bool> LagreStrekning(string StrekningFra, string StrekningTil);
    Task<List<Poststeder>> HentAllePoststeder();
    Task<bool> SlettPoststed(string postnummer);
    Task<bool> EndrePoststed(string postnummer, string nyttPoststed);
    Task<bool> LagrePoststed(string postnummer, string poststed);
    Task<bool> LagreLugar(string navn, string beskrivelse, int antallSengeplasser, int antall, int antallLedige, int pris);
    Task<List<Baater>> HentAlleBaater();
    Task<bool> slettBaat(int id);
    Task<bool> endreBaat(int id, String navn);
    Task<bool> lagreBaat(String navn);
    Task<List<Kunder>> HentAlleKunder();
    Task<bool> slettKunde(int id);
    Task<bool> endreKunde(Kunde k);
    Task<List<LugarMaler>> HentAlleLugarer();
    Task<LugarMaler> HentLugar(int id);
    Task<bool> EndreLugar(string id, string navn, string antallSengeplasser, string antLugarer, string pris, string beskrivelse);
    Task<bool> SlettBillett(string id);

  }
}
