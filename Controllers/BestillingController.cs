using Mappe1_ITPE3200.ClientApp.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Mappe1_ITPE3200.Models.DatabaseContext;

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
        public async Task<List<Strekning>> HentAlleStrekninger()
        {
            List<Strekning> alleStrekninger = await _db.HentAlleStrekninger();
            return alleStrekninger;
        }
    }
}
