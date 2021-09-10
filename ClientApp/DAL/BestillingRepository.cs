using Mappe1_ITPE3200.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mappe1_ITPE3200.ClientApp.DAL
{
  public class BestillingRepository : IBestillingRepository
  {
    private readonly DbContext _db;

    public BestillingRepository(DbContext db)
    {
      _db = db;
    }

    [HttpGet]
    public async Task<List<Strekning>> HentAlleStrekninger()
    {
      List<Strekning> alleStrekninger = await _db.
      return alleStrekninger;
    }
  }
}
