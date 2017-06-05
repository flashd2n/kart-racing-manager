using System;

namespace KarRacingManager.Models
{
    public class Racer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

    }
}
