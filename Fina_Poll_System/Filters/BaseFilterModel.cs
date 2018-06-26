using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fina_Poll_System.Filters
{
    public class BaseFilterModel
    {
        public int? start { get; set; }
        public int? limit { get; set; }
        public int count { get; set; }

        public string StatusName { get; set; }
    }
}