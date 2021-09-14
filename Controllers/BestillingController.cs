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
    [Route("api/[controller]")]
    public class BestillingController
    {
        private IBestillingRepository _db;

        public BestillingController(IBestillingRepository db)
        {
            _db = db;
        }


        [HttpGet]
        public async Task<List<Models.Strekning>> HentAlleStrekninger()
        {
            List<Models.Strekning> alleStrekninger = await _db.HentAlleStrekninger();
            return alleStrekninger;
        }
    }
}
