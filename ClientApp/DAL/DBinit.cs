using System;
using Mappe1_ITPE3200.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static Mappe1_ITPE3200.Models.DatabaseContext;

namespace Mappe1_ITPE3200.ClientApp.DAL
{
  public static class DBinit
  {
    public static void Seed(IApplicationBuilder app) 
    {
      var serviceScope = app.ApplicationServices.CreateScope();

      var db = serviceScope.ServiceProvider.GetService<DatabaseContext>();

      db.Database.EnsureDeleted();
      db.Database.EnsureCreated();


      //Strekninger
      var strekning_OsloKobenhavn = new Strekning
      {
        Fra = "Oslo",
        Til = "K?benhavn"
      };

      var strekning_KobenhavnOslo = new Strekning
      {
        Fra = "K?benhavn",
        Til = "Oslo"
      };

      db.Strekninger.Add(strekning_OsloKobenhavn);
      db.Strekninger.Add(strekning_KobenhavnOslo);







      db.SaveChanges();
    }
  }
}
