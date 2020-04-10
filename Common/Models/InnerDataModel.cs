using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devopsgenie.service.Common.Models
{
    public class InnerDataModel  //see, object/DTO solution https://stackoverflow.com/questions/52069694/post-received-by-frombody-causes-serializable-error
    {
        public string Body { get; set; }
    }
}
