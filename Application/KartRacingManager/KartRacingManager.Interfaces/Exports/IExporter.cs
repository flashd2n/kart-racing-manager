using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartRacingManager.Interfaces.Exports
{
    public interface IExporter
    {
        void ExportRacer(Dictionary<string, string> racerInfo);

        void ExportRace(Dictionary<string, string> raceInfo);
    }
}
