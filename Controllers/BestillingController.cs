using Mappe1_ITPE3200.ClientApp.DAL;
using Mappe1_ITPE3200.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mappe1_ITPE3200.Controllers
{
    [ApiController]
    [Route("api/[controller]/{action}")]
    public class BestillingController : ControllerBase
    {
        private IBestillingRepository _db;
        private const string _loggetInn = "loggetInn";

        private ILogger<BestillingController> _log;

        public BestillingController(IBestillingRepository db, ILogger<BestillingController> log)
        {
            _db = db;
            _log = log;
        }


        [HttpGet]
        [ActionName("hentStrekning")]
        public async Task<ActionResult> HentAlleStrekninger()
        {
            List<Strekning> alleStrekninger = await _db.HentAlleStrekninger();
            _log.LogInformation("GET: Hentet alle strekninger");
            return Ok(alleStrekninger);
        }

        // finner alle avgangene som hører til den valgte strekningen
        [HttpGet("{strekning}")]
        [ActionName("hentAktiveAvganger")]
        public async Task<List<Avganger>> HentAlleAvganger(String strekning)
        {
            Array s_split = strekning.Split(" - ");

            // strekningen kommer inn som en streng ("fra - til") og et strekning-objekt lages ut ifra denne strengen
            Strekning valgtStrekning = new Strekning()
            {
                Fra = (string)s_split.GetValue(0),
                Til = (string)s_split.GetValue(1)
            };

            List<Avganger> alleAvganger = await _db.HentAlleAvganger(valgtStrekning);
            _log.LogInformation("GET: Hentet aktive avganger");
            return alleAvganger;
        }

        // finner alle retur-avgangene som hører til den valgte strekningen
        [HttpGet("{strekning}")]
        [ActionName("hentAvgangRetur")]
        public async Task<List<Avganger>> HentAlleAvgangerRetur(String strekning)
        {
            Array s_split = strekning.Split(" - ");

            // strekningen kommer inn som en streng ("fra - til") og et strekning-objekt lages ut ifra denne strengen
            Strekning valgtStrekning = new Strekning()
            {
                Fra = (string)s_split.GetValue(0),
                Til = (string)s_split.GetValue(1)
            };

            List<Avganger> alleAvganger = await _db.HentAlleAvganger(valgtStrekning);
            _log.LogInformation("GET: Hentet retur avganger");
            return alleAvganger;
        }

        [HttpGet("{id}")]
        [ActionName("hentValgtAvgang")]
        public async Task<Avganger> HentValgtAvgang(int id)
        {
            Avganger avgang = await _db.HentValgtAvgang(id);
            _log.LogInformation("GET: Hentet avgang med ID:" + id);
            return avgang;
        }

        [HttpGet]
        [ActionName("hentAlleAvganger")]
        public async Task<List<Avganger>> HentAlleAvganger()
        {
            List<Avganger> avganger = await _db.HentAlleAvganger();
            _log.LogInformation("GET: Hentet alle avganger");
            return avganger;
        }

        [HttpPost]
        [ActionName("lagreKunde")]
        public async Task<ActionResult> LagreKunde(Kunde lagretKunde)
        {
            // sjekk at kunde-objektet tilfredsstiller regex-mønsteret som er definert i Kunde-modellen
            if (ModelState.IsValid)
            {
                int kundeLagretId = await _db.LagreKunde(lagretKunde);
                _log.LogInformation("POST: Lagret kunde med ID: " + kundeLagretId);
                return Ok(kundeLagretId);
            }
            _log.LogInformation("FEIL: Feil i LagreKunde()");
            return BadRequest("Feil i inputvalidering på server");
        }


        [HttpGet("{id}")]
        [ActionName("hentKunde")]
        public async Task<Kunde> hentKunde(int id)
        {
            Kunde kunde = await _db.HentKunde(id);
            _log.LogInformation("GET: Hentet kunde med ID: " + id);
            return kunde;
        }

        [HttpPost]
        [ActionName("lagreBillett")]
        public async Task<ActionResult> LagreBillett(Billett innBillett)
        {
          
            var regexAntallPersoner = @"[0-9]{1,2}";
            var regexAntallPersonerRetur = @"[0-9]{1,2}";
            var regexTotalpris = @"[0-9]{1,5}";

            var antallpersonerMatch = Regex.Match(innBillett.AntallPersoner.ToString(), regexAntallPersoner);
            var antallpersonerReturMatch = Regex.Match(innBillett.AntallPersonerRetur.ToString(), regexAntallPersonerRetur);
            var totalprisMatch = Regex.Match(innBillett.TotalPris.ToString(), regexTotalpris);

            if(innBillett.AntallPersonerRetur == 0)
            {
                if(!antallpersonerMatch.Success || !totalprisMatch.Success || !(innBillett.lugarer.Count() > 0))
                {
                    _log.LogInformation("FEIL: Feil i regex LagreBillett()");
                    return BadRequest("Feil input validering på server");
                }
            }

            //Alternativ validering ved retur valgt
            if (innBillett.AntallPersonerRetur != 0)
            {
                if (!antallpersonerMatch.Success || !totalprisMatch.Success || innBillett.lugarer.Count() <= 0 ||
                    !antallpersonerReturMatch.Success || innBillett.lugarerRetur.Count() <= 0)
                {
                    _log.LogInformation("FEIL: Feil i regex LagreBillett()");
                    return BadRequest("Feil input validering på server");
                }
            }

           
           int billettLagret = await _db.LagreBillett(innBillett);

            // fjern en bilplass for avgangen hvis bilplass er valgt i billetten
            if (innBillett.Bilplass)
            {
                await _db.DecrementBilplass(innBillett.AvgangId);
            }
            await _db.OppdaterAntallLedigeLugarer(innBillett.AvgangId, innBillett.lugarer);
            _log.LogInformation("POST: Lagret billett med ID: " + billettLagret);

            return Ok(billettLagret);
        }

        [HttpGet("{id}")]
        [ActionName("hentBillett")]
        public async Task<Billetter> HentBillett(int id)
        {
            Billetter billett = await _db.HentBillett(id);
            _log.LogInformation("GET: Hentet billett med ID: " + id);
            return billett;
        }

        //ADMIN
        [HttpGet]
        [ActionName("hentAlleBilletter")]
        public async Task<ActionResult> HentAlleBillett()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            List<Billetter> billetter = await _db.HentAlleBilletter();
            if(billetter == null)
            {
                _log.LogInformation("GET: Henting av billetter ble ikke utført");
                return NotFound("GET: Henting av billetter ble ikke utført");
            }
            return Ok(billetter);
        }

        //ADMIN
        [HttpPost("{navn}/{beskrivelse}/{antallSengeplasser}/{antall}/{antallLedige}/{pris}")]
        [ActionName("lagreLugar")]
        public async Task<ActionResult> LagreLugar(string navn, string beskrivelse, int antallSengeplasser, int antall, int antallLedige, int pris)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            var regexNavn = @"[a-zA-ZøæåØÆÅ. \-]{2,30}";
            var regexAntallSengeplasser = @"[0-9]{1,2}";
            var regexAntall = @"[0-9]{1,5}";
            var regexPris = @"[0-9]{2,5}";
            var regexBeskrivelse = @"[a-zA-ZøæåØÆÅ. \-]{2,500}";

            var navnMatch = Regex.Match(navn, regexNavn);
            var antallSengeplasserMatch = Regex.Match(antallSengeplasser.ToString(), regexAntallSengeplasser);
            var antallMatch = Regex.Match(antall.ToString(), regexAntall);
            var prisMatch = Regex.Match(pris.ToString(), regexPris);
            var beskrivelseMatch = Regex.Match(beskrivelse, regexBeskrivelse);

            if (!navnMatch.Success || !antallSengeplasserMatch.Success || !antallMatch.Success || !prisMatch.Success || !beskrivelseMatch.Success)
            {
                _log.LogInformation("DELETE: Lugar kunne ikke lagres, feil i validering");
                return BadRequest("DELETE: Lugar kunne ikke lagres, feil i validering");
            }
            
            bool lugarLagret = await _db.LagreLugar(navn, beskrivelse, antallSengeplasser, antall, antallLedige, pris);

            if(!lugarLagret)
            {
                _log.LogInformation("POST: Lagring av lugar ble ikke utført");
                return NotFound("POST: Lagring av lugar ble ikke utført");
            }

            _log.LogInformation("POST: Lagret lugar med navn " + navn);
            return Ok(lugarLagret);
        }

        //ADMIN
        [HttpDelete("{id}")]
        [ActionName("slettStrekning")]
        public async Task<ActionResult> SlettStrekning(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            bool strekningSlettet = await _db.SlettStrekning(id);

            if (!strekningSlettet)
            {
                _log.LogInformation("DELETE: Sletting av strekning ble ikke utført");
                return NotFound("DELETE: Sletting av strekning ble ikke utført");
            }

            _log.LogInformation("DELETE: Slettet strekning med ID: " + id);
            return Ok(strekningSlettet);
        }

        //ADMIN
        [HttpDelete("{id}")]
        [ActionName("slettLugar")]
        public async Task<ActionResult> SlettLugar(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            bool lugarSlettet = await _db.SlettLugar(id);

            if (!lugarSlettet)
            {
                _log.LogInformation("DELETE: Sletting av lugar ble ikke utført");
                return NotFound("Sletting av lugar ble ikke utført");
            }

            _log.LogInformation("DELETE: Slettet lugar med ID: " + id);
            return Ok(lugarSlettet);
        }

        //ADMIN
        [HttpPut("{id}/{strekningFra}/{strekningTil}")]
        [ActionName("endreStrekning")]
        public async Task<ActionResult> EndreStrekning(int id, string strekningFra, string strekningTil)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            var regexStrekning = @"[a-zA-ZøæåØÆÅ. \-]{2,30}";
            var strekningFraMatch = Regex.Match(strekningFra, regexStrekning);
            var strekningTilMatch = Regex.Match(strekningTil, regexStrekning);

            if (!strekningFraMatch.Success || !strekningTilMatch.Success)
            {
                _log.LogInformation("FEIL: Feil i regex i EndreStrekning()");
                return BadRequest("FEIL: Feil i inputvalidering i EndreStrekning()"); ;
            }

            bool strekningEndret = await _db.EndreStrekning(id, strekningFra, strekningTil);

            if(!strekningEndret)
            {
                _log.LogInformation("PUT: Endringen av strekning kunne ikke utføres");
                return NotFound("PUT: Endringen av strekning kunne ikke utføres");
            }

            _log.LogInformation("PUT: Endret strekning med ID: " + id);
            return Ok(strekningEndret);
        }
 
        //ADMIN
        [HttpPost("{strekningFra}/{strekningTil}")]
        [ActionName("lagreStrekning")]
        public async Task<ActionResult> LagreStrekning(string strekningFra, string strekningTil)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            var regexStrekning = @"[a-zA-ZøæåØÆÅ. \-]{2,30}";
            var strekningFraMatch = Regex.Match(strekningFra, regexStrekning);
            var strekningTilMatch = Regex.Match(strekningTil, regexStrekning);

            if (!strekningFraMatch.Success || !strekningTilMatch.Success)
            {
                _log.LogInformation("FEIL: Feil i regex i LagreStrekning()");
                return BadRequest("FEIL: Feil i regex i LagreStrekning()");
            }

            bool strekningLagret = await _db.LagreStrekning(strekningFra, strekningTil);

            if(!strekningLagret)
            {
                _log.LogInformation("POST: Lagring av strekning kunne ikke utføres");
                return NotFound("POST: Lagring av strekning kunne ikke utføres");
            }

            _log.LogInformation("POST: Lagret strekning" + strekningFra + " - " +strekningTil);
            return Ok(strekningLagret);       
        }

        //ADMIN
        [HttpGet]
        [ActionName("hentPoststed")]
        public async Task<ActionResult> HentPoststed()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            List<Poststeder> allePoststeder = await _db.HentAllePoststeder();

            if(allePoststeder == null)
            {
                _log.LogInformation("GET: Poststeder kunne ikke hentes");
                return NotFound("GET: Poststeder kunne ikke hentes");
            }
            _log.LogInformation("GET: Hentet alle poststeder");
            return Ok(allePoststeder);
        }

        //ADMIN
        [HttpDelete("{postnummer}")]
        [ActionName("slettPoststed")]
        public async Task<ActionResult> SlettPoststed(string postnummer)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            bool poststedSlettet = await _db.SlettPoststed(postnummer);
            if (!poststedSlettet)
            {
                _log.LogInformation("DELETE: Poststeder kunne ikke slettes");
                return NotFound("DELETE: Poststeder kunne ikke slettes");
            }
            _log.LogInformation("DELETE: Slettet postnummer: " + postnummer);
            return Ok(poststedSlettet);
        }

        //ADMIN
        [HttpPut("{postnummer}/{poststed}")]
        [ActionName("endrePoststed")]
        public async Task<ActionResult> EndrePoststed(string postnummer, string poststed)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            var regPostnummer = @"[0-9]{4}";
            var regPoststed = @"[a-zA-ZøæåØÆÅ. \-]{2,30}";

            var pnrMatch = Regex.Match(postnummer, regPostnummer);
            var pstdMatch = Regex.Match(poststed, regPoststed);

            if (!pnrMatch.Success || !pstdMatch.Success)
            {
                _log.LogInformation("FEIL: Feil i RegEx i EndrePoststed()");
                return BadRequest("FEIL: Feil i RegEx i EndrePoststed()");
            }

            bool poststedEndret = await _db.EndrePoststed(postnummer, poststed);

            if(!poststedEndret)
            {
                _log.LogInformation("PUT: Poststeder kunne ikke endres");
                return NotFound("PUT: Poststeder kunne ikke endres");
            }

            _log.LogInformation("PUT: Endret poststed med postnummer: " + postnummer);
            return Ok(poststedEndret);
        }

        //ADMIN
        [HttpPost("{postnummer}/{poststed}")]
        [ActionName("lagrePoststed")]
        public async Task<ActionResult> LagrePoststed(string postnummer, string poststed)
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            var regPostnummer = @"[0-9]{4}";
            var regPoststed = @"[a-zA-ZøæåØÆÅ. \-]{2,30}";

            var pnrMatch = Regex.Match(postnummer, regPostnummer);
            var pstdMatch = Regex.Match(poststed, regPoststed);

            if (!pnrMatch.Success || !pstdMatch.Success)
            {
                _log.LogInformation("FEIL: Feil i RegEx i LagrePoststed()");
                return BadRequest("FEIL: Feil i RegEx i LagrePoststed()"); ;
            }

            bool poststedLagret = await _db.LagrePoststed(postnummer, poststed);

            if(!poststedLagret)
            {
                _log.LogInformation("POST: Poststed kunne ikke lagres");
                return NotFound("POST: Poststed kunne ikke lagres");

            }

            _log.LogInformation("POST: Lagret poststed med postnummer: " + postnummer);
            return Ok(poststedLagret);
        }

        //ADMIN
        [ActionName("hentBaater")]
        public async Task<ActionResult> HentBaater()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            List<Baater> baater = await _db.HentAlleBaater();
            if(baater == null)
            {
                _log.LogInformation("GET: Båter kunne ikke hentes");
                return NotFound("GET: Båter kunne ikke hentes");
            }

            _log.LogInformation("GET: Hentet alle båter");
            return Ok(baater);
        }

        //ADMIN
        [HttpDelete("{id}")]
        [ActionName("slettBaat")]
        public async Task<ActionResult> slettBaat(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            bool baat = await _db.slettBaat(id);

            if (!baat)
            {
                _log.LogInformation("DELETE: Båt kunne ikke slettes");
                return BadRequest("DELETE: Båter kunne ikke slettes");
            }

            _log.LogInformation("DELETE: Slettet båt med ID: " + id);
            return Ok(baat);
        }

        //ADMIN
        [HttpPut("{id}/{navn}")]
        [ActionName("endreBaat")]
        public async Task<ActionResult> endreBaat(int id, String navn)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            var regexNavn = @"[a-zA-ZøæåØÆÅ. \-]{2,30}";
            var navnMatch = Regex.Match(navn, regexNavn);

            if (!navnMatch.Success)
            {
                _log.LogInformation("FEIL: Feil i inputvalidering endreBaat");
                return BadRequest("FEIL: Feil i inputvalidering endreBaat");
            }

            bool baatEndret = await _db.endreBaat(id, navn);

            if (!baatEndret)
            {
                _log.LogInformation("GET: Båt kunne ikke endres");
                return NotFound("GET: Båt kunne ikke endres");
            }


            _log.LogInformation("PUT: Endret båt med ID: " + id);
            return Ok(baatEndret);
        }

        //ADMIN
        [HttpPost("{navn}")]
        [ActionName("lagreBaat")]
        public async Task<ActionResult> lagreBaat(String navn)
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            var regexNavn = @"[a-zA-ZøæåØÆÅ. \-]{2,30}";
            var navnMatch = Regex.Match(navn, regexNavn);

            if (!navnMatch.Success)
            {
                _log.LogInformation("FEIL: Feil i inputvalidering lagreBaat");
                return BadRequest("FEIL: Feil i inputvalidering lagreBaat");
            }

            bool baatLagret = await _db.lagreBaat(navn);

            if (!baatLagret)
            {
                _log.LogInformation("POST: Båt kunne ikke lagres");
                return NotFound("POST: Båt kunne ikke lagres");
            }

            _log.LogInformation("POST: Lagret båt med navn: " + navn);
            return Ok(baatLagret);
        }

        //ADMIN
        [ActionName("hentKunder")]
        public async Task<ActionResult> hentKunder()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            List<Kunder> kunder = await _db.HentAlleKunder();
            if(kunder == null)
            {
                _log.LogInformation("GET: Kunder kunne ikke hentes");
                return NotFound("GET: Kunder kunne ikke hentes");
            }

            _log.LogInformation("GET: Hentet alle kunder");
            return Ok(kunder);
        }

        //ADMIN
        [HttpDelete("{id}")]
        [ActionName("slettKunde")]
        public async Task<ActionResult> slettKunde(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            bool kundeSlettet = await _db.slettKunde(id);

            if (!kundeSlettet)
            {
                _log.LogInformation("DELETE: Kunde kunne ikke slettes");
                return NotFound("DELETE: Kunde kunne ikke slettes");
            }

            _log.LogInformation("DELETE: Slettet kunde med ID: " + id);
            return Ok(kundeSlettet);
        }

        //ADMIN
        [HttpPut]
        [ActionName("endreKunde")]
        public async Task<ActionResult> endreKunde(Kunde kunde)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            if (ModelState.IsValid)
            {
                bool kundeEndret = await _db.endreKunde(kunde);
                if(!kundeEndret)
                {
                    _log.LogInformation("PUT: Kunde kunne ikke endres");
                    return NotFound("PUT: Kunde kunne ikke endres");
                }
                _log.LogInformation("PUT: Endret kunde med ID: " + kunde.Id);
                return Ok(kundeEndret);
            }
            _log.LogInformation("Feil i inputvalidering endreKunde");
            return BadRequest("Feil i inputvalidering endreKunde");
        }

        //ADMIN
        [HttpPost]
        [ActionName("loggInn")]
        public async Task<ActionResult> LoggInn(Bruker bruker)
        {
            if (ModelState.IsValid)
            {
                bool returnOK = await _db.LoggInn(bruker);
                if (!returnOK)
                {
                    _log.LogInformation("POST: Innloggingen feilet for bruker" + bruker.Brukernavn);
                    HttpContext.Session.SetString(_loggetInn, "");
                    return Ok(false);
                }

                _log.LogInformation("POST: Logget inn med bruker: " + bruker.Brukernavn);
                HttpContext.Session.SetString(_loggetInn, "LoggetInn");
                return Ok(true); 
            }
            _log.LogInformation("Feil i inputvalidering loggInn");
            return BadRequest("Feil i inputvalidering på server loggInn");
        }

        //ADMIN
        [HttpGet]
        [ActionName("loggUt")]
        public void LoggUt()
        {
            _log.LogInformation("GET: Logget ut!");
            HttpContext.Session.SetString(_loggetInn, "");
        }

        [HttpGet]
        [ActionName("isLoggedIn")]
        public async Task<ActionResult> IsLoggedIn()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                _log.LogInformation("GET: Feil ved innlogging");
                return Unauthorized();
            }

            _log.LogInformation("GET: Logget inn!");
            return Ok();
        }

        //ADMIN
        [HttpPost("{baat}/{strekningFra}/{strekningTil}/{datoTidDag}/{datoTidMnd}/{datoTidAar}/{datoTidTime}/{datoTidMin}/{antallLedigeBilplasser}/{lugarer}/{aktiv}")]
        [ActionName("lagreAvgang")]
        public async Task<ActionResult> LagreAvgang(string baat, string strekningFra, string strekningTil, string datoTidDag, string datoTidMnd, string datoTidAar, string datoTidTime, string datoTidMin, string antallLedigeBilplasser, string lugarer, string aktiv)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            var regexBaat = @"[a-zA-ZøæåØÆÅ. \-]{2,30}";
            var regexStrekningFra = @"[a-zA-ZøæåØÆÅ. \-]{2,30}";
            var regexStrekningTil = @"[a-zA-ZøæåØÆÅ. \-]{2,30}";
            var regexDatoTidDag = @"[1-31]{1,2}";
            var regexDatoTidMnd = @"[1-12]{1,2}";
            var regexDatoTidAar = @"[2021-2030]{4}";
            var regexDatoTidTime = @"[0-23]{1,2}";
            var regexDatoTidMin = @"[0-59]{1,2}";
            var regexAntallLedigeBilplasser = @"[0-1000]{1,4}";

            var baatMatch = Regex.Match(baat, regexBaat);
            var strekningFraMatch = Regex.Match(strekningFra, regexStrekningFra);
            var strekningTilMatch = Regex.Match(strekningTil, regexStrekningTil);
            var datoTidDagMatch = Regex.Match(datoTidDag, regexDatoTidDag);
            var datoTidMndMatch = Regex.Match(datoTidMnd, regexDatoTidMnd);
            var datoTidAarMatch = Regex.Match(datoTidAar, regexDatoTidAar);
            var datoTidTimeMatch = Regex.Match(datoTidTime, regexDatoTidTime);
            var datoTidMinMatch = Regex.Match(datoTidMin, regexDatoTidMin);
            var antallLedigeBilplasserMatch = Regex.Match(antallLedigeBilplasser, regexAntallLedigeBilplasser);
            

            if (!baatMatch.Success || !strekningFraMatch.Success || !strekningTilMatch.Success || !datoTidDagMatch.Success || !datoTidMndMatch.Success
               || !datoTidAarMatch.Success || !datoTidTimeMatch.Success || !datoTidMinMatch.Success || !antallLedigeBilplasserMatch.Success)
            {
                _log.LogInformation("FEIL: Feil i validering lagreAvgang");
                return BadRequest("FEIL: Feil i validering lagreAvgang");
            }

            bool avgangLagret = await _db.lagreAvgang(baat, strekningFra, strekningTil, datoTidDag, datoTidMnd, datoTidAar, datoTidTime, datoTidMin, antallLedigeBilplasser, lugarer, aktiv);

            if (!avgangLagret)
            {
                _log.LogInformation("POST: Avgang kunne ikke lagres");
                return NotFound("POST: Avgang kunne ikke lagres");
            }


            _log.LogInformation("POST: Lagret avgang");
            return Ok(avgangLagret);
        }

        //ADMIN
        [HttpPut("{id}/{baat}/{strekningFra}/{strekningTil}/{datoTidDag}/{datoTidMnd}/{datoTidAar}/{datoTidTime}/{datoTidMin}/{antallLedigeBilplasser}/{lugarer}/{aktiv}")]
        [ActionName("endreAvgang")]
        public async Task<ActionResult> endreAvgang(string id, string baat, string strekningFra, string strekningTil, string datoTidDag, string datoTidMnd, string datoTidAar, string datoTidTime, string datoTidMin, string antallLedigeBilplasser, string lugarer, string aktiv)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            var regexBaat = @"[a-zA-ZøæåØÆÅ. \-]{2,30}";
            var regexStrekningFra = @"[a-zA-ZøæåØÆÅ. \-]{2,30}";
            var regexStrekningTil = @"[a-zA-ZøæåØÆÅ. \-]{2,30}";
            var regexDatoTidDag = @"[1-31]{1,2}";
            var regexDatoTidMnd = @"[1-12]{1,2}";
            var regexDatoTidAar = @"[2021-2030]{4}";
            var regexDatoTidTime = @"[0-23]{1,2}";
            var regexDatoTidMin = @"[0-59]{1,2}";
            var regexAntallLedigeBilplasser = @"[0-1000]{1,4}";

            var baatMatch = Regex.Match(baat, regexBaat);
            var strekningFraMatch = Regex.Match(strekningFra, regexStrekningFra);
            var strekningTilMatch = Regex.Match(strekningTil, regexStrekningTil);
            var datoTidDagMatch = Regex.Match(datoTidDag, regexDatoTidDag);
            var datoTidMndMatch = Regex.Match(datoTidMnd, regexDatoTidMnd);
            var datoTidAarMatch = Regex.Match(datoTidAar, regexDatoTidAar);
            var datoTidTimeMatch = Regex.Match(datoTidTime, regexDatoTidTime);
            var datoTidMinMatch = Regex.Match(datoTidMin, regexDatoTidMin);
            var antallLedigeBilplasserMatch = Regex.Match(antallLedigeBilplasser, regexAntallLedigeBilplasser);


            if (!baatMatch.Success || !strekningFraMatch.Success || !strekningTilMatch.Success || !datoTidDagMatch.Success || !datoTidMndMatch.Success
               || !datoTidAarMatch.Success || !datoTidTimeMatch.Success || !datoTidMinMatch.Success || !antallLedigeBilplasserMatch.Success)
            {
                _log.LogInformation("FEIL: Feil i validering endreAvgang");
                return BadRequest("FEIL: Feil i validering endreAvgang");
            }

            bool avgangEndret = await _db.endreAvgang(id, baat, strekningFra, strekningTil, datoTidDag, datoTidMnd, datoTidAar, datoTidTime, datoTidMin, antallLedigeBilplasser, lugarer, aktiv);

            if(!avgangEndret)
            {
                _log.LogInformation("PUT: Avgang kunne ikke endres");
                return NotFound("PUT: Avgang kunne ikke endres");
            }

            _log.LogInformation("PUT: Endret avgang med ID: " + id);
            return Ok(avgangEndret);
        }

        //ADMIN
        [HttpDelete("{id}")]
        [ActionName("slettAvgang")]
        public async Task<ActionResult> slettAvgang(string id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            int idInt = Int32.Parse(id);

            bool avgangSlettet = await _db.SlettAvgang(idInt);

            if (!avgangSlettet)
            {
                _log.LogInformation("DELETE: Avgang kunne ikke slettes");
                return NotFound("DELETE: Avgang kunne ikke slettes");
            }


            _log.LogInformation("DELETE: Slettet avgang med ID: " + idInt);
            return Ok(avgangSlettet);
        }

        [HttpGet]
        [ActionName("hentAlleLugarer")]
        public async Task<List<LugarMaler>> HentAlleLugarer()
        {
            List<LugarMaler> alleAvganger = await _db.HentAlleLugarer();
            _log.LogInformation("GET: Hentet alle lugarer");
            return alleAvganger;
        }

        //ADMIN
        [HttpGet("{id}")]
        [ActionName("hentLugar")]
        public async Task<ActionResult> hentLugar(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            LugarMaler lugar = await _db.HentLugar(id);
            if (lugar == null)
            {
                _log.LogInformation("GET: Lugar kunne ikke hentes");
                return NotFound("GET: Lugar kunne ikke hentes");
            }

            _log.LogInformation("GET: Hentet lugar med ID: " + id);
            return Ok(lugar);
        }


        //ADMIN
        [HttpPut("{id}/{navn}/{antallSengeplasser}/{antLugarer}/{pris}/{beskrivelse}")]
        [ActionName("endreLugar")]
        public async Task<ActionResult> endreLugar(string id, string navn, string antallSengeplasser, string antLugarer, string pris, string beskrivelse)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            var regexNavn = @"[a-zA-ZøæåØÆÅ. \-]{2,30}";
            var regexAntallSengeplasser = @"[0-9]{1,2}";
            var regexAntall = @"[0-9]{1,5}";
            var regexPris = @"[0-9]{2,5}";
            var regexBeskrivelse = @"[a-zA-ZøæåØÆÅ. \-]{2,500}";

            var navnMatch = Regex.Match(navn, regexNavn);
            var antallSengeplasserMatch = Regex.Match(antallSengeplasser.ToString(), regexAntallSengeplasser);
            var antallMatch = Regex.Match(antLugarer.ToString(), regexAntall);
            var prisMatch = Regex.Match(pris.ToString(), regexPris);
            var beskrivelseMatch = Regex.Match(beskrivelse, regexBeskrivelse);

            if (!navnMatch.Success || !antallSengeplasserMatch.Success || !antallMatch.Success || !prisMatch.Success || !beskrivelseMatch.Success)
            {
                _log.LogInformation("PUT: Lugar kunne ikke endres");
                return NotFound("PUT: Lugar kunne ikke endres");
            }

            bool lugarEndret = await _db.EndreLugar(id, navn, antallSengeplasser, antLugarer, pris, beskrivelse);

            if(!lugarEndret)
            {
                _log.LogInformation("PUT: Lugar kunne ikke endres");
                return NotFound("PUT: Lugar kunne ikke endres");
            }

            _log.LogInformation("PUT: Endret lugar med ID: " + id);
            return Ok(lugarEndret);
        }


        //ADMIN
        [HttpDelete("{id}")]
        [ActionName("slettBillett")]
        public async Task<ActionResult> slettBillett(string id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            bool billettSlettet = await _db.SlettBillett(id);

            if (!billettSlettet)
            {
                _log.LogInformation("DELETE: Billett kunne ikke slettes");
                return NotFound("DELETE: Billett kunne ikke slettes");
            }
            _log.LogInformation("DELETE: Slettet billett med ID: " + id);
            return Ok(billettSlettet);
        }
    }
}