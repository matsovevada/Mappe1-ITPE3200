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
        public async Task<ActionResult> LagreKunde(Kunde lagretKunde)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("testController");
                int kundeLagretId = await _db.LagreKunde(lagretKunde);
                return Ok(kundeLagretId); //BRUKE BADREQUEST OG ActionResult IKKE BOOLS?!
            }
            return BadRequest("Feil i inputvalidering på server");
        }



        [HttpPost]
        [ActionName("lagreBillett")]
        public async Task<int> LagreBillett(Billett innBillett)
        {   
            int billettLagret = await _db.LagreBillett(innBillett);
            
            // oppdater antall ledige bilplasser for avgangen
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
            Console.WriteLine("HALLO I CONTROLLER");
            Billetter billett = await _db.HentBillett(id);
            return billett;
        }


    }
}
