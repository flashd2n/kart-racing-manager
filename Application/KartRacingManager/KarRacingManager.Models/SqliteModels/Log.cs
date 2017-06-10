using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarRacingManager.Models.SqliteModels
{
    public class Log
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime? TimeStamp { get; set; }

        [ForeignKey("LogType")]
        public int? LogTypeId { get; set; }

        public LogType LogType { get; set; }
    }
}
