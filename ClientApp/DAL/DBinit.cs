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

      var strekning_TrondheimBergen = new Strekninger
      {
        Fra = "Trondheim",
        Til = "Bergen"
      };

      var strekning_BergenTrondheim = new Strekninger
      {
        Fra = "Bergen",
        Til = "Trondheim"
      };

      var strekning_OsloKiel = new Strekninger
      {
        Fra = "Oslo",
        Til = "Kiel"
      };

      var strekning_KielOslo = new Strekninger
      {
        Fra = "Kiel",
        Til = "Oslo"
      };

      var strekning_GeirangerStavanger = new Strekninger
      {
        Fra = "Geiranger",
        Til = "Stavanger"
      };

      var strekning_StavangerGeiranger = new Strekninger
      {
        Fra = "Stavanger",
        Til = "Geiranger"
      };

      db.Strekninger.Add(strekning_OsloKobenhavn);
      db.Strekninger.Add(strekning_KobenhavnOslo);
      db.Strekninger.Add(strekning_OsloKiel);
      db.Strekninger.Add(strekning_KielOslo);
      db.Strekninger.Add(strekning_TrondheimBergen);
      db.Strekninger.Add(strekning_BergenTrondheim);
      db.Strekninger.Add(strekning_StavangerGeiranger);
      db.Strekninger.Add(strekning_GeirangerStavanger);

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
        Navn = "Balkong lugar",
        Beskrivelse = "Lorem ipsum...",
        AntallSengeplasser = 4,
        Antall = 10,
        AntallLedige = 10,
        Pris = 2400
      };

      var lugar3 = new Lugarer
      {
        Navn = "Innvendig lugar",
        Beskrivelse = "Lorem ipsum...",
        AntallSengeplasser = 2,
        Antall = 15,
        AntallLedige = 15,
        Pris = 599
      };

      var lugar4 = new Lugarer
      {
        Navn = "Familielugar",
        Beskrivelse = "Lorem ipsum...",
        AntallSengeplasser = 6,
        Antall = 40,
        AntallLedige = 40,
        Pris = 800
      };


      List<Lugarer> lugarer = new List<Lugarer>();
      lugarer.Add(lugar1);
      lugarer.Add(lugar2);
      lugarer.Add(lugar3);

      List<Lugarer> lugarer2 = new List<Lugarer>();
      lugarer2.Add(lugar3);
      lugarer2.Add(lugar2);
      lugarer2.Add(lugar4);

      List<Lugarer> lugarer3 = new List<Lugarer>();
      lugarer3.Add(lugar1);
      lugarer3.Add(lugar3);
      lugarer3.Add(lugar4);

      List<Lugarer> lugarer4 = new List<Lugarer>();
      lugarer4.Add(lugar1);
      lugarer4.Add(lugar2);
      lugarer4.Add(lugar4);

      // Baater

      var baat1 = new Baater
      {
        Navn = "Color Fantasy",
        Lugarer = lugarer,
        AntallBilplasser = 300
    };

      var baat2 = new Baater
      {
        Navn = "Color Magic",
        Lugarer = lugarer2,
        AntallBilplasser = 50,
      };

      var baat3 = new Baater
      {
        Navn = "Color Magic",
        Lugarer = lugarer3,
        AntallBilplasser = 150,
      };

      var baat4 = new Baater
      {
        Navn = "Color Magic",
        Lugarer = lugarer4,
        AntallBilplasser = 75,
      };

      db.Baater.Add(baat1);
      db.Baater.Add(baat2);
      db.Baater.Add(baat3);
      db.Baater.Add(baat4);

      // Avganger 
      DateTime date1 = new DateTime(2021, 11, 1, 11, 30, 0);
      String date1String = date1.ToString();
      long date1Ticks = date1.Ticks;

      var avgang1 = new Avganger
      {
        Strekning = strekning_OsloKobenhavn,
        Baat = baat1,
        DatoTid = date1String,
        DatoTidTicks = date1Ticks,
        AntallLedigeBilplasser = baat1.AntallBilplasser,
        LedigeLugarer = baat1.Lugarer,
      };

      DateTime date2 = new DateTime(2021, 11, 5, 11, 30, 0);
      String date2String = date2.ToString();
      long date2Ticks = date2.Ticks;
      var avgang2 = new Avganger
      {
        Strekning = strekning_KobenhavnOslo,
        Baat = baat1,
        DatoTid = date2String,
        DatoTidTicks = date2Ticks,
        AntallLedigeBilplasser = baat1.AntallBilplasser,
        LedigeLugarer = baat1.Lugarer,
      };

      DateTime date3 = new DateTime(2021, 12, 23, 11, 30, 0);
      String date3String = date3.ToString();
      long date3Ticks = date2.Ticks;
      var avgang3 = new Avganger
      {
        Strekning = strekning_KobenhavnOslo,
        Baat = baat1,
        DatoTid = date3String,
        DatoTidTicks = date3Ticks,
        AntallLedigeBilplasser = baat1.AntallBilplasser,
        LedigeLugarer = baat1.Lugarer,
      };

      DateTime date4 = new DateTime(2021, 12, 27, 11, 30, 0);
      String date4String = date4.ToString();
      long date4Ticks = date4.Ticks;
      var avgang4 = new Avganger
      {
        Strekning = strekning_OsloKobenhavn,
        Baat = baat1,
        DatoTid = date4String,
        DatoTidTicks = date4Ticks,
        AntallLedigeBilplasser = baat1.AntallBilplasser,
        LedigeLugarer = baat1.Lugarer,
      };

      DateTime date5 = new DateTime(2021, 08, 29, 11, 30, 0);
      String date5String = date5.ToString();
      long date5Ticks = date5.Ticks;
      var avgang5 = new Avganger
      {
        Strekning = strekning_TrondheimBergen,
        Baat = baat2,
        DatoTid = date5String,
        DatoTidTicks = date5Ticks,
        AntallLedigeBilplasser = baat2.AntallBilplasser,
        LedigeLugarer = baat2.Lugarer,
      };

      DateTime date6 = new DateTime(2021, 09, 2, 11, 30, 0);
      String date6String = date6.ToString();
      long date6Ticks = date6.Ticks;
      var avgang6 = new Avganger
      {
        Strekning = strekning_BergenTrondheim,
        Baat = baat2,
        DatoTid = date6String,
        DatoTidTicks = date6Ticks,
        AntallLedigeBilplasser = baat2.AntallBilplasser,
        LedigeLugarer = baat2.Lugarer,
      };

      DateTime date7 = new DateTime(2022, 5, 11, 11, 30, 0);
      String date7String = date7.ToString();
      long date7Ticks = date7.Ticks;
      var avgang7 = new Avganger
      {
        Strekning = strekning_TrondheimBergen,
        Baat = baat2,
        DatoTid = date7String,
        DatoTidTicks = date7Ticks,
        AntallLedigeBilplasser = baat2.AntallBilplasser,
        LedigeLugarer = baat2.Lugarer,
      };

      DateTime date8 = new DateTime(2022, 5, 13, 11, 30, 0);
      String date8String = date8.ToString();
      long date8Ticks = date8.Ticks;
      var avgang8 = new Avganger
      {
        Strekning = strekning_BergenTrondheim,
        Baat = baat2,
        DatoTid = date8String,
        DatoTidTicks = date8Ticks,
        AntallLedigeBilplasser = baat2.AntallBilplasser,
        LedigeLugarer = baat2.Lugarer,
      };

      DateTime date9 = new DateTime(2021, 02, 28, 11, 30, 0);
      String date9String = date9.ToString();
      long date9Ticks = date9.Ticks;
      var avgang9 = new Avganger
      {
        Strekning = strekning_KielOslo,
        Baat = baat3,
        DatoTid = date9String,
        DatoTidTicks = date9Ticks,
        AntallLedigeBilplasser = baat3.AntallBilplasser,
        LedigeLugarer = baat3.Lugarer,
      };

      DateTime date10 = new DateTime(2021, 03, 4, 11, 30, 0);
      String date10String = date10.ToString();
      long date10Ticks = date10.Ticks;
      var avgang10 = new Avganger
      {
        Strekning = strekning_OsloKiel,
        Baat = baat3,
        DatoTid = date10String,
        DatoTidTicks = date10Ticks,
        AntallLedigeBilplasser = baat3.AntallBilplasser,
        LedigeLugarer = baat3.Lugarer,
      };

      DateTime date11 = new DateTime(2021, 12, 15, 11, 30, 0);
      String date11String = date11.ToString();
      long date11Ticks = date11.Ticks;
      var avgang11 = new Avganger
      {
        Strekning = strekning_KielOslo,
        Baat = baat3,
        DatoTid = date11String,
        DatoTidTicks = date11Ticks,
        AntallLedigeBilplasser = baat3.AntallBilplasser,
        LedigeLugarer = baat3.Lugarer,
      };

      DateTime date12 = new DateTime(2021, 12, 20, 11, 30, 0);
      String date12String = date12.ToString();
      long date12Ticks = date12.Ticks;
      var avgang12 = new Avganger
      {
        Strekning = strekning_OsloKiel,
        Baat = baat3,
        DatoTid = date12String,
        DatoTidTicks = date12Ticks,
        AntallLedigeBilplasser = baat3.AntallBilplasser,
        LedigeLugarer = baat3.Lugarer,
      };

      DateTime date13 = new DateTime(2021, 06, 1, 11, 30, 0);
      String date13String = date13.ToString();
      long date13Ticks = date13.Ticks;
      var avgang13 = new Avganger
      {
        Strekning = strekning_StavangerGeiranger,
        Baat = baat4,
        DatoTid = date13String,
        DatoTidTicks = date13Ticks,
        AntallLedigeBilplasser = baat4.AntallBilplasser,
        LedigeLugarer = baat4.Lugarer,
      };

      DateTime date14 = new DateTime(2021, 06, 6, 11, 30, 0);
      String date14String = date14.ToString();
      long date14Ticks = date14.Ticks;
      var avgang14 = new Avganger
      {
        Strekning = strekning_GeirangerStavanger,
        Baat = baat4,
        DatoTid = date14String,
        DatoTidTicks = date14Ticks,
        AntallLedigeBilplasser = baat4.AntallBilplasser,
        LedigeLugarer = baat4.Lugarer,
      };

      DateTime date15 = new DateTime(2021, 06, 5, 11, 30, 0);
      String date15String = date15.ToString();
      long date15Ticks = date15.Ticks;
      var avgang15 = new Avganger
      {
        Strekning = strekning_StavangerGeiranger,
        Baat = baat4,
        DatoTid = date15String,
        DatoTidTicks = date15Ticks,
        AntallLedigeBilplasser = baat4.AntallBilplasser,
        LedigeLugarer = baat4.Lugarer,
      };

      DateTime date16 = new DateTime(2021, 06, 10, 11, 30, 0);
      String date16String = date16.ToString();
      long date16Ticks = date16.Ticks;
      var avgang16 = new Avganger
      {
        Strekning = strekning_GeirangerStavanger,
        Baat = baat4,
        DatoTid = date16String,
        DatoTidTicks = date16Ticks,
        AntallLedigeBilplasser = baat4.AntallBilplasser,
        LedigeLugarer = baat4.Lugarer,
      };

      db.Avganger.Add(avgang1);
      db.Avganger.Add(avgang2);
      db.Avganger.Add(avgang3);
      db.Avganger.Add(avgang4);
      db.Avganger.Add(avgang5);
      db.Avganger.Add(avgang6);
      db.Avganger.Add(avgang7);
      db.Avganger.Add(avgang8);
      db.Avganger.Add(avgang9);
      db.Avganger.Add(avgang10);
      db.Avganger.Add(avgang11);
      db.Avganger.Add(avgang12);
      db.Avganger.Add(avgang13);
      db.Avganger.Add(avgang14);
      db.Avganger.Add(avgang15);
      db.Avganger.Add(avgang16);

      db.SaveChanges();
    }
  }
}


