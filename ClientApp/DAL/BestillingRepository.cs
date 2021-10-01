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

        /*Sjekker om postnummer finnes i DB fra før. Poststed (postnr + poststed) lagres i DB og settes til kunde. Hvis postnummer
        finnes settes kundes poststed til det som ligger i DB fra før. */
        var sjekkPostnr = await _db.Poststeder.FindAsync(innKunde.Postnr);
        if (sjekkPostnr == null)
        {
          var nyttPoststed = new Poststeder();
          nyttPoststed.Poststed = innKunde.Poststed;
          nyttPoststed.Postnr = innKunde.Postnr;
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
        //Kopierer innListe til ny liste av type Lugarer. Metoden tar inn type Lugar og må konverteres (castes) til Lugarer.
        List<Lugarer> lug = new List<Lugarer>();
        innBillett.lugarer.ForEach(lugar => lug.Add((Lugarer)lugar));

        List<Lugarer> lugRetur;
        Billetter billett = new Billetter();

        //Setter returfelter hvis billetten er bestilt med retur
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


    //Setter antall ledige bilplasser for avgangen
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

    //Setter antall ledige lugarer for avgangen
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