using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mappe1_ITPE3200.Models;


namespace Mappe1_ITPE3200.ClientApp.DAL
{
  public interface IBestillingRepository
  {
    Task<List<Models.Strekning>> HentAlleStrekninger();
  }
}
