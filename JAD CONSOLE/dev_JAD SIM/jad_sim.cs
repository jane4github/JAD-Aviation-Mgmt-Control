sharp
// In deiner JAD Console Infrastruktur
public async Task MonitorFlightAsync(FlightData currentData)
{
    // Die KI-Berechnung läuft auf einem eigenen Task, damit die Console nicht laggt
    var isAnomaly = await Task.Run(() => _aiEngine.IsBehaviorAbnormal(currentData));

    if (isAnomaly)
    {
        // UI-Event auslösen: Pilot muss Abweichung begründen
        await RequestPilotStatement("Starke Abweichung vom Flugprofil erkannt. Bitte Grund angeben.");
    }
}