using Mappe1_ITPE3200.ClientApp.DAL;
using Mappe1_ITPE3200.Models;
using Microsoft.AspNetCore.Mvc;
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
            return alleStrekninger;
        }

        [HttpGet("{id}")]
        [ActionName("hentValgtAvgang")]
        public async Task<Avganger> HentValgtAvgang(int id)
        {
            Avganger avgang = await _db.HentValgtAvgang(id);
            return avgang;
        }

        [HttpGet("{bestilling}")]
        [ActionName("hentBaat")]
        public async Task<Baater> HentBaat(int id)
        {
            Baater baat = await _db.HentBaat(id);
            return baat;
        }

        //Må se på routing 
        [HttpPost]
        [ActionName("lagreKunde")]
        public async Task<int> LagreKunde(Kunde lagretKunde)
        {
            Console.WriteLine("testController");
            int kundeLagret = await _db.LagreKunde(lagretKunde);
            return kundeLagret; //BRUKE BADREQUEST OG ActionResult IKKE BOOLS?!
          

        }

        [HttpPost]
        [ActionName("lagreBillett")]
        public async Task<bool> LagreBillett(Billett innBillett)
        {   
            bool billettLagret = await _db.LagreBillett(innBillett);
        
            if (!billettLagret)
            {
                return false; //BRUKE BADREQUEST OG ActionResult IKKE BOOLS?!
            }
            else
            {
                // oppdater antall ledige bilplasser for avgangen
                if (innBillett.Bilplass)
                {
                    await _db.DecrementBilplass(innBillett.AvgangId);
                }

                // oppdater antall ledige lugarer for avgangen
                await _db.OppdaterAntallLedigeLugarer(innBillett.AvgangId, innBillett.lugarer);

                return true;
            }

        }

        [HttpGet("{bestilling}")]
        [ActionName("hentBillett")]
        public async Task<Baater> HentBillett(int id)
        {
            Baater baat = await _db.HentBaat(id);
            return baat;
        }


    }
}
