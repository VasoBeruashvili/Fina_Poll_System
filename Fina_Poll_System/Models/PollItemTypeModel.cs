using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fina_Poll_System.Models
{
    public class PollItemTypeModel : IEntity
    {
        public int ID { get; set; }
        public int PollItemID { get; set; }
        public string Name { get; set; }

        public bool Deleted { get; set; }
        public int? OrderIndex { get; set; }
    }
}