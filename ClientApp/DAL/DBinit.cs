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
        Til = "Kobenhavn",
        strekingReturId = 0
      };

      var strekning_KobenhavnOslo = new Strekninger
      {
        Fra = "Kobenhavn",
        Til = "Oslo",
        strekingReturId = 0
      };

      //Legger til returstrekninger
      strekning_OsloKobenhavn.strekingReturId = strekning_KobenhavnOslo.Id;
      strekning_KobenhavnOslo.strekingReturId = strekning_OsloKobenhavn.Id;

      db.Strekninger.Add(strekning_OsloKobenhavn);
      db.Strekninger.Add(strekning_KobenhavnOslo);

      // Lugarer
      var lugar1 = new Lugarer
      {
        Navn = "Suite",
        Beskrivelse = "Lorem ipsum sit...",
        AntallSengeplasser = 8,
        Antall = 20,
        AntallLedige = 20,
        Pris = 1200
      };

      var lugar2 = new Lugarer
      {
        Navn = "Balkong",
        Beskrivelse = "Lorem ipsum...",
        AntallSengeplasser = 4,
        Antall = 10,
        AntallLedige = 10,
        Pris = 2400
      };

      List<Lugarer> lugarer = new List<Lugarer>();
      lugarer.Add(lugar1);
      lugarer.Add(lugar2);

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
        LedigeLugarer = lugarer
      };

      String date2 = "12/25/2015 12:00:00 AM";
      var avgang2 = new Avganger
      {
        Strekning = strekning_KobenhavnOslo,
        Baat = baat1,
        DatoTid = date2,
        AntallLedigeBilplasser = baat1.AntallBilplasser,
        LedigeLugarer = baat1.Lugarer,
      };

      db.Avganger.Add(avgang1);
      db.Avganger.Add(avgang2);

      db.SaveChanges();
    }
  }
}


