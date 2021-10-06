# Prosjektoppgave for ITPE3200 Webapplikasjoner: applikasjon for salg av ferje-billetter

## Om prosjektet

Webside for salg av ferje-billetter. Kunder kan velge hvilken strekning for reisen, tidspunkt for reisen og om de ønsker en retur-reise.
Etter å ha valgt dette kan kunden velge hvilke lugarer de vil bestille, hvor mange personer som skal være med på reisen og om de ønsker bilplass eller ikke.
Kunden blir så bedt om å fylle ut relevant informasjon om seg selv.
Til slutt får kunden en kvittering som viser informasjon om bestillingen.

Prosjektet er utviklet med .NET Core og Angular.

## Installering

1. Last ned og pakk ut zip-mappen
2. Åpne "Mappe1 ITPE3200.sln"-filen i Visual Studio
3. Åpne en terminal og naviger fra rotmappen i prosjektet til ClientApp (cd ClientApp)
4. Bruk kommandoen "npm install" i terminalen

## Features vi ønsker å framheve

- Avganger blir dynamisk lastet inn på siden ut ifra hvilken strekning kunden har valgt
- Strekning for retur-reisen blir satt automatisk ut ifra hvilken strekning kunden har valgt (den motsatte strekningen)
- Det blir gjort en sjekk for at kunden velger en retur-avgang som kommer etter utreise-avgangen
- I databasen er det lagret informsjon om hvor mange av hver lugar som er ledige og hvor mange bilplasser som er igjen. Denne informasjonen blir oppdatert i databasen hver gang en kunde foretar en bestilling
- Utvalg av lugarer er knyttet til båten som kjører den valgte avgangen. Kun tilgjengelige lugarer lastes inn. Det samme gjelder for bilplasser.
- Det foretas en sjekk for at kunden har valgt nok sengeplasser i forhold til hvor mange personer kunden har valgt
- Billett-objektet bygges dynamisk etterhvert som kunden foretar valg. Billett-objektet sendes videre fra route til route før den blir lagret i databasen.

## Referanser til kode som ikke er egen-utviklet

- Koden for form-en for registrering av kunder (kundeForm.html og kundeForm.ts) er tatt fra "Kunde SPA med routing"-prosjektet til faglærer