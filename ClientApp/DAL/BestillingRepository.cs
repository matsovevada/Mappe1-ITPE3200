using Mappe1_ITPE3200.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Mappe1_ITPE3200.ClientApp.DAL
{
  public class BestillingRepository : IBestillingRepository
  {
    private readonly DatabaseContext _db;

    public BestillingRepository(DatabaseContext db)
    {
      _db = db;
    }


    [HttpGet]
    public async Task<List<Strekning>> HentAlleStrekninger()

    {
      List<Strekning> alleStrekninger = await _db.Strekninger.Select(s => new Strekning
      {
        Id = s.Id,
        Fra = s.Fra,
        Til = s.Til
      }).ToListAsync();

      return alleStrekninger;
    }


    [HttpGet]
    public async Task<List<Avganger>> HentAlleAvganger(Strekning valgtStrekning)

    {
      List<Avganger> alleAvganger = await _db.Avganger.Where(a => (a.Strekning.Til.Equals(valgtStrekning.Til) && a.Strekning.Fra.Equals(
        valgtStrekning.Fra))).ToListAsync();
      return alleAvganger;
    }

    [HttpGet]
    public async Task<Avganger> HentValgtAvgang(int id)
    {
      Avganger avgang = await _db.Avganger.FindAsync(id);
      return avgang;
    }

    [HttpGet]
    public async Task<Baater> HentBaat(int id)
    {
      Baater baat = await _db.Baater.FindAsync(id);
      return baat;
    }

    [HttpPost]
    public async Task<int> LagreKunde(Kunde innKunde)
    {
      try
        
      {
        Kunder kunde = new Kunder();
        kunde.Fornavn = innKunde.Fornavn;
        kunde.Etternavn = innKunde.Etternavn;
        kunde.Adresse = innKunde.Adresse;
        kunde.Epost = innKunde.Epost;
        kunde.Telefonnummer = innKunde.Telefonnummer;
        Console.WriteLine("TEST1");
        Console.WriteLine(innKunde.Poststed);

        var sjekkPostnr = await _db.Poststeder.FindAsync(innKunde.Postnr);
        if (sjekkPostnr == null)
        {
          Console.WriteLine("HERRR!");
          var nyttPoststed = new Poststeder();
          nyttPoststed.Poststed = innKunde.Poststed;
          nyttPoststed.Postnr = innKunde.Postnr;
          Console.WriteLine(nyttPoststed.Poststed);
          _db.Poststeder.Add(nyttPoststed);
          await _db.SaveChangesAsync();

          kunde.Poststed = nyttPoststed;

        }
        else
        {
          var poststedFraDB = new Poststeder();
          poststedFraDB = sjekkPostnr;
          kunde.Poststed = poststedFraDB;
        }

        _db.Kunder.Add(kunde);
        await _db.SaveChangesAsync();
        return kunde.Id;

      } catch
      {
        return -1;
      }
    }

    [HttpGet]
    public async Task<Kunder> HentKunde(int id)
    {
      Kunder kunde = await _db.Kunder.FindAsync(id);
      return kunde;
    }

    [HttpPost]
    public async Task<int> LagreBillett(Billett innBillett)
    {
      try
      {
        //Kopierer innListe til ny liste av type Lugarer.
        List<Lugarer> lug = new List<Lugarer>();
        innBillett.lugarer.ForEach(lugar => lug.Add((Lugarer)lugar));

        List<Lugarer> lugRetur;
        Billetter billett = new Billetter();

        if (innBillett.lugarerRetur != null)
        {
          lugRetur = new List<Lugarer>();
          innBillett.lugarerRetur.ForEach(lugar => lugRetur.Add((Lugarer)lugar));
          billett.lugarerRetur = lugRetur;
          billett.BilplassRetur = innBillett.Bilplass;
          billett.AvgangIdRetur = innBillett.AvgangIdRetur;
          billett.AntallPersonerRetur = innBillett.AntallPersonerRetur;
        }
        else
        {
          billett.lugarerRetur = null;
          billett.BilplassRetur = false;
          billett.AvgangIdRetur = null;
          billett.AntallPersonerRetur = null;
        }



        billett.AvgangId = innBillett.AvgangId;
        billett.KundeId = innBillett.KundeId;
        billett.AntallPersoner = innBillett.AntallPersoner;
        billett.Bilplass = innBillett.Bilplass;
        billett.lugarer = lug;
        billett.TotalPris = innBillett.TotalPris;
        _db.Billetter.Add(billett);
        await _db.SaveChangesAsync();
        return billett.Id;

      } catch
      {
        return -1;
      }
    }

    [HttpGet]
    public async Task<Billetter> HentBillett(int id)
    {
      Billetter billett = await _db.Billetter.FindAsync(id);
      return billett;
    }

    public async Task<bool> DecrementBilplass(int id)
    {
      try
      {
        Avganger avgang = await _db.Avganger.FindAsync(id);
        avgang.AntallLedigeBilplasser--;
        await _db.SaveChangesAsync();
        return true;
      }
      catch
      {
        return false;
      }
    }

    public async Task<bool> OppdaterAntallLedigeLugarer(int id, List<Lugar> lugarer)
    {
      try
      {
        Avganger avgang = await _db.Avganger.FindAsync(id);

        lugarer.ForEach(lugar =>
        {
          avgang.LedigeLugarer.ForEach(lugarIAvgang =>
          {
            if (lugar.Navn.Equals(lugarIAvgang.Navn))
            {
              lugarIAvgang.AntallLedige--;
            }
          });
        });

        await _db.SaveChangesAsync();
        return true;
      }
      catch
      {
        return false;
      }   
    }
  }
}





//Alt søke metode, filter etter 
//new Avganger
//{
//  Id = a.Id,
//  Strekning = a.Strekning,
//  Baat = a.Baat,
//  DatoTid = a.DatoTid,
//  AntallLedigeBilplasser = a.AntallLedigeBilplasser,
//  LedigeLugarer = a.LedigeLugarer,
//}).ToListAsync();

//alleAvganger.

//alt 2, hvis models skal brukes
// Strekning = new Strekninger
//        {
//          Id = a.Strekning.Id,
//          Fra = a.Strekning.Fra,
//          Til = a.Strekning.Til
//        },
//        Baat = new Baater
//        {
//          Id = a.Baat.Id,
//          Navn = a.Baat.Navn,
//          Lugarer = new List<Lugar>()
//          {

//          },
//          AntallBilplasser = a.Baat.AntallBilplasser
//        },
//        DatoTid = a.DatoTid,
//        AntallLedigeBilplasser = a.AntallLedigeBilplasser,
//        LedigeLugarer = new List<Lugar>()
//      }).ToListAsync();
///*
