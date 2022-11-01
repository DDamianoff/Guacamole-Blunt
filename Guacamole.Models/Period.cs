using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Guacamole.Models;

public class Period
{
    public int PeriodId
    { get; set; }
    
    public string PeriodName
    { get; set; }
    
    [MaxLength(256)]
    public TimePeriod CorrespondingPeriod
    { get; set; }

    public override string ToString() => $"{PeriodName} ({CorrespondingPeriod})";
}