﻿using Mappe1_ITPE3200.ClientApp.DAL;
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
        public async Task<int> LagreBillett(Billett innBillett)
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
                    return -1;
                }
            }

            //Alternativ validering ved retur valgt
            if (innBillett.AntallPersonerRetur != 0)
            {
                if (!antallpersonerMatch.Success || !totalprisMatch.Success || innBillett.lugarer.Count() <= 0 ||
                    !antallpersonerReturMatch.Success || innBillett.lugarerRetur.Count() <= 0)
                {
                    _log.LogInformation("FEIL: Feil i regex LagreBillett()");
                    return -1;
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
            return billettLagret;
        }

        [HttpGet("{id}")]
        [ActionName("hentBillett")]
        public async Task<Billetter> HentBillett(int id)
        {
            Billetter billett = await _db.HentBillett(id);
            _log.LogInformation("GET: Hentet billett med ID: " + id);
            return billett;
        }

        [HttpGet]
        [ActionName("hentAlleBilletter")]
        public async Task<List<Billetter>> HentAlleBillett()
        {
            List<Billetter> billetter = await _db.HentAlleBilletter();
            return billetter;
        }

        [HttpPost("{navn}/{beskrivelse}/{antallSengeplasser}/{antall}/{antallLedige}/{pris}")]
        [ActionName("lagreLugar")]
        public async Task<bool> LagreLugar(string navn, string beskrivelse, int antallSengeplasser, int antall, int antallLedige, int pris)
        {
            
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
                return false;
            }
            _log.LogInformation("POST: Lagret lugar med navn " + navn);
            return await _db.LagreLugar(navn, beskrivelse, antallSengeplasser, antall, antallLedige, pris);
        }

        [HttpDelete("{id}")]
        [ActionName("slettStrekning")]
        public async Task<ActionResult> SlettStrekning(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            bool returOK = await _db.SlettStrekning(id);
            if (!returOK)
            {
                //_log.LogInformation("Sletting av Strekningen ble ikke utført");
                return NotFound("Sletting av Strekningen ble ikke utført");
            }
            return Ok("Strekning slettet");
        }

        [HttpDelete("{id}")]
        [ActionName("slettLugar")]

        public async Task<bool> SlettLugar(int id)
        {
            _log.LogInformation("DELETE: Slettet lugar med ID: " + id);
            return await _db.SlettLugar(id);
        }

        [HttpPut("{id}/{strekningFra}/{strekningTil}")]
        [ActionName("endreStrekning")]
        public async Task<bool> EndreStrekning(int id, string strekningFra, string strekningTil)
        {

            var regexStrekning = @"[a-zA-ZøæåØÆÅ. \-]{2,30}";
            var strekningFraMatch = Regex.Match(strekningFra, regexStrekning);
            var strekningTilMatch = Regex.Match(strekningTil, regexStrekning);

            if (!strekningFraMatch.Success || !strekningTilMatch.Success)
            {
                _log.LogInformation("FEIL: Feil i regex i EndreStrekning()");
                return false;
            }

            _log.LogInformation("PUT: Endret strekning med ID: " + id);
            return await _db.EndreStrekning(id, strekningFra, strekningTil);
        }

        [HttpPost("{strekningFra}/{strekningTil}")]
        [ActionName("lagreStrekning")]
        public async Task<bool> LagreStrekning(string strekningFra, string strekningTil)
        {
            var regexStrekning = @"[a-zA-ZøæåØÆÅ. \-]{2,30}";
            var strekningFraMatch = Regex.Match(strekningFra, regexStrekning);
            var strekningTilMatch = Regex.Match(strekningTil, regexStrekning);

            if (!strekningFraMatch.Success || !strekningTilMatch.Success)
            {
                _log.LogInformation("FEIL: Feil i regex i LagreStrekning()");
                return false;
            }

            _log.LogInformation("POST: Lagret strekning" + strekningFra + " - " +strekningTil);
            return await _db.LagreStrekning(strekningFra, strekningTil);    
        }

        [HttpGet]
        [ActionName("hentPoststed")]
        public async Task<ActionResult> HentPoststed()
        {
            List<Poststeder> allePoststeder = await _db.HentAllePoststeder();
            _log.LogInformation("GET: Hentet alle poststeder");
            return Ok(allePoststeder);
        }

        [HttpDelete("{postnummer}")]
        [ActionName("slettPoststed")]
        public async Task<bool> SlettPoststed(string postnummer)
        {
            _log.LogInformation("DELETE: Slettet med ID: " + postnummer);
            return await _db.SlettPoststed(postnummer);
        }

        [HttpPut("{postnummer}/{poststed}")]
        [ActionName("endrePoststed")]
        public async Task<bool> EndrePoststed(string postnummer, string poststed)
        {
            var regPostnummer = @"[0-9]{4}";
            var regPoststed = @"[a-zA-ZøæåØÆÅ. \-]{2,30}";

            var pnrMatch = Regex.Match(postnummer, regPostnummer);
            var pstdMatch = Regex.Match(poststed, regPoststed);

            if (!pnrMatch.Success || !pstdMatch.Success)
            {
                _log.LogInformation("ERROR: Feil i RegEx i EndrePoststed()");
                return false;
            }

            _log.LogInformation("PUT: Endret poststed med postnummer: " + postnummer);
            return await _db.EndrePoststed(postnummer, poststed);
        }

        [HttpPost("{postnummer}/{poststed}")]
        [ActionName("lagrePoststed")]
        public async Task<bool> LagrePoststed(string postnummer, string poststed)
        {
            var regPostnummer = @"[0-9]{4}";
            var regPoststed = @"[a-zA-ZøæåØÆÅ. \-]{2,30}";

            var pnrMatch = Regex.Match(postnummer, regPostnummer);
            var pstdMatch = Regex.Match(poststed, regPoststed);

            if (!pnrMatch.Success || !pstdMatch.Success)
            {
                _log.LogInformation("ERROR: Feil i RegEx i LagrePoststed()");
                return false;
            }

            _log.LogInformation("POST: Lagret poststed med postnummer: " + postnummer);
            return await _db.LagrePoststed(postnummer, poststed);
        }

        [ActionName("hentBaater")]
        public async Task<ActionResult> HentBaater()
        {
            List<Baater> baater = await _db.HentAlleBaater();
            _log.LogInformation("GET: Hentet alle båter");
            return Ok(baater);
        }


        [HttpDelete("{id}")]
        [ActionName("slettBaat")]
        public async Task<bool> slettBaat(int id)
        {
            _log.LogInformation("DELETE: Slettet båt med ID: " + id);
            return await _db.slettBaat(id);
        }

        [HttpPut("{id}/{navn}")]
        [ActionName("endreBaat")]
        public async Task<bool> endreBaat(int id, String navn)
        {
            var regexNavn = @"[a-zA-ZøæåØÆÅ. \-]{2,30}";
            var navnMatch = Regex.Match(navn, regexNavn);

            if (!navnMatch.Success)
            {
                _log.LogInformation("FEIL: Feil i regex i endreBaat()");
                return false;
            }

            _log.LogInformation("PUT: Endret båt med ID: " + id);
            return await _db.endreBaat(id, navn);
        }

        [HttpPost("{navn}")]
        [ActionName("lagreBaat")]
        public async Task<bool> lagreBaat(String navn)
        {

            var regexNavn = @"[a-zA-ZøæåØÆÅ. \-]{2,30}";
            var navnMatch = Regex.Match(navn, regexNavn);

            if (!navnMatch.Success)
            {
                _log.LogInformation("FEIL: Feil i regex i lagreBaat()");
                return false;
            }

            _log.LogInformation("POST: Lagret båt med navn: " + navn);
            return await _db.lagreBaat(navn);
        }


        [ActionName("hentKunder")]
        public async Task<ActionResult> hentKunder()
        {
            List<Kunder> kunder = await _db.HentAlleKunder();
            _log.LogInformation("GET: Hentet alle kunder");
            return Ok(kunder);
        }

        [HttpDelete("{id}")]
        [ActionName("slettKunde")]
        public async Task<bool> slettKunde(int id)
        {
            _log.LogInformation("DELETE: Slettet kunde med ID: " + id);
            return await _db.slettKunde(id);
        }

        [HttpPut]
        [ActionName("endreKunde")]
        public async Task<ActionResult> endreKunde(Kunde kunde)
        {
            if (ModelState.IsValid)
            {
                _log.LogInformation("PUT: Endret kunde med ID: " + kunde.Id);
                bool kundeEndret = await _db.endreKunde(kunde);
                return Ok(kundeEndret);
            }
            _log.LogInformation("Feil i endreKunde");
            return BadRequest("Feil i inputvalidering på server");
        }

        [HttpPost]
        [ActionName("loggInn")]
        public async Task<bool> LoggInn(Bruker bruker)
        {
            bool returnOK = await _db.LoggInn(bruker);
            if (!returnOK)
            {
                _log.LogInformation("POST: Innloggingen feilet for bruker" + bruker.Brukernavn);
                HttpContext.Session.SetString(_loggetInn, "");
                return false;
            }
            _log.LogInformation("POST: Logget inn med bruker: " + bruker.Brukernavn);
            HttpContext.Session.SetString(_loggetInn, "LoggetInn");
            return true;
        }

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
                _log.LogInformation("GET: Feil ve innlogging");
                return Unauthorized();
            }
            _log.LogInformation("GET: Logget inn!");
            return Ok();
        }

        [HttpPost("{baat}/{strekningFra}/{strekningTil}/{datoTidDag}/{datoTidMnd}/{datoTidAar}/{datoTidTime}/{datoTidMin}/{antallLedigeBilplasser}/{lugarer}/{aktiv}")]
        [ActionName("lagreAvgang")]
        public async Task<bool> LagreAvgang(string baat, string strekningFra, string strekningTil, string datoTidDag, string datoTidMnd, string datoTidAar, string datoTidTime, string datoTidMin, string antallLedigeBilplasser, string lugarer, string aktiv)
        {
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
                _log.LogInformation("FEIL: Feil i regex i lagreAvgang()");
                return false;
            }

            _log.LogInformation("POST: Lagret avgang");
            return await _db.lagreAvgang(baat, strekningFra, strekningTil, datoTidDag, datoTidMnd, datoTidAar, datoTidTime, datoTidMin, antallLedigeBilplasser, lugarer, aktiv);
        }


        [HttpPut("{id}/{baat}/{strekningFra}/{strekningTil}/{datoTidDag}/{datoTidMnd}/{datoTidAar}/{datoTidTime}/{datoTidMin}/{antallLedigeBilplasser}/{lugarer}/{aktiv}")]
        [ActionName("endreAvgang")]
        public async Task<bool> endreAvgang(string id, string baat, string strekningFra, string strekningTil, string datoTidDag, string datoTidMnd, string datoTidAar, string datoTidTime, string datoTidMin, string antallLedigeBilplasser, string lugarer, string aktiv)
        {
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
                _log.LogInformation("FEIL: Feil i regex i endreAvgang()");
                return false;
            }

            _log.LogInformation("PUT: Endret avgang med ID: " + id);
            return await _db.endreAvgang(id, baat, strekningFra, strekningTil, datoTidDag, datoTidMnd, datoTidAar, datoTidTime, datoTidMin, antallLedigeBilplasser, lugarer, aktiv);
        }

        [HttpDelete("{id}")]
        [ActionName("slettAvgang")]
        public async Task<bool> slettAvgang(string id)
        {
            int idInt = Int32.Parse(id);
            _log.LogInformation("DELETE: Slettet avgang med ID: " + idInt);
            return await _db.SlettAvgang(idInt);
        }

        [HttpGet]
        [ActionName("hentAlleLugarer")]
        public async Task<List<LugarMaler>> HentAlleLugarer()
        {
            List<LugarMaler> alleAvganger = await _db.HentAlleLugarer();
            _log.LogInformation("GET: Hentet alle lugarer");
            return alleAvganger;
        }

        [HttpGet("{id}")]
        [ActionName("hentLugar")]
        public async Task<LugarMaler> hentLugar(int id)
        {
            LugarMaler lugar = await _db.HentLugar(id);
            _log.LogInformation("GET: Hentet lugar med ID: " + id);
            return lugar;
        }


        [HttpPut("{id}/{navn}/{antallSengeplasser}/{antLugarer}/{pris}/{beskrivelse}")]
        [ActionName("endreLugar")]
        public async Task<bool> endreLugar(string id, string navn, string antallSengeplasser, string antLugarer, string pris, string beskrivelse)
        {
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
                return false;
            }
            _log.LogInformation("PUT: Endret lugar med ID: " + id);

            return await _db.EndreLugar(id, navn, antallSengeplasser, antLugarer, pris, beskrivelse);
        }


        [HttpDelete("{id}")]
        [ActionName("slettBillett")]
        public async Task<bool> slettBillett(string id)
        {
            return await _db.SlettBillett(id);
        }
    }
}