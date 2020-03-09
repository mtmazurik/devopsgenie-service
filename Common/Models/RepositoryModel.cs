using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevopsGenie.Service.Common.Models
{
    public class RepositoryModel
    {
        public object _id { get; set; }
        public string key { get; set; }
        public IEnumerable<string> tags { get; set; }
        public DateTime createdDate { get; set; }
        public string createdBy { get; set; }
        public object modifiedDate { get; set; }
        public string modifiedBy { get; set; }
        public string app { get; set; }
        public string repository { get; set; }
        public string collection { get; set; }
        public bool validate { get; set; }
        public string schemaUri { get; set; }
        public string data { get; set; }
    }
}
