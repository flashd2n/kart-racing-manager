using System.Collections.Generic;

namespace KartRacingManager.Interfaces.Imports
{
    public interface IImporter
    {
        string Path { get; set; }

        Dictionary<string, string> Execute();
    }
}
