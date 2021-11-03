using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Mappe1_ITPE3200.Models;
using Microsoft.EntityFrameworkCore;


namespace Mappe1_ITPE3200.ClientApp.DAL

{
    [ExcludeFromCodeCoverage]
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
    public DbSet<Lugarer> Lugarer { get; set; }
    public DbSet<LugarMaler> LugarMaler { get; set; }
    public DbSet<Brukere> Brukere { get; set; }
  }


  public class Billetter
  {
      public int Id { get; set; }
      public int? AvgangId { get; set; }
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

    public Lugarer(string Navn, string Beskrivelse, int AntallSengeplasser, int Antall, int AntallLedige, int Pris)
    {
      this.Navn = Navn;
      this.Beskrivelse = Beskrivelse;
      this.AntallSengeplasser = AntallSengeplasser;
      this.Antall = Antall;
      this.AntallLedige = AntallLedige;
      this.Pris = Pris;
    }

    //Gj?r det mulig ? caste fra Lugar til Lugarer 
    public static explicit operator Lugarer(Lugar v)
    {
      Lugarer lug = new Lugarer(v.Navn, v.Beskrivelse, v.AntallSengeplasser, v.Antall, v.AntallLedige, v.Pris);
      return lug;
    }
  }

  public class LugarMaler
    {
        [Key]
        public int Id { get; set; }
        public string Navn { get; set; }
        public string Beskrivelse { get; set; }
        public int AntallSengeplasser { get; set; }
        public int Antall { get; set; }
        public int AntallLedige { get; set; }
        public int Pris { get; set; }
    }

  public class Baater
  {
      [Key]
      public int Id { get; set; }
      public string Navn { get; set; }

  }

  public class Avganger
  {
      [Key]
      public int Id { get; set; }
      public string StrekningFra { get; set; }
      public string StrekningTil { get; set; }
      public virtual Baater Baat { get; set; }
      public string DatoTid { get; set; }
      public long DatoTidTicks { get; set; }
      public int AntallLedigeBilplasser { get; set; }
      public virtual List<Lugarer> LedigeLugarer { get; set; }
      public bool Aktiv { get; set; }
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

    public class Brukere
    {
        public int Id { get; set; }
        public string Brukernavn { get; set; }
        public byte[] Passord { get; set; }
        public byte[] Salt { get; set; }
    }
}
