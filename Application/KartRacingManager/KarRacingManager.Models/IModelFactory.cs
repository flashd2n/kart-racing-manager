using KarRacingManager.Models.PostgreSqlModels;
using KarRacingManager.Models.SqliteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarRacingManager.Models
{
    public interface IModelFactory
    {
        Kart CreateKart();
        TransmissionType CreateTransmissionType();
        Log CreateLog();
        LogType CreateLogType();
        Address CreateAddress();
        City CreateCity();
        Country CreateCountry();
        DetailedRacersInformation CreateDetailedRacersInformation();
        Lap CreateLap();
        Race CreateRace();
        Racer CreateRacer();
        Track CreateTrack();
    }
}
