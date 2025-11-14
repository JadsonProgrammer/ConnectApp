using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectApp.Shared.Helpers
{
    public class Validation
    {
        public static bool IsNullOrEmptyGuid(Guid? guid)
        {
            return guid == null || guid == Guid.Empty;
        }
    }
}
