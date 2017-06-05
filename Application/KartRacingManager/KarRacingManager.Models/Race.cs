using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarRacingManager.Models
{
    public class Race
    {
        private ICollection<Racer> racers;
        private ICollection<Lap> laps;

        public Race()
        {
            this.racers = new HashSet<Racer>();
            this.laps = new HashSet<Lap>();
        }

        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int LapCount { get; set; }

        public int TrackId { get; set; }

        public virtual Track Track { get; set; }

        public RaceStatus RaceStatus { get; set; }

        public virtual ICollection<Racer> Racers
        {
            get { return this.racers; }
            set { this.racers = value; }
        }

        public virtual ICollection<Lap> Laps
        {
            get { return this.laps; }
            set { this.laps = value; }
        }
    }
}
