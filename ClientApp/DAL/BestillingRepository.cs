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
    public async Task<List<Avganger>> HentAlleAvganger(Strekninger valgtStrekning)

    {
      List<Avganger> alleAvganger = await _db.Avganger.Where(a => a.Strekning == valgtStrekning).ToListAsync();
      return alleAvganger;
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
