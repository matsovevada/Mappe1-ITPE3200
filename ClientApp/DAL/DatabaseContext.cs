using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace Mappe1_ITPE3200.Models
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
        public DbSet<Billett> Billetter { get; set; }
        public DbSet<Kunde> Kunder { get; set; }
        public DbSet<Avgang> Avganger { get; set; }
        public DbSet<Baat> Baater { get; set; }
        public DbSet<Strekning> Strekninger { get; set; }
        public DbSet<Poststed> Poststeder { get; set; }


        public class Lugar
        {
            public string Type { get; set; }
            public int AntallSengeplasser { get; set; }
            public int AntallLedigeAvType { get; set; }
            public int Pris { get; set; }
        }

        public class Baat
        {
            [Key]
            public int BaatID { get; set; }
            public string Navn { get; set; }
            public virtual List<Lugar> Lugarer { get; set; }
            public int AntallBilplasser { get; set; }
        }

        public class Avgang
        {
            [Key]
            public int Id { get; set; }
            public int StrekningsID { get; set; }
            public int BaatID { get; set; }
            public DateTime DatoTid { get; set; }
            public int AntallLedigeBilplasser { get; set; }
            public virtual List<Lugar> LedigeLugarer { get; set; }
        }

        public class Poststed
        {
            [Key]
            public string Postnr { get; set; }
            public string PostSted { get; set; }
        }

        public class Strekning
        {
            [Key]
            public int StrekningsID { get; set; }
            public string Fra { get; set; }
            public string Til { get; set; }

        }
    }
}
