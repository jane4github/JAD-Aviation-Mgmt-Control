using System.Globalization;
using Microsoft.EntityFrameworkCore;


namespace janine_aviation.Models
{

public class AviationLog
{
    // Diese Felder existieren bereits in deiner DB
    public int Id { get; set; }
    public string FlightDate { get; set; } = string.Empty;
    public string AirC_ID { get; set; } = string.Empty;
    public string Hrs_PIC { get; set; } = string.Empty;// Enthält z.B. "1.4" oder "fwd 21.3 + 130.2"

    // Diese Eigenschaft ist NUR im Code, nicht in der DB
    public decimal ComputedPicValue
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Hrs_PIC)) return 0;

            // Logik für deine "Marys" (Summaries): Nimm den Wert nach dem Plus
            if (Hrs_PIC.Contains("+"))
            {
                var val = Hrs_PIC.Split('+').Last().Trim();
                decimal.TryParse(val, CultureInfo.InvariantCulture, out decimal res);
                return res;
            }

            // Normaler Eintrag: Punkt durch Punkt ersetzen (Sicherheit) und parsen
            decimal.TryParse(Hrs_PIC.Replace(',', '.'), CultureInfo.InvariantCulture, out decimal normal);
            return normal;
        }
    }
}
}