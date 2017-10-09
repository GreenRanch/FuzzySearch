using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzySearch.Core.Models.Config
{
    public class DocumentDbConfig
    {
        public string Endpoint { get; set; }
        public string Key { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
