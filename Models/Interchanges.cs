using System.ComponentModel.DataAnnotations;

namespace TollCalculation.Models
{
    public class Interchanges
    {
        [Key]
        public int InterchangeId { get; set; }
        public string? InterchnageName { get; set; }
        public decimal Distance { get; set; }
    }
}
