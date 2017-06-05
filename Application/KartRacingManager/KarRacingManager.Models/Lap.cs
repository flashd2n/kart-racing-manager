using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarRacingManager.Models
{
    public class Lap
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime FinishTime { get; set; }

        public int RacerId { get; set; }

        public virtual Racer Racer { get; set; }

        public int TrackId { get; set; }

        public virtual Track Track { get; set; }

        [ForeignKey("Race")]
        public int? RaceId { get; set; }

        public virtual Race Race { get; set; }
    }
}
