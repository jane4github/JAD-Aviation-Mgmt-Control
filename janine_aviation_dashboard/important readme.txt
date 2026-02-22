Architect: Jana Janine Servais

This Project is powered by Microsoft

Konfigurieren Sie IIS für .NET Entity Framework .NET CORE
https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/?view=aspnetcore-10.0

Laden Sie den SQL - Lite Browser herunter
erstellen Sie eine Datenbank Aviation.db

Erstellen kopieren Sie die Tabelle Flights mit folgenden Text in als SQL Ausdruck in ihre Management UI

CREATE TABLE "Flights" (
	"Id"	INTEGER NOT NULL,
	"total_Duration"	TEXT,
	"FlightDate"	TEXT,
	"Year"	TEXT,
	"Aircraft"	TEXT,
	"Aircraft_ID"	TEXT,
	"Airplane_Single"	TEXT,
	"Airplane_Multi"	TEXT,
	"Hrs_Rotorcraft"	TEXT,
	"SOLO"	TEXT,
	"Hrs_Dual_Received"	TEXT,
	"Hrs_PIC_Received"	TEXT,
	"Hrs_Secnd_Command_Received"	TEXT,
	"Hrs_CFI_Received"	TEXT,
	"Ground"	TEXT,
	"Flight_Route"	TEXT,
	"Pilots"	TEXT,
	"Rules"	TEXT,
	"Landings"	TEXT,
	"Starts"	TEXT,
	"Cond_Day"	TEXT,
	"Cond_Night"	TEXT,
	"Cond_XCountry"	TEXT,
	"Cond_Instrument"	TEXT,
	"Cond_Sim_Instr"	TEXT,
	"Lnd_Day"	TEXT,
	"Lnd_Night"	TEXT,
	"Hrs_total_Dual"	TEXT,
	"Remarks"	TEXT,
	"Pilot_CFI_Names"	TEXT,
	"Sim_IFR"	TEXT,
	CONSTRAINT "PK_Flights" PRIMARY KEY("Id" AUTOINCREMENT)
)