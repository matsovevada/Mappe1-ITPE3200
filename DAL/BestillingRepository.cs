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
            List<Avganger> alleAvganger = await _db.Avganger.Where(a => (a.StrekningTil.Equals(valgtStrekning.Til) && a.StrekningFra.Equals(
              valgtStrekning.Fra))).ToListAsync();
            return alleAvganger;
        }

        [HttpGet]
        public async Task<List<Avganger>> HentAktiveAvganger(Strekning valgtStrekning)

        {
            List<Avganger> alleAvganger = await _db.Avganger.Where(a => (a.StrekningTil.Equals(valgtStrekning.Til) && a.StrekningFra.Equals(
              valgtStrekning.Fra) && a.Aktiv)).ToListAsync();
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

        [HttpGet]
        public async Task<Baater> HentBaatPaaNavn(String baatnavn)
        {
            Baater baat = await _db.Baater.FirstOrDefaultAsync(b => b.Navn.Equals(baatnavn));
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

            }
            catch
            {
                return -1;
            }
        }

        [HttpGet]
        public async Task<Kunde> HentKunde(int id)
        {
            Kunder kunde = await _db.Kunder.FindAsync(id);

            Kunde returKunde = new Kunde()
            {
                Id = kunde.Id,
                Fornavn = kunde.Fornavn,
                Etternavn = kunde.Etternavn,
                Adresse = kunde.Adresse,
                Postnr = kunde.Poststed.Postnr,
                Poststed = kunde.Poststed.Poststed,
                Telefonnummer = kunde.Telefonnummer,
                Epost = kunde.Epost
            };

            return returKunde;
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

            }
            catch
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

        public async Task<bool> SlettStrekning(int id)
        {
            try
            {
                Strekninger strekning = await _db.Strekninger.FindAsync(id);

                //SLETTE AVGANGER KNYTTET TIL STREKNINGEN?
                // slett avganger som har strekningen
                /*   await _db.Avganger.ForEachAsync(avgang =>
                   {
                       if (avgang.Strekning.Id == strekning.Id)
                       {
                           _db.Avganger.Remove(avgang);
                       }
                   });*/

                _db.Strekninger.Remove(strekning);

                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EndreStrekning(int id, string nyStrekningFra, string nyStrekningTil)
        {
            try
            {
                Strekninger strekning = await _db.Strekninger.FindAsync(id);
                strekning.Fra = nyStrekningFra;
                strekning.Til = nyStrekningTil;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> LagreStrekning(string StrekningFra, string StrekningTil)
        {
            try
            {
                Strekninger strekning = new Strekninger();
                strekning.Fra = StrekningFra;
                strekning.Til = StrekningTil;

                await _db.Strekninger.AddAsync(strekning);
                await _db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Poststeder>> HentAllePoststeder()
        {
            try
            {
                List<Poststeder> allePoststeder = await _db.Poststeder.ToListAsync();
                return allePoststeder;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> SlettPoststed(string postnummer)
        {
            try
            {
                Poststeder poststed = await _db.Poststeder.FindAsync(postnummer);
                _db.Poststeder.Remove(poststed);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EndrePoststed(string postnummer, string nyttPoststed)
        {
            try
            {
                Poststeder poststed = await _db.Poststeder.FindAsync(postnummer);
                poststed.Poststed = nyttPoststed;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> LagrePoststed(string postnummer, string poststed)
        {
            try
            {
                Poststeder nyttPoststed = new Poststeder
                {
                    Postnr = postnummer,
                    Poststed = poststed
                };

                await _db.Poststeder.AddAsync(nyttPoststed);
                await _db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }



        public async Task<bool> LagreLugar(string navn, string beskrivelse, int antallSengeplasser, int antall, int antallLedige, int pris)
        {
            try
            {

                LugarMaler lugarMal = new LugarMaler()
                {
                    Navn = navn,
                    Beskrivelse = beskrivelse,
                    AntallSengeplasser = antallSengeplasser,
                    Antall = antall,
                    AntallLedige = antallLedige,
                    Pris = pris
                };

                await _db.LugarMaler.AddAsync(lugarMal);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }



        //Henter alle båter fra DB
        [HttpGet]
        public async Task<List<Baater>> HentAlleBaater()
        {
            try
            {
                List<Baater> alleBaater = await _db.Baater.ToListAsync();
                return alleBaater;
            }
            catch
            {
                return null;
            }
        }

        [HttpDelete]
        public async Task<bool> slettBaat(int id)
        {
            try
            {
                Baater slettBaat = await _db.Baater.FindAsync(id);
                List<Avganger> alleAvgangerMedValgtBaat = await _db.Avganger.Where(a => a.Baat.Equals(slettBaat)).ToListAsync();
                alleAvgangerMedValgtBaat.ForEach(avgang => _db.Avganger.Remove(avgang));
                await _db.SaveChangesAsync();

                _db.Baater.Remove(slettBaat);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPut]
        public async Task<bool> endreBaat(int id, String navn)
        {
            try
            {
                Baater endreBaat = await _db.Baater.FindAsync(id);
                endreBaat.Navn = navn;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        public async Task<bool> lagreBaat(String navn)
        {
            try
            {
                Baater nyBaat = new Baater();
                nyBaat.Navn = navn;
                await _db.Baater.AddAsync(nyBaat);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


        [HttpGet]
        public async Task<List<Kunder>> HentAlleKunder()
        {
            try
            {
                List<Kunder> alleKunder = await _db.Kunder.ToListAsync();
                return alleKunder;
            }
            catch
            {
                return null;
            }
        }

        [HttpDelete]
        public async Task<bool> slettKunde(int id)
        {
            try
            {
                Kunder slettKunde = await _db.Kunder.FindAsync(id);

                _db.Kunder.Remove(slettKunde);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<LugarMaler>> HentAlleLugarer()
        {
            try
            {
                return await _db.LugarMaler.ToListAsync();
            }
            catch
            {
                return null;
            }
        }


        public async Task<LugarMaler> HentLugarPaaNavn(string navn)
        {
            try
            {
                return await _db.LugarMaler.FirstOrDefaultAsync(lug => lug.Navn.Equals(navn));
            }
            catch
            {
                return null;
            }
        }

        [HttpPut]
        public async Task<bool> endreKunde(Kunde k)
        {
            try

            {
                Kunder kunde = await _db.Kunder.FindAsync(k.Id);
                kunde.Fornavn = k.Fornavn;
                kunde.Etternavn = k.Etternavn;
                kunde.Adresse = k.Adresse;
                kunde.Telefonnummer = k.Telefonnummer;
                kunde.Epost = k.Epost;


                //Sjekker om postnr/poststed er endret. Sjekkes mot DB og settes til verdi fra DB hvis det er registrert. Oppretter
                //nytt felt i Poststeder DB hvis det ikke finnes. Verdi settes til kunde.
                if ((k.Postnr != kunde.Poststed.Postnr) && (k.Poststed != kunde.Poststed.Poststed)) {
                    var sjekkPostnr = await _db.Poststeder.FindAsync(k.Postnr);
                    if (sjekkPostnr == null)
                    {
                        var nyttPoststed = new Poststeder();
                        nyttPoststed.Poststed = k.Poststed;
                        nyttPoststed.Postnr = k.Postnr;
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
                }

                _db.Kunder.Add(kunde);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete]
        public async Task<bool> SlettLugar(int id)
        {
            try
            {
                LugarMaler slettLugarMal = await _db.LugarMaler.FindAsync(id);

                _db.LugarMaler.Remove(slettLugarMal);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


        [HttpPost]
        public async Task<bool> lagreAvgang(string baat, string strekningFra, string strekningTil, string datoTidDag, string datoTidMnd, string datoTidAar, string datoTidTime, string datoTidMin, string antallLedigeBilplasser, string lugarer, string aktiv)
        {
            try
            {
                Avganger nyAvgang = new Avganger();
                Baater baatFraDB = await HentBaatPaaNavn(baat);

                nyAvgang.Baat = baatFraDB;

                nyAvgang.StrekningFra = strekningFra;
                nyAvgang.StrekningTil = strekningTil;


                DateTime date = new DateTime(Int32.Parse(datoTidAar), Int32.Parse(datoTidMnd), Int32.Parse(datoTidDag), Int32.
                Parse(datoTidTime), Int32.Parse(datoTidMin), 0);

                string dateString = date.ToString();
                long date1Ticks = date.Ticks;
                nyAvgang.DatoTid = dateString;
                nyAvgang.DatoTidTicks = date1Ticks;

                List<Lugarer> lugarListe = new List<Lugarer>();
                string[] lugarerSplit = lugarer.Split(",");
                foreach (string lug in lugarerSplit)
                {

                    LugarMaler lugarFraDB = await HentLugarPaaNavn(lug);
                    Lugarer lugarTilAvgang = new Lugarer(lugarFraDB.Navn, lugarFraDB.Beskrivelse, lugarFraDB.AntallSengeplasser, lugarFraDB.Antall, lugarFraDB.AntallLedige, lugarFraDB.Pris);
                    lugarListe.Add(lugarTilAvgang);
                }
                nyAvgang.LedigeLugarer = lugarListe;


                nyAvgang.AntallLedigeBilplasser = Int32.Parse(antallLedigeBilplasser);

                nyAvgang.Aktiv = Convert.ToBoolean(aktiv);

                await _db.Avganger.AddAsync(nyAvgang);  
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }



        [HttpPut]
        public async Task<bool> endreAvgang(string id, string baat, string strekningFra, string strekningTil, string datoTidDag, string datoTidMnd, string datoTidAar, string datoTidTime, string datoTidMin, string antallLedigeBilplasser, string lugarer, string aktiv)
        {
            try
            {
                Avganger endreAvgang = await _db.Avganger.FindAsync(Int32.Parse(id));

                Baater baatFraDB = await HentBaatPaaNavn(baat);

                endreAvgang.Baat = baatFraDB;

                endreAvgang.StrekningFra = strekningFra;
                endreAvgang.StrekningTil = strekningTil;


                DateTime date = new DateTime(Int32.Parse(datoTidAar), Int32.Parse(datoTidMnd), Int32.Parse(datoTidDag), Int32.
                Parse(datoTidTime), Int32.Parse(datoTidMin), 0);

                string dateString = date.ToString();
                long date1Ticks = date.Ticks;
                endreAvgang.DatoTid = dateString;
                endreAvgang.DatoTidTicks = date1Ticks;

                List<Lugarer> lugarListe = new List<Lugarer>();
                string[] lugarerSplit = lugarer.Split(",");
                foreach (string lug in lugarerSplit)
                {

                    LugarMaler lugarFraDB = await HentLugarPaaNavn(lug);
                    Lugarer lugarTilAvgang = new Lugarer(lugarFraDB.Navn, lugarFraDB.Beskrivelse, lugarFraDB.AntallSengeplasser, lugarFraDB.Antall, lugarFraDB.AntallLedige, lugarFraDB.Pris);
                    lugarListe.Add(lugarTilAvgang);
                }
                endreAvgang.LedigeLugarer = lugarListe;

                endreAvgang.AntallLedigeBilplasser = Int32.Parse(antallLedigeBilplasser);

                endreAvgang.Aktiv = Convert.ToBoolean(aktiv);

                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete]
        public async Task<bool> SlettAvgang(int id)
        {
            try
            {
                Avganger slettAvgang = await _db.Avganger.FindAsync(id);
                _db.Avganger.Remove(slettAvgang);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Avganger>> HentAlleAvganger()
        {
            try
            {
                return await _db.Avganger.ToListAsync();
            }
            catch
            {
                return null;
            }
        }
    }


}