﻿using Mappe1_ITPE3200.ClientApp.DAL;
using Mappe1_ITPE3200.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mappe1_ITPE3200.Controllers
{
    [ApiController]
    [Route("api/[controller]/{action}")]
    public class BestillingController : ControllerBase
    {
        private IBestillingRepository _db;

        public BestillingController(IBestillingRepository db)
        {
            _db = db;
        }


        [HttpGet]
        [ActionName("hentStrekning")]
        public async Task<List<Strekning>> HentAlleStrekninger()
        {
            List<Strekning> alleStrekninger = await _db.HentAlleStrekninger();
            return alleStrekninger;
        }

        // finner alle avgangene som hører til den valgte strekningen
        [HttpGet("{strekning}")]
        [ActionName("hentAvgang")]
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
            return alleAvganger;
        }

        [HttpGet("{strekning}")]
        [ActionName("hentAktiveAvganger")]
        public async Task<List<Avganger>> HentAktiveAvganger(String strekning)
        {
            Array s_split = strekning.Split(" - ");

            // strekningen kommer inn som en streng ("fra - til") og et strekning-objekt lages ut ifra denne strengen
            Strekning valgtStrekning = new Strekning()
            {
                Fra = (string)s_split.GetValue(0),
                Til = (string)s_split.GetValue(1)
            };

            List<Avganger> alleAvganger = await _db.HentAktiveAvganger(valgtStrekning);
            return alleAvganger;
        }


        [HttpGet("{id}")]
        [ActionName("hentValgtAvgang")]
        public async Task<Avganger> HentValgtAvgang(int id)
        {
            Avganger avgang = await _db.HentValgtAvgang(id);
            return avgang;
        }

        [HttpPost]
        [ActionName("lagreKunde")]
        public async Task<ActionResult> LagreKunde(Kunde lagretKunde)
        {
            // sjekk at kunde-objektet tilfredsstiller regex-mønsterert som er definert i Kunde-modellen
            if (ModelState.IsValid)
            {
                int kundeLagretId = await _db.LagreKunde(lagretKunde);
                return Ok(kundeLagretId);
            }
            return BadRequest("Feil i inputvalidering på server");
        }


        [HttpGet("{id}")]
        [ActionName("hentKunde")]
        public async Task<Kunde> hentKunde(int id)
        {
            Kunde kunde = await _db.HentKunde(id);
            return kunde;
        }

        [HttpPost]
        [ActionName("lagreBillett")]
        public async Task<int> LagreBillett(Billett innBillett)
        {
            int billettLagret = await _db.LagreBillett(innBillett);

            // fjern en bilplass for avgangen hvis bilplass er valgt i billetten
            if (innBillett.Bilplass)
            {
                await _db.DecrementBilplass(innBillett.AvgangId);
            }
            await _db.OppdaterAntallLedigeLugarer(innBillett.AvgangId, innBillett.lugarer);

            return billettLagret;
        }

        [HttpGet("{id}")]
        [ActionName("hentBillett")]
        public async Task<Billetter> HentBillett(int id)
        {
            Billetter billett = await _db.HentBillett(id);
            return billett;
        }

        [HttpPost("{navn}/{beskrivelse}/{antallSengeplasser}/{antall}/{antallLedige}/{pris}")]
        [ActionName("lagreLugar")]
        public async Task<bool> LagreLugar(string navn, string beskrivelse, int antallSengeplasser, int antall, int antallLedige, int pris)
        {
            return await _db.LagreLugar(navn, beskrivelse, antallSengeplasser, antall, antallLedige, pris);
        }

        [HttpDelete("{id}")]
        [ActionName("slettStrekning")]
        public async Task<bool> SlettStrekning(int id)
        {
            return await _db.SlettStrekning(id);
        }

        [HttpDelete("{id}")]
        [ActionName("slettLugar")]

        public async Task<bool> SlettLugar(int id)
        {
            return await _db.SlettLugar(id);
        }

        [HttpPut("{id}/{strekningFra}/{strekningTil}")]
        [ActionName("endreStrekning")]
        public async Task<bool> EndreStrekning(int id, string strekningFra, string strekningTil)
        {

            return await _db.EndreStrekning(id, strekningFra, strekningTil);
        }

        [HttpPost("{strekningFra}/{strekningTil}")]
        [ActionName("lagreStrekning")]
        public async Task<bool> LagreStrekning(string strekningFra, string strekningTil)
        {

            return await _db.LagreStrekning(strekningFra, strekningTil);
        }

        [HttpGet]
        [ActionName("hentPoststed")]
        public async Task<List<Poststeder>> HentPoststed()
        {
            return await _db.HentAllePoststeder();
        }

        [HttpDelete("{postnummer}")]
        [ActionName("slettPoststed")]
        public async Task<bool> SlettPoststed(string postnummer)
        {
            return await _db.SlettPoststed(postnummer);
        }

        [HttpPut("{postnummer}/{poststed}")]
        [ActionName("endrePoststed")]
        public async Task<bool> EndrePoststed(string postnummer, string poststed)
        {
            return await _db.EndrePoststed(postnummer, poststed);
        }

        [HttpPost("{postnummer}/{poststed}")]
        [ActionName("lagrePoststed")]
        public async Task<bool> LagrePoststed(string postnummer, string poststed)
        {
            return await _db.LagrePoststed(postnummer, poststed);
        }

        [ActionName("hentBaater")]
        public async Task<List<Baater>> HentBaater()
        {
            List<Baater> baater = await _db.HentAlleBaater();
            return baater;
        }


        [HttpDelete("{id}")]
        [ActionName("slettBaat")]
        public async Task<bool> slettBaat(int id)
        {
            return await _db.slettBaat(id);
        }

        [HttpPut("{id}/{navn}")]
        [ActionName("endreBaat")]
        public async Task<bool> endreBaat(int id, String navn)
        {
            return await _db.endreBaat(id, navn);
        }

        [HttpPost("{navn}")]
        [ActionName("lagreBaat")]
        public async Task<bool> lagreBaat(String navn)
        {
            return await _db.lagreBaat(navn);
        }


        [ActionName("hentKunder")]
        public async Task<List<Kunder>> hentKunder()
        {
            List<Kunder> kunder = await _db.HentAlleKunder();
            return kunder;
        }

        [HttpDelete("{id}")]
        [ActionName("slettKunde")]
        public async Task<bool> slettKunde(int id)
        {
            return await _db.slettKunde(id);
        }

        [HttpGet]
        [ActionName("hentAlleLugarer")]
        public async Task<List<LugarMaler>> HentAlleLugarer()
        {
            return await _db.HentAlleLugarer();
        }

        [HttpPut("{kunde}")]
        public async Task<bool> endreKunde(Kunde k)
        {
            return await _db.endreKunde(k);
        }

        [HttpPost("{baat}/{strekningFra}/{strekningTil}/{datoTidDag}/{datoTidMnd}/{datoTidAar}/{datoTidTime}/{datoTidMin}/{antallLedigeBilplasser}/{lugarer}/{aktiv}")]
        [ActionName("lagreAvgang")]
        public async Task<bool> LagreAvgang(string baat, string strekningFra, string strekningTil, string datoTidDag, string datoTidMnd, string datoTidAar, string datoTidTime, string datoTidMin, string antallLedigeBilplasser, string lugarer, string aktiv)
        {
            Avganger nyAvgang = new Avganger();
            Baater baatFraDB = await _db.HentBaatPaaNavn(baat);

            nyAvgang.Baat = baatFraDB;

            nyAvgang.StrekningFra = strekningFra;
            nyAvgang.StrekningTil = strekningTil;


            DateTime date = new DateTime(Int32.Parse(datoTidAar), Int32.Parse(datoTidMnd), Int32.Parse(datoTidDag), Int32.
            Parse(datoTidTime), Int32.Parse(datoTidMin), 0);

            string dateString = date.ToString();
            long date1Ticks = date.Ticks;
            nyAvgang.DatoTid = dateString;
            nyAvgang.DatoTidTicks = date1Ticks;

            List<Lugarer> lugarListe = new List<Lugarer>();

            Console.WriteLine("LUGARER");
            Console.WriteLine(lugarer);



            string[] lugarerSplit = lugarer.Split(",");
            foreach(string lug in lugarerSplit)
            {
                Console.WriteLine(lug);
                LugarMaler lugarFraDB = new LugarMaler();
                lugarFraDB = await _db.HentLugarPaaNavn(lug);
                Lugarer lugarTilAvgang = new Lugarer(lugarFraDB.Navn, lugarFraDB.Beskrivelse, lugarFraDB.AntallSengeplasser, lugarFraDB.Antall, lugarFraDB.AntallLedige, lugarFraDB.Pris);
                lugarListe.Add(lugarTilAvgang);
                Console.WriteLine(lugarTilAvgang.Antall);
            }
       


   

            nyAvgang.AntallLedigeBilplasser = Int32.Parse(antallLedigeBilplasser);

            nyAvgang.Aktiv = Convert.ToBoolean(aktiv);


            return await _db.lagreAvgang(nyAvgang);
        }
    }
}