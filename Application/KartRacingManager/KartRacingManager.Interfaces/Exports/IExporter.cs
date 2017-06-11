using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartRacingManager.Interfaces.Exports
{
    public interface IExporter
    {
        void ExportRacerInfo(Dictionary<string, string> racerInfo);

        void ExportRaceInfo(Dictionary<string, string> raceInfo);
    }
}
