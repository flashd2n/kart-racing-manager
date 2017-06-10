﻿using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace KartRacingManager.Imports
{
    public class XmlImporter : Importer
    {

        public XmlImporter(string path) : base(path)
        {
        }

        public override Dictionary<string, string> Execute()
        {
            var doc = XDocument.Load(this.Path);
            var rootNodes = doc.Root.DescendantNodes().OfType<XElement>();
            var keyValuePairs = from n in rootNodes
                                select new
                                {
                                    TagName = n.Name,
                                    TagValue = n.Value
                                };
            var allItems = rootNodes.ToDictionary(n => n.Name.ToString(), n => n.Value.ToString());

            return allItems;
        }
    }
}
