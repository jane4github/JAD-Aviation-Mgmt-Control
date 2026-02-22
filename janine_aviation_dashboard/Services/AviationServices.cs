using janine_aviation.Models;
//using janine_aviation.Service;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Threading.Tasks;


namespace janine_aviation.Services
{

    public class AviationServices : IAviationServices
    {
        public readonly AviationContext _context;
        public AviationServices(AviationContext context)
        {
            _context = context;
            return;
        }

        public async Task<List<Flight>>searchFlights(string query)
        {
            var pattern = $"%{query}%"; //standard SQL wildcard pattern for "contains"
            return await _context.Flights
                .Where(f =>
                                EF.Functions.Like(f.Flight_Route, pattern)
                                || EF.Functions.Like(f.FlightDate, pattern)
                                || EF.Functions.Like(f.Remarks, pattern)
                                || EF.Functions.Like(f.Aircraft_ID, pattern))//Aircraft.Contains(term) || f.FlightDate.ToString().Contains(term))
                .ToListAsync();
        }

        public async Task<List<Flight>> GetFlightsPageAsync(int page)
        {
            int pageSize = 5; // Anzahl der Einträge pro Seite
            return await _context.Flights
                .OrderByDescending(f => f.FlightDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        public async Task UpdateFlightAsync(Flight data)
        {
            var existingFlight = await _context.Flights.FindAsync(data.Id);
            if (existingFlight != null)
            {
                _context.Entry(existingFlight).CurrentValues.SetValues(data);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<List<Flight>> GetAllFlightsAsync()
        {
            return await _context.Flights
                .OrderByDescending(f => f.FlightDate.ToString())
                .ToListAsync();
        }


        public async Task AddFlightAsync(Flight flight)
        {
            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFlightAsync(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<double> GetTotalHoursAsync()
        {
            // Berechnet die Summe aller Stunden für das Dashboard
            return await _context.Flights.SumAsync(f => Convert.ToDouble(f.Hrs_total_Dual));
        }

        public async Task<List<Flight>> SearchFlightsPagedAsync(string query, int page, int pageSize = 5)
        {
            var pattern = $"%{query}%"; //standard SQL wildcard pattern for "contains"
            return await _context.Flights
                .Where(f =>
                                EF.Functions.Like(f.Flight_Route, pattern)
                                || EF.Functions.Like(f.FlightDate, pattern)
                                || EF.Functions.Like(f.Remarks, pattern)
                                || EF.Functions.Like(f.Aircraft_ID, pattern))//Aircraft.Contains(term) || f.FlightDate.ToString().Contains(term))
                .OrderByDescending(f => f.FlightDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }       
       

        public interface IAviationService
        {
            Task<List<Flight>> SearchFlights();
            Task<List<Flight>> GetFlightsByYearAsync();
            Task<List<Flight>> GetAllFlightsAsync();
            Task AddFlightAsync(Flight flight);
            Task DeleteFlightAsync(int id);
            Task<double> GetTotalHoursAsync();
            Task UpdateFlightAsync(Flight data);
            Task<List<Flight>> GetFlightsPageAsync(int page);
            Task SearchFlightsPagedAsync(string query, int page, int pageSize);
            Task<IEnumerable<object>> SearchFlightsPagedAsync(string query, int page);
        }
    }
}