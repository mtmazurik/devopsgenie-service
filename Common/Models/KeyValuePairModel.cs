using System;
using System.Collections.Generic;
using System.Text;

namespace devopsgenie.service.Common.Models
{
    public class KeyValuePairModel
    {
        public KeyValuePairModel(string nameIn, string valueIn)
        {
            name = nameIn;
            value = valueIn;
        }

        public string name;
        public string value;
    }
}
