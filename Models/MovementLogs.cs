using System.ComponentModel.DataAnnotations;


namespace TollCalculation.Models
{
    public class MovementLogs
    {
        public int MovementId { get; set; }
        public int EnteryInterchangeId { get; set; }
        public int ExitInterchangeId { get; set; }
        public DateTime EnteryTime { get; set; }
        public DateTime ExitTime { get; set; }
        public string? CarRegNumber { get; set; }
        

    }
    public class MovementLogsRequest
    {
        public string? InterchangeName { get; set; }
        public string? CarRegNumber { get; set; }
        public DateTime EntryExitDate { get; set; }
    }
    public class TollCalculationRespomse
    {
        public decimal BaseRate { get; set; }
        public decimal breakdown { get; set; }
        public decimal subtotal { get; set; }
        public decimal totaldiscount { get; set; }
        public decimal TobeCharged { get; set; }

    }
}
