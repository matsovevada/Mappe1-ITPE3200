using Mappe1_ITPE3200.ClientApp.DAL;
using Mappe1_ITPE3200.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpGet]
        [ActionName("hentAvgang")]
        public async Task<List<Avganger>> HentAlleAvganger(String s)
        {
            Array s_split = s.Split(" - ");
            Strekning valgtStrekning = new Strekning()
            {
                Fra = (string)s_split.GetValue(0),
                Til = (string)s_split.GetValue(0)
            };

            List<Avganger> alleStrekninger = await _db.HentAlleAvganger(valgtStrekning);
            return alleStrekninger;
        }


    }
}
