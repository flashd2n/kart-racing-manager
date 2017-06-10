using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarRacingManager.Models.PostgreSqlModels
{
    public class TransmissionType
    {
        private ICollection<Kart> karts;

        public TransmissionType()
        {
            this.karts = new HashSet<Kart>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Kart> Karts
        {
            get { return this.karts; }
            set { this.karts = value; }
        }
    }
}
