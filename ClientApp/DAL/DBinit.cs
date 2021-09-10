using System;
using System.Collections.Generic;
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
        StrekningsID = 0,
        Fra = "Oslo",
        Til = "Kobenhavn"
      };

      var strekning_KobenhavnOslo = new Strekning
      {
        StrekningsID = 1,
        Fra = "Kobenhavn",
        Til = "Oslo"
      };

      db.Strekninger.Add(strekning_OsloKobenhavn);
      db.Strekninger.Add(strekning_KobenhavnOslo);

     
      //Lugarer
      var lugar1 = new Lugar
      {
        Romkode = "Baat1_dobbeltrom",
        Beskrivelse = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis dui ligula, dignissim quis urna et, " +
        "faucibus fermentum est. Pellentesque felis orci, egestas at dictum vel, vestibulum non ante. Pellentesque neque.",
        AntallSengeplasser = 2,
        Antall = 5,
        AntallLedige = 5,
        Pris = 0
      };

      var lugar2 = new Lugar
      {
        Romkode = "Baat2_suite",
        Beskrivelse = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis dui ligula, dignissim quis urna et, " +
       "faucibus fermentum est. Pellentesque felis orci, egestas at dictum vel, vestibulum non ante. Pellentesque neque.",
        AntallSengeplasser = 4,
        Antall = 3,
        AntallLedige = 3,
        Pris = 0
      };

      //Baater
      //Legger lugarer til i lugarliste for b?t
      var Lugarer_bat1 = new List<Lugar>();
      Lugarer_bat1.Add(lugar1);

      var bat1 = new Baat
      {
        BaatID = 0,
        Navn = "Bat1",
        AntallBilplasser = 20,
        Lugarer = Lugarer_bat1,
      };

      var Lugarer_bat2 = new List<Lugar>();
      Lugarer_bat1.Add(lugar2);

      var bat2 = new Baat
      {
        BaatID = 1,
        Navn = "B?t2",
        AntallBilplasser = 10,
        Lugarer = Lugarer_bat2,
      };


      db.Baater.Add(bat1);
      db.Baater.Add(bat2);


      //Avganger
      var avgang1 = new Avgang
      {
        StrekningsID = 0,
        BaatID = 0,
        DatoTid = new DateTime(2020, 10, 10, 12, 30, 00),
        AntallLedigeBilplasser = bat1.AntallBilplasser,
        LedigeLugarer = bat1.Lugarer
      };

      //Avganger
      var avgang2 = new Avgang
      {
        StrekningsID = 1,
        BaatID = 1,
        DatoTid = new DateTime(2020, 10, 10, 12, 30, 00),
        AntallLedigeBilplasser = bat2.AntallBilplasser,
        LedigeLugarer = bat2.Lugarer
      };

      db.Avganger.Add(avgang1);
      db.Avganger.Add(avgang2);


      //Poststed
      var poststed1 = new Poststed
      {
        Postnr = "0560",
        PostSted = "Oslo"
      };

      var poststed2 = new Poststed
      {
        Postnr = "1430",
        PostSted = "As"
      };

      var poststed3 = new Poststed
      {
        Postnr = "7052",
        PostSted = "Trondheim"
      };

      var poststed4 = new Poststed
      {
        Postnr = "0456",
        PostSted = "Oslo"
      };

      var poststed5 = new Poststed
      {
        Postnr = "3110",
        PostSted = "Tonsberg"
      };

      db.Poststeder.Add(poststed1);
      db.Poststeder.Add(poststed2);
      db.Poststeder.Add(poststed3);
      db.Poststeder.Add(poststed4);
      db.Poststeder.Add(poststed5);


      db.SaveChanges();
    }
  }
}
