using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Http.WebHost;

namespace TaskTestWebAPI.Controllers.Utils
{
    public class NoBufferPolicyModifier : WebHostBufferPolicySelector
    {
        public override bool UseBufferedInputStream(object hostContext)
        {
            return false;
        }
    }
}