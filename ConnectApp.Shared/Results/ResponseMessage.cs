using ConnectApp.Shared.Types.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectApp.Shared.Results
{
    public class ResponseMessage
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
        public IList<KeyValue>? Values { get; set; }
        public Guid Id { get; set; }
        public bool Status { get; set; } = false;
    }
}
