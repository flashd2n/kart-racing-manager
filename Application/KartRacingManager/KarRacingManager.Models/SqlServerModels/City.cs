using System.Collections.Generic;

namespace KarRacingManager.Models
{
    public class City
    {
        private ICollection<Address> addresses;

        public City()
        {
            this.addresses = new HashSet<Address>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Address> Addresses
        {
            get { return this.addresses; }
            set { this.addresses = value; }
        }
    }
}
