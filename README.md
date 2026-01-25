# Invoice Management System - Backend  
RESTful API backend pro správu faktur a subjektů postavený na **ASP.NET Core**. Projekt byl původně vytvořen pro React frontend, ale postupně jsem ho rozšířil o podporu **Blazor WebAssembly** jako alternativního SPA klienta. Architektura se sdílenou vrstvou (Shared) pro eliminaci duplicitních modelů je aktuálně work in progress.

## 📋 Popis projektu  
Fullstack systém pro správu fakturační evidence s modulární architekturou umožňující bezproblémovou integraci různých frontendových technologií. Backend implementuje kompletní CRUD operace a poskytuje REST API endpoint pro správu osob/firem a faktur.  

## 🏗️ Architektura  
Projekt je rozdělen do několika vrstev podle best practices:  

Invoices.Api - REST API vrstva s controllery a routingem  
Invoices.Data - Datová vrstva s Entity Framework Core  
Invoices.Shared - Sdílené modely a DTOs pro znovupoužití napříč klienty  
Invoices.Blazor - Blazor WebAssembly frontend (alternativa k React klientu)  

## ✨ Hlavní funkce
### Správa subjektů

✅ Vytvoření nové osoby/firmy  
✅ Zobrazení seznamu všech subjektů  
✅ Detail konkrétního subjektu  
✅ Úprava existujícího subjektu  
✅ Smazání subjektu (soft delete)  

### Správa faktur

✅ Vytvoření nové faktury  
✅ Výpis faktur s pokročilým filtrováním  
✅ Detail faktury včetně kompletních údajů dodavatele a odběratele  
✅ Úprava existující faktury  
✅ Smazání faktury  

### Statistiky

✅ Celkové statistiky faktur (aktuální rok, celkový součet, počet faktur)  
✅ Statistiky příjmů jednotlivých subjektů  

### Pokročilé funkce

🔍 Filtrování faktur (podle dodavatele, odběratele, produktu, ceny)  
📊 Výpis vystavených/přijatých faktur podle IČ  
🗄️ Soft delete pro zachování integrity dat  

## 🛠️ Technologie

ASP.NET Core  
Entity Framework Core  
SQLite   
AutoMapper - Mapování mezi entitami a DTOs    
Swagger - Automatická dokumentace API    
Blazor WebAssembly - Alternativní SPA frontend    
MudBlazor - UI komponenty pro Blazor    



## 🎯 Datový model  

### Person (Osoba/Firma)  
- Základní údaje (název, IČ, DIČ)  
- Bankovní údaje (číslo účtu, IBAN)  
- Kontaktní údaje (telefon, email)  
- Adresa (ulice, PSČ, město, země)  

### Invoice (Faktura)  
- Číslo faktury  
- Odkazy na dodavatele a odběratele (Person)  
- Data (datum vystavení, datum splatnosti)  
- Položky (produkt, cena, DPH)  
- Poznámka  
