using Mappe1_ITPE3200.ClientApp.DAL;
using Mappe1_ITPE3200.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace Mappe1_ITPE3200.Controllers
{
    [ApiController]
    [Route("api/[controller]/{action}")]
    public class BestillingController
    {
        private IBestillingRepository _db;

        private ILogger<BestillingController> _log;

        public BestillingController(IBestillingRepository db, ILogger<BestillingController> log)
        {
            _db = db;
            _log = log;
        }


        [HttpGet]
        [ActionName("hentStrekning")]
        public async Task<List<Strekning>> HentAlleStrekninger()
        {
            List<Strekning> alleStrekninger = await _db.HentAlleStrekninger();
            _log.LogInformation("Fetched alleStrekninger");
            return alleStrekninger;
        }

        [HttpGet("{strekning}")]
        [ActionName("hentAvgang")]
        public async Task<List<Avganger>> HentAlleAvganger(String strekning)
        {
            Array s_split = strekning.Split(" - ");

            Strekning valgtStrekning = new Strekning()
            {
                Fra = (string)s_split.GetValue(0),
                Til = (string)s_split.GetValue(1)
            };

            List<Avganger> alleStrekninger = await _db.HentAlleAvganger(valgtStrekning);
            _log.LogInformation("GET: Hentet avganger med strekning: "+strekning);
            return alleStrekninger;
        }

        [HttpGet("{id}")]
        [ActionName("hentValgtAvgang")]
        public async Task<Avganger> HentValgtAvgang(int id)
        {
            Avganger avgang = await _db.HentValgtAvgang(id);
            _log.LogInformation("GET: Hentet avgang med ID: "+id);
            return avgang;
        }

        [HttpGet("{bestilling}")]
        [ActionName("hentBaat")]
        public async Task<Baater> HentBaat(int id)
        {
            Baater baat = await _db.HentBaat(id);
            _log.LogInformation("GET: Hentet båt med ID: "+id);
            return baat;
        }

        //Må se på routing 
        [HttpPost]
        [ActionName("lagreKunde")]
        public async Task<bool> LagreKunde(Kunde innKunde)
        {
            Console.WriteLine("testController");
            bool kundeLagret = await _db.LagreKunde(innKunde);
            if (!kundeLagret)
            {
                _log.LogInformation("POST: Problemer med å lagre kunde");
                return false; //BRUKE BADREQUEST OG ActionResult IKKE BOOLS?!
            }
            else
            {
                _log.LogInformation("POST: lagret kunde med ID: "+ innKunde.Id);
                return true;
            }

        }

        [HttpPost("{bestilling}")]
        [ActionName("lagreBillett")]
        public async Task<bool> LagreBillett(Billett innBillett)
        {   
            bool billettLagret = await _db.LagreBillett(innBillett);
            if (!billettLagret)
            {
                _log.LogInformation("POST: Problemer med å poste billett med ID: " + innBillett.Id);
                return false; //BRUKE BADREQUEST OG ActionResult IKKE BOOLS?!
            }
            else
            {
                _log.LogInformation("POST: Lagret billett med ID: " + innBillett.Id);
                return true;
            }

        }

        [HttpGet("{bestilling}")]
        [ActionName("hentBillett")]
        public async Task<Baater> HentBillett(int id)
        {
            Baater baat = await _db.HentBaat(id);
            _log.LogInformation("Kommer vi hit noengang?????????");
            return baat;
        }


    }
}
