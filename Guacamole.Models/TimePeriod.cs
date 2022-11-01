using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Guacamole.Models;
/// <summary>
/// Represents a period of
/// centuries.
/// </summary>
public class TimePeriod
{
    public int TimePeriodId 
    { get; set; }
    
    /// <summary>
    /// Represents a Century.
    /// </summary>
    [MaxLength(22)]
    [MinLength(-5000)]
    public int InitialCentury;

    [NotMapped]
    public string InitialCenturyAsString =>
        $"{Math.Abs(InitialCentury)}" + (InitialCentury < 0 
            ? "b.C." 
            : "a.C.");

    /// <summary>
    /// This does also represents a Century.
    /// </summary>
    [MaxLength(22)]
    [MinLength(-5000)]
    public int FinalCentury;
    
    [NotMapped]
    public string FinalCenturyAsString =>
        $"{Math.Abs(FinalCentury)}" + (FinalCentury < 0 
            ? "b.C." 
            : "a.C.");
    
    public string? Location;

    public override string ToString() => $"{InitialCenturyAsString} ― {FinalCenturyAsString}";

}