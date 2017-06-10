using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using KartRacingManager.Interfaces.Imports;

namespace KartRacingManager.Imports
{
    public class JsonImporter : Importer, IJsonImporter
    {
        public override Dictionary<string, string> Execute()
        {
            string json = ReadJsonFile();

            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            return result;
        }

        private string ReadJsonFile()
        {
            string json;

            using (StreamReader r = new StreamReader(this.Path))
            {
                json = r.ReadToEnd();
            }

            return json;
        }
    }
}
