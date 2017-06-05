using System.Collections.Generic;

namespace KarRacingManager.Models
{
    public class Track
    {
        private ICollection<Lap> laps;

        public Track()
        {
            this.laps = new HashSet<Lap>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public double Length { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Lap> Laps
        {
            get { return this.laps; }
            set { this.laps = value; }
        }
    }
}
