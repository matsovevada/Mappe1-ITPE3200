using System;
using System.Collections;
using System.Collections.Generic;
using Mappe1_ITPE3200.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
      var strekning_OsloKobenhavn = new Strekninger
      {
        Fra = "Oslo",
        Til = "Kobenhavn"
      };

      var strekning_KobenhavnOslo = new Strekninger
      {
        Fra = "Kobenhavn",
        Til = "Oslo"
      };

      db.Strekninger.Add(strekning_OsloKobenhavn);
      db.Strekninger.Add(strekning_KobenhavnOslo);

      // Lugarer
      var lugar1 = new Lugarer
      {
        Romkode = "KS40",
        Beskrivelse = "Suite",
        AntallSengeplasser = 8,
        Antall = 20,
        AntallLedige = 20,
        Pris = 1200
      };

      List<Lugarer> lugarer = new List<Lugarer>();
      lugarer.Add(lugar1);

      // Baater

      var baat1 = new Baater
      {
        Navn = "Color Fantasy",
        Lugarer = lugarer,
        AntallBilplasser = 300
    };

      db.Baater.Add(baat1);

      // Avganger
      String date = DateTime.Now.ToString("dddd, dd MMMM yyy");
      var avgang1 = new Avganger
      {
        Strekning = strekning_OsloKobenhavn,
        Baat = baat1,
        DatoTid = date,
        AntallLedigeBilplasser = baat1.AntallBilplasser,
        LedigeLugarer = baat1.Lugarer,
      };

      db.Avganger.Add(avgang1);

      db.SaveChanges();
    }
  }
}


