using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Globalization;
using janine_aviation.Models;

namespace janine_aviation.Pages
{
    public class SuperuserModel : PageModel
    {
        public readonly AviationContext _context;
       // private readonly ILogger<SuperuserModel> _logger;

        // Die Liste für deine Tabelle links
        [BindProperty(SupportsGet = true)]

        public string searchId { get; set; } = string.Empty;
        public string ConsoleOutput { get; set; } = string.Empty;//"[ SYSTEM STARTING:.... ]";
        public List<AviationLog> Results { get; set; } = new List<AviationLog>();
        // Der Konstruktor verbindet sich mit deiner SQLite-Datenbank
        public SuperuserModel(AviationContext context)
        {
            _context = context; //Absicherung initialisierung im Konstruktor verhindert NullReferenceExceptions
            Results= new List<AviationLog>();
           ConsoleOutput = "[SYSTEM_BOOTING : Waiting for Input]";
        }


        /*
        private bool IsAuthorizedAdmin()
        {
            // Deine bekannte lokale Handy-IP
            string myMobileIP = "192.168.1.56";
            string visitorIP = Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "unbekannt:";

            // Erlaubt Zugriff nur vom PC selbst (::1) oder deinem Handy im WLAN
            if (visitorIP == myMobileIP || visitorIP == "::1" || visitorIP == "127.0.0.1")
            {
                return true;
            }

            // Wenn nicht autorisiert: Logging und Drohbotschaft
            _logger.LogWarning("LEG DICH NICHT MIT DEN BESTEN AN ODER STIRB WIE ALLE DANN! IP: {IP}", visitorIP);
            return false;
        }*/

        // Der Text für dein schwarzes Terminal rechts
        //[BindProperty(SupportsGet = true)]
        // public string ConsoleOutput { get; set; } = "[ SYSTEM OFFLINE:.... ]";

        // Diese Methode wird durch deinen [ TEST ] Button (triggerSearch) aufgerufen
        public async Task OnGetAsync()
        {
            // Wenn keine ID eingegeben wurde, zeigen wir den Standard-Status
            if (string.IsNullOrEmpty(searchId))
            {
                ConsoleOutput = "[ SYTEM: 0.0]";
                Results = new List<AviationLog>();
                return; 
            }

            // 1. Die SQL-Suche ausführen (Filtert nach der Flugzeug-Kennung)
            Results = await _context.Set<AviationLog>()
                .Where(l => l.AirC_ID.Contains(searchId))
                .OrderByDescending(l => l.FlightDate) // Neueste Flüge zuerst anzeigen
                .ToListAsync();

            // 2. Den "Hacker-Header" für das Terminal bauen
            var sb = new StringBuilder();
            sb.AppendLine($"> ACCESS_GRANTED: ANALYSIS FOR '{searchId}'");
            sb.AppendLine($"> RECORDS_FOUND: {Results.Count}");
            sb.AppendLine("> -----------------------------------------");

            // 3. Die Flugstunden (Marys) zusammenrechnen
            // Wir nutzen 'ComputedPicValue', wie in deinem Screenshot
            decimal totalPic = Results.Sum(r => (decimal?)r.ComputedPicValue ?? 0m);

            sb.AppendLine($"> TOTAL PIC HOURS: {totalPic.ToString("F2", CultureInfo.InvariantCulture)}");
            sb.AppendLine("> ");
            sb.AppendLine("> [ SYSTEM_STATUS: OK ]");
            sb.AppendLine("> READY_FOR_NEXT_COMMAND...");

            // 4. Den fertigen Text an die Anzeige übergeben
            ConsoleOutput = sb.ToString();
        }
    }
}

