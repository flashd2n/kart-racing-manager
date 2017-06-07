using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarRacingManager.Models.PostgreSqlModels
{
    public class Kart
    {
        public int Id { get; set; }

        public int HorsePower { get; set; }

        public double TopSpeedKph { get; set; }

        [ForeignKey("TransmissionType")]
        public int TransmissionTypeId { get; set; }

        public virtual TransmissionType TransmissionType { get; set; }
    }
}
