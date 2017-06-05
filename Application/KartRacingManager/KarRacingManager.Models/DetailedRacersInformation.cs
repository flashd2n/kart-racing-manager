using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarRacingManager.Models
{
    public class DetailedRacersInformation
    {
        public int Id { get; set; }
        
        public double? Weight { get; set; }

        public double? Height { get; set; }

        public virtual Racer Racer { get; set; }
    }
}
