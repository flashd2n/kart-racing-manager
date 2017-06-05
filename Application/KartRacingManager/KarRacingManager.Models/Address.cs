using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarRacingManager.Models
{
    public class Address
    {
        private ICollection<Racer> racers;

        public Address()
        {
            this.racers = new HashSet<Racer>();
        }

        public int Id { get; set; }

        public string Location { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<Racer> Racers
        {
            get { return this.racers; }
            set { this.racers = value; }
        }
    }
}
