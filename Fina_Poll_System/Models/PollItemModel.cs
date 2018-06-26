using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fina_Poll_System.Models
{
    public class PollItemModel
    {
        public int ID { get; set; }
        public int PollID { get; set; }
        public string Name { get; set; }

        public List<PollItemTypeModel> PollItemTypes { get; set; }

        public bool Deleted { get; set; }
        public int? OrderIndex { get; set; }

        public string Description { get; set; }
    }
}