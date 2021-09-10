using Mappe1_ITPE3200.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mappe1_ITPE3200.ClientApp.DAL
{
  public interface IBestillingRepository
  {
    Task<List<Strekning>> HentAlleStrekninger();
  }
}
