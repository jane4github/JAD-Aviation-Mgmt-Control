using janine_aviation.Models;
using janine_aviation.Services;
using static janine_aviation.Services.AviationServices;

namespace janine_aviation.Services
{
        public interface IAviationServices
        {

            //Task<List<Flight>>SearchFlights();
            //Task<List<Flight>>GetFlightsByYearAsync();
            Task<List<Flight>> SearchFlightsPagedAsync(string query, int page, int pageSize);

   
            //Task<List<Flight>>SearchFlights(string query);
            Task<List<Flight>>GetAllFlightsAsync(); 
            Task AddFlightAsync(Flight flight);
            Task DeleteFlightAsync(int id);        
            Task UpdateFlightAsync(Flight data);
            Task<List<Flight>>GetFlightsPageAsync(int page);

    }


}
