using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KarRacingManager.Models.SqliteModels
{
    public class LogType
    {
        private ICollection<Log> logs;

        public LogType()
        {
            this.logs = new HashSet<Log>();
        }

        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Log> Logs
        {
            get { return this.logs; }
            set { this.logs = value; }
        }
    }
}
