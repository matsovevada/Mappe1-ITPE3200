using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mappe1_ITPE3200.Models;
using Microsoft.EntityFrameworkCore;


namespace Mappe1_ITPE3200.ClientApp.DAL

{
  public class DatabaseContext : DbContext
  {
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
      Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }


    //Tabeller
    public DbSet<Billetter> Billetter { get; set; }
    public DbSet<Kunder> Kunder { get; set; }
    public DbSet<Avganger> Avganger { get; set; }
    public DbSet<Baater> Baater { get; set; }
    public DbSet<Strekninger> Strekninger { get; set; }
    public DbSet<Poststeder> Poststeder { get; set; }
  }


  public class Billetter
  {
      public int Id { get; set; }
      public int AvgangId { get; set; }
      public int? AvgangIdRetur { get; set; }
      public int KundeId { get; set; }
      public int AntallPersoner { get; set; }
      public int? AntallPersonerRetur { get; set; }
      public double TotalPris { get; set; }
      public bool Bilplass { get; set; }
      public bool BilplassRetur { get; set; }
      public virtual List<Lugarer> lugarer { get; set; }
      public virtual List<Lugarer> lugarerRetur { get; set; }
  }

  public class Kunder
  {
      [Key]
      public int Id { get; set; }
      public String Fornavn { get; set; }
      public String Etternavn { get; set; }
      public String Adresse { get; set; }
      virtual public Poststeder Poststed { get; set; }
      public String Telefonnummer { get; set; }
      public String Epost { get; set; }
  }


  public class Lugarer
  {
      [Key]
      public int Id { get; set; }
      public string Navn { get; set; }
      public string Beskrivelse { get; set; }
      public int AntallSengeplasser { get; set; }
      public int Antall { get; set; }
      public int AntallLedige { get; set; }
      public int Pris { get; set; }

    public static explicit operator Lugarer(Lugar v)
    {
      Lugarer lug = new Lugarer();
      lug.Navn = v.Navn;
      lug.Beskrivelse = v.Beskrivelse;
      lug.AntallSengeplasser = v.AntallSengeplasser;
      lug.Antall = v.Antall;
      lug.AntallLedige = v.Antall;
      lug.Pris = v.Pris;
      return lug;
    }
  }

  public class Baater
  {
      [Key]
      public int Id { get; set; }
      public string Navn { get; set; }
      public virtual List<Lugarer> Lugarer { get; set; }
      public int AntallBilplasser { get; set; }
  }

  public class Avganger
  {
      [Key]
      public int Id { get; set; }
      public virtual Strekninger Strekning { get; set; }
      public virtual Baater Baat { get; set; }
      public string DatoTid { get; set; }
      public long DatoTidTicks { get; set; }
      public int AntallLedigeBilplasser { get; set; }
      public virtual List<Lugarer> LedigeLugarer { get; set; }
  }

  public class Poststeder
  {
      [Key]
      public string Postnr { get; set; }
      public string Poststed { get; set; }
  }

  public class Strekninger
  {
      [Key]
      public int Id { get; set; }
      public string Fra { get; set; }
      public string Til { get; set; }
  }
}
