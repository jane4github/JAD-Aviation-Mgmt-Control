public class DamageMitigationEngine 
{
    // Berechnet den sichersten Spot im Gleitradius
    public Vector3 FindLeastCollateralDamageSpot(Vector3 currentPos, float glideRange) 
    {
        var potentialSpots = MapService.GetSurroundingAreas(currentPos, glideRange);
        
        return potentialSpots
            .OrderBy(spot => spot.PopulationDensity) // Priorität: Wenig Menschen
            .ThenBy(spot => spot.SurfaceType == Surface.BrownField ? 0 : 1) // Dann: Weicher Untergrund
            .ThenBy(spot => Vector3.Distance(currentPos, spot.Location)) // Dann: Kürzester Weg
            .First().Location;
    }

    public void ExecuteForcedLanding(Vector3 targetSpot) 
    {
        // 1. Mayday mit exakten Koordinaten des Feldes absetzen
        _radio.Broadcast("MAYDAY - Forced Landing at BrownField Coordinates: " + targetSpot);
        
        // 2. Fly-By-Wire: Steuere den "Dead Stick" (Gleitflug) zum Spot
        _autopilot.GlideTo(targetSpot);
    }
}