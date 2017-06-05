using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarRacingManager.Models
{
    public class Racer
    {
        private ICollection<Lap> laps;
        private ICollection<Race> races;

        public Racer()
        {
            this.laps = new HashSet<Lap>();
            this.races = new HashSet<Race>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual DetailedRacersInformation DetailedInformation { get; set; }

        public virtual ICollection<Lap> Laps
        {
            get { return this.laps; }
            set { this.laps = value; }
        }

        public virtual ICollection<Race> Races
        {
            get { return this.races; }
            set { this.races = value; }
        }
    }
}
