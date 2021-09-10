using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Mappe1_ITPE3200.Models.DatabaseContext;

namespace Mappe1_ITPE3200.ClientApp.DAL
{
  public interface IBestillingRepository
  {
    Task<List<Strekning>> HentAlleStrekninger();
  }
}
