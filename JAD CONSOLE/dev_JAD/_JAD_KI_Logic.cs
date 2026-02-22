using Microsoft.ML;
using Microsoft.ML.TimeSeries;

// 1. Datenmodell für den Flugzustand
public class FlightData {
    public float Altitude { get; set; }
    public float Heading { get; set; }
    // Zeitstempel oder Distanz zum Ziel
}

public class FlightAnomalyPrediction {
    // [Alert, Score, P-Value]
    [VectorType(3)] public double[] Prediction { get; set; }
}

// 2. Infrastruktur in der Console
public void CheckFlightBehavior(List<FlightData> telemetry) {
    var context = new MLContext();
    var dataView = context.Data.LoadFromEnumerable(telemetry);

    // Der "Sicherheits-Aspekt": Erkennt plötzliche Abweichungen in der Flughöhe
    var pipeline = context.Transforms.DetectIidSpike(
        outputColumnName: nameof(FlightAnomalyPrediction.Prediction),
        inputColumnName: nameof(FlightData.Altitude),
        confidence: 99.0,                // Strenge der Überwachung
        pvalueHistoryLength: telemetry.Count / 4); // Historisches Fenster

    var transformedData = pipeline.Fit(dataView).Transform(dataView);
    var predictions = context.Data.CreateEnumerable<FlightAnomalyPrediction>(transformedData, reuseRowObject: false);

    foreach (var p in predictions) {
        if (p.Prediction[0] == 1) { 
            // ALARM: Hier weicht der Pilot massiv von seiner Struktur ab!
            TriggerSecurityProtocol(); 
        }
    }
}