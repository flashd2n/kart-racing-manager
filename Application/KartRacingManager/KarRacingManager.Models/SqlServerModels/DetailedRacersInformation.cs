using System.ComponentModel.DataAnnotations.Schema;

namespace KarRacingManager.Models
{
    [Table("DetailedRacersInformation")]
    public class DetailedRacersInformation
    {
        public int Id { get; set; }
        
        public double? Weight { get; set; }

        public double? Height { get; set; }

        public virtual Racer Racer { get; set; }
    }
}
