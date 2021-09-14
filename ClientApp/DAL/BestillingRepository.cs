using Mappe1_ITPE3200.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Mappe1_ITPE3200.ClientApp.DAL
{
  public class BestillingRepository : IBestillingRepository
  {
    private readonly DatabaseContext _db;

    public BestillingRepository(DatabaseContext db)
    {
      _db = db;
    }

    
    [HttpGet]
    public async Task<List<Models.Strekning>> HentAlleStrekninger()

    {
      List<Models.Strekning> alleStrekninger = await _db.Strekninger.Select(s => new Models.Strekning
      {
        StrekningsID = s.StrekningsID,
        Fra = s.Fra,
        Til = s.Til
      }).ToListAsync();

      return alleStrekninger;
    }
  }
}
