using janine_aviation.Services;
using janine_aviation.Models;   
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Runtime.InteropServices;
using SQLitePCL;

[Route("SuperUser")]
public class AdminController : Controller
{

    public readonly IAviationServices _aviationService;
    
    public AdminController(IAviationServices aviationService)
    {
        _aviationService = aviationService;
    }




    public async Task<IActionResult> SuperUser()
    {
        return View("~/Views/SuperUser/SuperUser.cshtml");
    }

    /*
    public IActionResult Index()
    {
        return View("~/Views/SuperUser/SuperUser.cshtml");
    }*/


    // --- API ENDPUNKTE FÜR DIE KONSOLE ---
    [HttpGet("/SuperUser/List")]
    public async Task<IActionResult> GetFlights([FromQuery] int page = 1)
    {
        var flights = await _aviationService.GetFlightsPageAsync(page);
        //var flights = await _aviationService.GetAllFlightsAsync();
        return Ok(flights);
    }


         
     [HttpGet("/SuperUser/search")]

    public async Task<IActionResult> SearchFlights([FromQuery] string query,[FromQuery] int page = 1,[FromQuery]  int pageSize = 5)
    {

        //var flights = await _aviationService.SearchFlightsPagedAsync(query, page);
        //return Ok(flights);

        
        if (string.IsNullOrWhiteSpace(query))
        {
           return await GetFlights(page); // Fallback zur normalen Liste wenn Suche leer
        }

        //Paginierung kombinieren
        var filteredflights = await _aviationService.SearchFlightsPagedAsync(query,page,pageSize);
        //var flights = await _aviationService.GetFlightsPageAsync(page);
        //var allflights = await _aviationService.GetAllFlightsAsync();

        if (string.IsNullOrEmpty(query))
        {
            return Ok(new List<Flight>());

        }
       /*
       var filtered = flights.Where(f =>
            (f.Flight_Route != null && f.Flight_Route.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
            (f.FlightDate != null && f.FlightDate.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
            (f.Remarks != null && f.Remarks.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
            (f.Aircraft_ID != null && f.Aircraft_ID.Contains(query, StringComparison.OrdinalIgnoreCase))).ToList();
       */
        return Ok(filteredflights);//filtered);
        


    }
    

    /*

    [HttpPost("SuperUser/Inject")]
    public async Task<IActionResult> Inject([FromBody] Flight flight)
    {
        if (flight == null) return BadRequest("Keine Daten Empfangen");        
       // var result = _aviationService.ValidateFlightData(flight);
        await _aviationService.AddFlightAsync(flight);
        return Ok();
    }*/



    [HttpPost("Inject")]
    public async Task<IActionResult> Inject([FromBody] Flight data)
    {
        try
        {
            WriteToLog("Inject Endpoint aufgerufen");
            _aviationService.AddFlightAsync(data).Wait();
            if (data == null)
            {
                return BadRequest("Keine Daten Empfangen");
                //WriteToLog("Keine Daten empfangen im Inject Endpoint");
            }
            else
            {
                await _aviationService.AddFlightAsync(data);
            }
            // var result = _aviationService.ValidateFlightData(flight);

            return Ok();
        }
        catch (Exception ex)
        {
            WriteToLog($"Fehler im Inject Endpoint: {ex.Message}");
            return StatusCode(500, "Interner Serverfehler");
        }
    }

    [HttpPost("Update")]
    public async Task<IActionResult>Update([FromBody] Flight data)
    {
        if (ModelState.IsValid)
        {

            var flights = await _aviationService.GetAllFlightsAsync();

            var existingFlight = flights.FirstOrDefault(f => f.Id == data.Id); //(data.Id);// Flug Id

            if(existingFlight != null)
            {
                existingFlight.FlightDate = data.FlightDate;
                existingFlight.Aircraft_ID = data.Aircraft_ID;
                existingFlight.Hrs_total_Dual = data.Hrs_total_Dual;
                existingFlight.Hrs_Rotorcraft = data.Hrs_Rotorcraft;
                existingFlight.Hrs_PIC_Received = data.Hrs_PIC_Received;
                existingFlight.Cond_Day = data.Cond_Day;
                existingFlight.Cond_Night = data.Cond_Night;
                existingFlight.SOLO = data.SOLO;
                existingFlight.Cond_XCountry = data.Cond_XCountry;
                existingFlight.Lnd_Day = data.Lnd_Day;
                existingFlight.Lnd_Night = data.Lnd_Night;
                existingFlight.Flight_Route = data.Flight_Route;
                existingFlight.Sim_IFR = data.Sim_IFR;
                existingFlight.Remarks = data.Remarks;

                // jetzt feuern
                await _aviationService.UpdateFlightAsync(existingFlight);
            }

            return Ok();
        }
        else { return BadRequest("Ungültiger Moduszustand."); //hier wird das fehlerhafte Feld angezeigt
        }
    }
    /*
    [HttpPut("Update")]
    public async Task<IActionResult> 
    /*Update([FromBody] Flight data)
    {
        WriteToLog($"--- START: Update-Vorgang für ID {data.Id} ---");

        try
        {
            //var db = _aviationService_context;
            // 1. Den bestehenden Flug aus der DB suchen
            //var existingFlight = 
           await _aviationService.UpdateFlightAsync(data);

/*
            if (existingFlight == null)
            {
                WriteToLog($"FEHLER: Flug mit ID {data.Id} wurde nicht in der Datenbank gefunden.");
                return NotFound("Eintrag nicht gefunden.");
            }

            // 2. Die Werte vom JavaScript-Objekt auf das Datenbank-Objekt übertragen
            // Das ist das "Eingemachte": Wir kleben die neuen Werte über die alten
            //await _aviationService.UpdateFlightAsync(data);

            // 3. In der Aviation.db speichern
            //await _aviationService._context.SaveChangesAsync();
            WriteToLog($"ERFOLG: Änderungen für ID {data.Id} gespeichert.");

            return Ok(data.Id);
        }
        catch (Exception ex)
        {
            WriteToLog($"KRITISCHER FEHLER beim Update: {ex.Message}");
            return StatusCode(500, "Datenbankfehler beim Update.");
        }*/   

    [HttpDelete("/SuperUser/Purge/{id}")]
    public async Task<IActionResult> DELETE(int id)
    {
        try
        {
           WriteToLog("Purge Endpoint aufgerufen");
             await _aviationService.DeleteFlightAsync(id);            
            return Ok();
        }
        catch (Exception ex)
        {
            WriteToLog($"Fehler im Purge Endpoint: {ex.Message}");
            return StatusCode(500, "Interner Serverfehler");
        }
        
    }
    /*

    [HttpGet("List")]
    public async Task<IActionResult> GetFlights()
    {
        var flights = await _aviationService.GetAllFlightsAsync();
        return Ok(flights);
    }*/

    private void WriteToLog(string message)
    {
        try
        {
            Path.Combine(Directory.GetCurrentDirectory(), "DB_Logic.txt");


            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "DB_Logic.txt");
            using (var writer = new StreamWriter(logFilePath, append: true))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
            System.IO.File.AppendAllText(logFilePath, message + Environment.NewLine);

        }
        catch (Exception ex)
        {
            // Fehlerbehandlung, falls das Schreiben in die Log-Datei fehlschlägt
            Console.WriteLine($"Fehler beim Schreiben in die Log-Datei: {ex.Message}");
        }
    }
}
