using System.Collections.Generic;

namespace KarRacingManager.Models
{
    public class Country
    {
        private ICollection<City> city;

        public Country()
        {
            this.city = new HashSet<City>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<City> Cities
        {
            get { return this.city; }
            set { this.city = value; }
        }
    }
}
