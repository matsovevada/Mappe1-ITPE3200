using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Mappe1_ITPE3200.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mappe1_ITPE3200.ClientApp.DAL
{
    [ExcludeFromCodeCoverage]
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

      // Lugarer-maler

      // mal1
      string Navn = "Suite";
      string Beskrivelse = "Veldig stort rom med totalt atte sengeplasser. Passer for en stor familie. Utstyrt med piano, boblebad og gratis minibar.";
      int AntallSengeplasser = 8;
      int Antall = 5;
      int AntallLedige = 5;
      int Pris = 7500;

      // mal2
      string Navn1 = "Balkong lugar";
      string Beskrivelse1 = "Lugar med flott sjoutsikt. Denne lugartypen har totalt fire sengeplasser og er ideel for en vennegruppe eller liten familie.";
      int AntallSengeplasser1 = 4;
      int Antall1 = 75;
      int AntallLedige1 = 75;
      int Pris1 = 1500;

      // mal3
      string Navn2 = "Innvendig lugar";
        string Beskrivelse2 = "Vart billigste alternativ. Liten lugar med to sengeplasser.";
        int AntallSengeplasser2 = 2;
        int Antall2 = 150;
        int AntallLedige2 = 150;
        int Pris2 = 400;

      // mal4
      string Navn3 = "Familielugar";
      string Beskrivelse3 = "Den perfekte lugaren for familier. Har totalt seks sengeplasser og utsikt til sjoen.";
      int AntallSengeplasser3 = 6;
      int Antall3 = 40;
      int AntallLedige3 = 40;
      int Pris3 = 1000;

    LugarMaler lugarMal1 = new LugarMaler()
    {
        Navn = new string(Navn),
        Beskrivelse = new string(Beskrivelse),
        AntallSengeplasser = AntallSengeplasser,
        Antall = Antall,
        AntallLedige = AntallLedige,
        Pris = Pris
    };

    LugarMaler lugarMal2 = new LugarMaler()
    {
        Navn = new string(Navn1),
        Beskrivelse = new string(Beskrivelse1),
        AntallSengeplasser = AntallSengeplasser1,
        Antall = Antall1,
        AntallLedige = AntallLedige1,
        Pris = Pris1
    };

    db.LugarMaler.Add(lugarMal1);
    db.LugarMaler.Add(lugarMal2);
  
      List<Lugarer> lugarer = new List<Lugarer>();
      lugarer.Add(new Lugarer(Navn, Beskrivelse, AntallSengeplasser, Antall, AntallLedige, Pris));
      lugarer.Add(new Lugarer(Navn2, Beskrivelse2, AntallSengeplasser2, Antall2, AntallLedige2, Pris2));
      lugarer.Add(new Lugarer(Navn3, Beskrivelse3, AntallSengeplasser3, Antall3, AntallLedige3, Pris3));

      List<Lugarer> lugarer2 = new List<Lugarer>();
      lugarer2.Add(new Lugarer(Navn, Beskrivelse, AntallSengeplasser, Antall, AntallLedige, Pris));
      lugarer2.Add(new Lugarer(Navn2, Beskrivelse2, AntallSengeplasser2, Antall2, AntallLedige2, Pris2));
      lugarer2.Add(new Lugarer(Navn3, Beskrivelse3, AntallSengeplasser3, Antall3, AntallLedige3, Pris3));

      List<Lugarer> lugarer3 = new List<Lugarer>();
      lugarer3.Add(new Lugarer(Navn, Beskrivelse, AntallSengeplasser, Antall, AntallLedige, Pris));
      lugarer3.Add(new Lugarer(Navn2, Beskrivelse2, AntallSengeplasser2, Antall2, AntallLedige2, Pris2));
      lugarer3.Add(new Lugarer(Navn3, Beskrivelse3, AntallSengeplasser3, Antall3, AntallLedige3, Pris3));

      List<Lugarer> lugarer4 = new List<Lugarer>();
      lugarer4.Add(new Lugarer(Navn, Beskrivelse, AntallSengeplasser, Antall, AntallLedige, Pris));
      lugarer4.Add(new Lugarer(Navn2, Beskrivelse2, AntallSengeplasser2, Antall2, AntallLedige2, Pris2));
      lugarer4.Add(new Lugarer(Navn3, Beskrivelse3, AntallSengeplasser3, Antall3, AntallLedige3, Pris3));

      List<Lugarer> lugarer5 = new List<Lugarer>();
      lugarer5.Add(new Lugarer(Navn, Beskrivelse, AntallSengeplasser, Antall, AntallLedige, Pris));
      lugarer5.Add(new Lugarer(Navn1, Beskrivelse1, AntallSengeplasser1, Antall1, AntallLedige1, Pris1));
      lugarer5.Add(new Lugarer(Navn3, Beskrivelse3, AntallSengeplasser3, Antall3, AntallLedige3, Pris3));

      List<Lugarer> lugarer6 = new List<Lugarer>();
      lugarer6.Add(new Lugarer(Navn, Beskrivelse, AntallSengeplasser, Antall, AntallLedige, Pris));
      lugarer6.Add(new Lugarer(Navn1, Beskrivelse1, AntallSengeplasser1, Antall1, AntallLedige1, Pris1));
      lugarer6.Add(new Lugarer(Navn3, Beskrivelse3, AntallSengeplasser3, Antall3, AntallLedige3, Pris3));

      List<Lugarer> lugarer7 = new List<Lugarer>();
      lugarer7.Add(new Lugarer(Navn, Beskrivelse, AntallSengeplasser, Antall, AntallLedige, Pris));
      lugarer7.Add(new Lugarer(Navn1, Beskrivelse1, AntallSengeplasser1, Antall1, AntallLedige1, Pris1));
      lugarer7.Add(new Lugarer(Navn3, Beskrivelse3, AntallSengeplasser3, Antall3, AntallLedige3, Pris3));

      List<Lugarer> lugarer8 = new List<Lugarer>();
      lugarer8.Add(new Lugarer(Navn, Beskrivelse, AntallSengeplasser, Antall, AntallLedige, Pris));
      lugarer8.Add(new Lugarer(Navn1, Beskrivelse1, AntallSengeplasser1, Antall1, AntallLedige1, Pris1));
      lugarer8.Add(new Lugarer(Navn3, Beskrivelse3, AntallSengeplasser3, Antall3, AntallLedige3, Pris3));

      List<Lugarer> lugarer9 = new List<Lugarer>();
      lugarer9.Add(new Lugarer(Navn2, Beskrivelse2, AntallSengeplasser2, Antall2, AntallLedige2, Pris2));
      lugarer9.Add(new Lugarer(Navn, Beskrivelse, AntallSengeplasser, Antall, AntallLedige, Pris));
      lugarer9.Add(new Lugarer(Navn1, Beskrivelse1, AntallSengeplasser1, Antall1, AntallLedige1, Pris1));

      List<Lugarer> lugarer10 = new List<Lugarer>();
      lugarer10.Add(new Lugarer(Navn2, Beskrivelse2, AntallSengeplasser2, Antall2, AntallLedige2, Pris2));
      lugarer10.Add(new Lugarer(Navn, Beskrivelse, AntallSengeplasser, Antall, AntallLedige, Pris));
      lugarer10.Add(new Lugarer(Navn1, Beskrivelse1, AntallSengeplasser1, Antall1, AntallLedige1, Pris1));

      List<Lugarer> lugarer11 = new List<Lugarer>();
      lugarer11.Add(new Lugarer(Navn2, Beskrivelse2, AntallSengeplasser2, Antall2, AntallLedige2, Pris2));
      lugarer11.Add(new Lugarer(Navn, Beskrivelse, AntallSengeplasser, Antall, AntallLedige, Pris));
      lugarer11.Add(new Lugarer(Navn1, Beskrivelse1, AntallSengeplasser1, Antall1, AntallLedige1, Pris1));

      List<Lugarer> lugarer12 = new List<Lugarer>();
      lugarer12.Add(new Lugarer(Navn2, Beskrivelse2, AntallSengeplasser2, Antall2, AntallLedige2, Pris2));
      lugarer12.Add(new Lugarer(Navn, Beskrivelse, AntallSengeplasser, Antall, AntallLedige, Pris));
      lugarer12.Add(new Lugarer(Navn1, Beskrivelse1, AntallSengeplasser1, Antall1, AntallLedige1, Pris1));

      List<Lugarer> lugarer13 = new List<Lugarer>();
      lugarer13.Add(new Lugarer(Navn1, Beskrivelse1, AntallSengeplasser1, Antall1, AntallLedige1, Pris1));
      lugarer13.Add(new Lugarer(Navn3, Beskrivelse3, AntallSengeplasser3, Antall3, AntallLedige3, Pris3));
      lugarer13.Add(new Lugarer(Navn2, Beskrivelse2, AntallSengeplasser2, Antall2, AntallLedige2, Pris2));

      List<Lugarer> lugarer14 = new List<Lugarer>();
      lugarer14.Add(new Lugarer(Navn1, Beskrivelse1, AntallSengeplasser1, Antall1, AntallLedige1, Pris1));
      lugarer14.Add(new Lugarer(Navn3, Beskrivelse3, AntallSengeplasser3, Antall3, AntallLedige3, Pris3));
      lugarer14.Add(new Lugarer(Navn2, Beskrivelse2, AntallSengeplasser2, Antall2, AntallLedige2, Pris2));

      List<Lugarer> lugarer15 = new List<Lugarer>();
      lugarer15.Add(new Lugarer(Navn1, Beskrivelse1, AntallSengeplasser1, Antall1, AntallLedige1, Pris1));
      lugarer15.Add(new Lugarer(Navn3, Beskrivelse3, AntallSengeplasser3, Antall3, AntallLedige3, Pris3));
      lugarer15.Add(new Lugarer(Navn2, Beskrivelse2, AntallSengeplasser2, Antall2, AntallLedige2, Pris2));

      List<Lugarer> lugarer16 = new List<Lugarer>();
      lugarer16.Add(new Lugarer(Navn1, Beskrivelse1, AntallSengeplasser1, Antall1, AntallLedige1, Pris1));
      lugarer16.Add(new Lugarer(Navn3, Beskrivelse3, AntallSengeplasser3, Antall3, AntallLedige3, Pris3));
      lugarer16.Add(new Lugarer(Navn2, Beskrivelse2, AntallSengeplasser2, Antall2, AntallLedige2, Pris2));

      // Baater

      var baat1 = new Baater
      {
        Navn = "Color Fantasy",
    };

      var baat2 = new Baater
      {
        Navn = "Color Magic",
    
      };

      var baat3 = new Baater
      {
        Navn = "Color Heaven",
      
      };

      var baat4 = new Baater
      {
        Navn = "Color Titanic",
      
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
                StrekningFra = new string(strekning_OsloKobenhavn.Fra),
                StrekningTil = new string(strekning_OsloKobenhavn.Til),
                Baat = baat1,
                DatoTid = date1String,
                DatoTidTicks = date1Ticks,
                AntallLedigeBilplasser = 123,
                LedigeLugarer = lugarer,
                Aktiv = true
      };

            DateTime date2 = new DateTime(2021, 11, 5, 11, 30, 0);
            String date2String = date2.ToString();
            long date2Ticks = date2.Ticks;
            var avgang2 = new Avganger
            {
                StrekningFra = new string(strekning_KobenhavnOslo.Fra),
                StrekningTil = new string(strekning_KobenhavnOslo.Til),
                Baat = baat1,
                DatoTid = date2String,
                DatoTidTicks = date2Ticks,
                AntallLedigeBilplasser = 321,
                LedigeLugarer = lugarer2,
                Aktiv = true
            };

            DateTime date3 = new DateTime(2021, 12, 23, 11, 30, 0);
            String date3String = date3.ToString();
            long date3Ticks = date2.Ticks;
            var avgang3 = new Avganger
            {
                StrekningFra = new string(strekning_OsloKobenhavn.Fra),
                StrekningTil = new string(strekning_OsloKobenhavn.Til),
                Baat = baat1,
                DatoTid = date3String,
                DatoTidTicks = date3Ticks,
                AntallLedigeBilplasser = 666,
                LedigeLugarer = lugarer3,
                Aktiv = true
            };

           DateTime date4 = new DateTime(2021, 12, 27, 11, 30, 0);
            String date4String = date4.ToString();
            long date4Ticks = date4.Ticks;
            var avgang4 = new Avganger
            {
                StrekningFra = new string(strekning_KobenhavnOslo.Fra),
                StrekningTil = new string(strekning_KobenhavnOslo.Til),
                Baat = baat1,
                DatoTid = date4String,
                DatoTidTicks = date4Ticks,
                AntallLedigeBilplasser = 550,
                LedigeLugarer = lugarer4,
                Aktiv = true
            };

            DateTime date5 = new DateTime(2021, 08, 29, 11, 30, 0);
            String date5String = date5.ToString();
            long date5Ticks = date5.Ticks;
            var avgang5 = new Avganger
            {
                StrekningFra = new string(strekning_TrondheimBergen.Fra),
                StrekningTil = new string(strekning_TrondheimBergen.Til),
                Baat = baat2,
                DatoTid = date5String,
                DatoTidTicks = date5Ticks,
                AntallLedigeBilplasser = 400,
                LedigeLugarer = lugarer5,
                Aktiv = true
            };

            DateTime date6 = new DateTime(2021, 09, 2, 11, 30, 0);
            String date6String = date6.ToString();
            long date6Ticks = date6.Ticks;
            var avgang6 = new Avganger
            {
                StrekningFra = new string(strekning_BergenTrondheim.Fra),
                StrekningTil = new string(strekning_BergenTrondheim.Til),
                Baat = baat2,
                DatoTid = date6String,
                DatoTidTicks = date6Ticks,
                AntallLedigeBilplasser = 200,
                LedigeLugarer = lugarer6,
                Aktiv = true
            };

            DateTime date7 = new DateTime(2022, 5, 11, 11, 30, 0);
            String date7String = date7.ToString();
            long date7Ticks = date7.Ticks;
            var avgang7 = new Avganger
            {
                StrekningFra = new string(strekning_TrondheimBergen.Fra),
                StrekningTil = new string(strekning_TrondheimBergen.Til),
                Baat = baat2,
                DatoTid = date7String,
                DatoTidTicks = date7Ticks,
                AntallLedigeBilplasser = 200,
                LedigeLugarer = lugarer7,
                Aktiv = true
            };

            DateTime date8 = new DateTime(2022, 5, 13, 11, 30, 0);
            String date8String = date8.ToString();
            long date8Ticks = date8.Ticks;
            var avgang8 = new Avganger
            {
                StrekningFra = new string(strekning_BergenTrondheim.Fra),
                StrekningTil = new string(strekning_BergenTrondheim.Til),
                Baat = baat2,
                DatoTid = date8String,
                DatoTidTicks = date8Ticks,
                AntallLedigeBilplasser = 140,
                LedigeLugarer = lugarer8,
                Aktiv = true

            };

            DateTime date9 = new DateTime(2021, 02, 28, 11, 30, 0);
            String date9String = date9.ToString();
            long date9Ticks = date9.Ticks;
            var avgang9 = new Avganger
            {
                StrekningFra = new string(strekning_KielOslo.Fra),
                StrekningTil = new string(strekning_KielOslo.Til),
                Baat = baat3,
                DatoTid = date9String,
                DatoTidTicks = date9Ticks,
                AntallLedigeBilplasser = 400,
                LedigeLugarer = lugarer9,
                Aktiv = true
            };

            DateTime date10 = new DateTime(2021, 03, 4, 11, 30, 0);
            String date10String = date10.ToString();
            long date10Ticks = date10.Ticks;
            var avgang10 = new Avganger
            {
                StrekningFra = new string(strekning_OsloKiel.Fra),
                StrekningTil = new string(strekning_OsloKiel.Til),
                Baat = baat3,
                DatoTid = date10String,
                DatoTidTicks = date10Ticks,
                AntallLedigeBilplasser = 200,
                LedigeLugarer = lugarer10,
                Aktiv = true
            };

            DateTime date11 = new DateTime(2021, 12, 15, 11, 30, 0);
            String date11String = date11.ToString();
            long date11Ticks = date11.Ticks;
            var avgang11 = new Avganger
            {
                StrekningFra = new string(strekning_KielOslo.Fra),
                StrekningTil = new string(strekning_KielOslo.Til),
                Baat = baat3,
                DatoTid = date11String,
                DatoTidTicks = date11Ticks,
                AntallLedigeBilplasser = 320,
                LedigeLugarer = lugarer11,
                Aktiv = true
            };

            DateTime date12 = new DateTime(2021, 12, 20, 11, 30, 0);
            String date12String = date12.ToString();
            long date12Ticks = date12.Ticks;
            var avgang12 = new Avganger
            {
                StrekningFra = new string(strekning_OsloKiel.Fra),
                StrekningTil = new string(strekning_OsloKiel.Til),
                Baat = baat3,
                DatoTid = date12String,
                DatoTidTicks = date12Ticks,
                AntallLedigeBilplasser = 320,
                LedigeLugarer = lugarer12,
                Aktiv = true
            };

            DateTime date13 = new DateTime(2021, 06, 1, 11, 30, 0);
            String date13String = date13.ToString();
            long date13Ticks = date13.Ticks;
            var avgang13 = new Avganger
            {
                StrekningFra = new string(strekning_StavangerGeiranger.Fra),
                StrekningTil = new string(strekning_StavangerGeiranger.Til),
                Baat = baat4,
                DatoTid = date13String,
                DatoTidTicks = date13Ticks,
                AntallLedigeBilplasser = 70,
                LedigeLugarer = lugarer13,
                Aktiv = true
            };

            DateTime date14 = new DateTime(2021, 06, 6, 11, 30, 0);
            String date14String = date14.ToString();
            long date14Ticks = date14.Ticks;
            var avgang14 = new Avganger
            {
                StrekningFra = new string(strekning_GeirangerStavanger.Fra),
                StrekningTil = new string(strekning_GeirangerStavanger.Til),
                Baat = baat4,
                DatoTid = date14String,
                DatoTidTicks = date14Ticks,
                AntallLedigeBilplasser = 70,
                LedigeLugarer = lugarer14,
                Aktiv = true
            };

            DateTime date15 = new DateTime(2021, 06, 8, 11, 30, 0);
            String date15String = date15.ToString();
            long date15Ticks = date15.Ticks;
            var avgang15 = new Avganger
            {
                StrekningFra = new string(strekning_StavangerGeiranger.Fra),
                StrekningTil = new string(strekning_StavangerGeiranger.Til),
                Baat = baat4,
                DatoTid = date15String,
                DatoTidTicks = date15Ticks,
                AntallLedigeBilplasser = 85,
                LedigeLugarer = lugarer15,
                Aktiv = true
            };

            DateTime date16 = new DateTime(2021, 06, 10, 11, 30, 0);
            String date16String = date16.ToString();
            long date16Ticks = date16.Ticks;
            var avgang16 = new Avganger
            {
                StrekningFra = new string(strekning_GeirangerStavanger.Fra),
                StrekningTil = new string(strekning_GeirangerStavanger.Til),
                Baat = baat4,
                DatoTid = date16String,
                DatoTidTicks = date16Ticks,
                AntallLedigeBilplasser = 85,
                LedigeLugarer = lugarer16,
                Aktiv = true
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

    // postnummere
    Poststeder poststed1 = new Poststeder
    {
        Postnr = "1400",
        Poststed = "Ski"
    };

    Poststeder poststed2 = new Poststeder
    {
        Postnr = "1410",
        Poststed = "Kolbotn"
    };

    db.Poststeder.Add(poststed1);
    db.Poststeder.Add(poststed2);

    // lag admin-bruker
    var bruker = new Brukere();
    bruker.Brukernavn = "admin";
    string passord = "admin123";
    byte[] salt = BestillingRepository.LagSalt();
    byte[] hash = BestillingRepository.LagHash(passord, salt);
    bruker.Passord = hash;
    bruker.Salt = salt;
    db.Brukere.Add(bruker);

    db.SaveChanges();
    }
  }
}


