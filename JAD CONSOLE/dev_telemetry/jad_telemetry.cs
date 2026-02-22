public interface IFlightTelemetry
{
    float Pitch { get; }
    float Roll { get; }
    float VerticalSpeed { get; }
    bool IsRadioActive { get; } // Wurden Funksprüche abgesetzt?
    Vector3 Position { get; }
}