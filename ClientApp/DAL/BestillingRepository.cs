using Mappe1_ITPE3200.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Mappe1_ITPE3200.Models.DatabaseContext;

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
    public async Task<List<Strekning>> HentAlleStrekninger()
    {
      List<Strekning> alleStrekninger = await _db.Strekninger.ToListAsync();
      return alleStrekninger;
    }
  }
}
