namespace janine_aviation.Models
{
 public class Flight
{
    public Flight() { }

    

    public int Id { get; set; }
        public string? total_Duration { get; set; } = "0";
        public string? FlightDate { get; set; } = "0";
        public string? Year { get; set; } = "0";
        public string? Aircraft { get; set; } = "0";
        public string? Aircraft_ID { get; set; } = "0";        
    public string? Airplane_Single { get; set; } = "0";
    public string? Airplane_Multi { get; set; } = "0";
    public string? Hrs_Rotorcraft { get; set; } = "0";
    public string? SOLO { get; set; } = "0";
    public string? Hrs_Dual_Received { get; set; } = "0";
    public string? Hrs_PIC_Received { get; set; } = "0";
    public string? Hrs_Secnd_Command_Received { get; set; } = "0";
    public string? Hrs_CFI_Received { get; set; } = "0";
    public string? Ground { get; set; } = "0";
    public string? Flight_Route { get; set; } = "0";
    public string? Pilots { get; set; } = "0";
    public string? Rules { get; set; } = "0";
    public string? Landings { get; set; } = "0";
    public string? Starts { get; set; } = "0";
    public string? Cond_Day { get; set; } = "0";
    public string? Cond_Night { get; set; } = "0";
    public string? Cond_XCountry { get; set; } = "0";
    public string? Cond_Instrument { get; set; } = "0";
    public string? Cond_Sim_Instr { get; set; } = "0";
    public string? Lnd_Day { get; set; } = "0";
    public string? Lnd_Night { get; set; } = "0";
    public string? Hrs_total_Dual { get; set; } = "0";
    public string? Remarks { get; set; } = "0";
    public string? Pilot_CFI_Names { get; set; } = "0";
    public string? Sim_IFR { get; set; } = "0";
    }
}
