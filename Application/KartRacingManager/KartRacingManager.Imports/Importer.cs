using KartRacingManager.Interfaces.Imports;
using System;
using System.Collections.Generic;

namespace KartRacingManager.Imports
{
    public abstract class Importer : IImporter
    {
        private string path;

        public Importer(string path)
        {
            this.Path = path;
        }

        public string Path
        {
            get
            {
                return this.path;
            }

            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Path cannot be null or empty string!");
                }

                this.path = value;
            }
        }

        public abstract Dictionary<string, string> Execute();
    }
}
