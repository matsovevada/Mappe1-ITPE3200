# Prosjektoppgave for ITPE3200 Webapplikasjoner: applikasjon for salg av ferje-billetter

## Om prosjektet

Webside for salg av ferje-billetter og administrering av objekter. Kunder kan velge hvilken strekning for reisen, tidspunkt for reisen og om de ønsker en retur-reise.
Etter å ha valgt dette kan kunden velge hvilke lugarer de vil bestille, hvor mange personer som skal være med på reisen og om de ønsker bilplass eller ikke.
Kunden blir så bedt om å fylle ut relevant informasjon om seg selv.
Til slutt får kunden en kvittering som viser informasjon om bestillingen.

Administratorer kan logge seg inn for å få tilgang til en egen admin-side. Admin kan legge til, slette og endre objekter tilknyttet ferje-systemet. 

Prosjektet er utviklet med .NET Core og Angular.

## Installering

1. Last ned og pakk ut zip-mappen
2. Åpne "Mappe1 ITPE3200.sln"-filen i Visual Studio
3. Åpne en terminal og naviger fra rotmappen i prosjektet til ClientApp (cd ClientApp)
4. Bruk kommandoen "npm install" i terminalen

## Innlogging som admin

Trykk på "Admin-meny" eller "Logg inn (admin)".

Brukernavn: admin
Passord: admin123

## Features vi ønsker å framheve

- Avganger blir dynamisk lastet inn på siden ut ifra hvilken strekning kunden har valgt
- Strekning for retur-reisen blir satt automatisk ut ifra hvilken strekning kunden har valgt (den motsatte strekningen)
- Det blir gjort en sjekk for at kunden velger en retur-avgang som kommer etter utreise-avgangen
- I databasen er det lagret informsjon om hvor mange av hver lugar som er ledige og hvor mange bilplasser som er igjen. Denne informasjonen blir oppdatert i databasen hver gang en kunde foretar en bestilling
- Utvalg av lugarer er knyttet til båten som kjører den valgte avgangen. Kun tilgjengelige lugarer lastes inn. Det samme gjelder for bilplasser.
- Det foretas en sjekk for at kunden har valgt nok sengeplasser i forhold til hvor mange personer kunden har valgt
- Billett-objektet bygges dynamisk etterhvert som kunden foretar valg. Billett-objektet sendes videre fra route til route før den blir lagret i databasen når kunde bekrefter bestillingen.

## Testing og coverage

Alle metoder i controlleren som er relatert til admin-siden er enhetstestet.
Coverage av controlleren er på 52,7 %.

## Adminsider

    Lugar:
        Legge til:
            Lag ny lugar som kan legges til i avganger. Input valideres både på frontend og backend. 

        Endre / slett
            Endre eksisterende lugarer. Input valideres på samme måte som ved oppretting av ny lugar. 
            Slett eksisterende lugarer.


    Avgang:
        Legge til
            Opprett ny avgang. Admin velger båt, strekning, lugarer, bilplasser og tidspunkt. Avgangen kan også settes til aktiv eller inaktiv ved oppretting. Inaktive avganger kan ikke velges av bruker ved bestilling. Validering på front- og backend. 

        Endre / slett
            Administrer eksisterende avgang. Admin kan endre båt, strekning, lugarer, bilplasser og tidspunkt for valgt avgang. Avganger kan også slettes. Aktiv status på valgt avgang kan også endres. Validering på front- og backend.


    Båt
        Legg til / Endre / Slett:
            Admin kan opprette ny båt i databasen. Admin kan også endre navn eller slett båter fra databasen. Validering på alle inputfelter i frontend og full inputvalidering i backend. 


    Strekning
        Legg til / Endre / Slett
            Admin kan opprette ny strekninger for bruk i avganger. Eksiterende strekninger kan også endres eller slettes. Validering på alle inputfelter i frontend og full inputvalidering i backend. 


    Kunde
        Endre / Slett
            Admin kan endre kundeinformasjon og slette eksisterende kunder. Inputvalidering på frontend og backend.

    Billett
        Slett
            Admin kan slette valgt billett fra databasen. 

    Postnummer
        Legg til / Endre / Slett
            Admin kan adminstrere poststeder i databasen ved å legge til nye, endre eksisterende eller slette valgt felt fra databasen. Inputvalidering på alle felter og validering i backend

## Referanser til kode som ikke er egen-utviklet

- Koden for form-en for registrering av kunder (kundeForm.html og kundeForm.ts) er laget med utgangspunkt i "Kunde SPA med routing"-prosjektet til faglærer
- Backend-metodene tilknyttet innlogging er kopiert fra "KundeApp2-med-logginn-sessions"-prosjektet til faglærer