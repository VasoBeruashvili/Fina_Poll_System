using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fina_Poll_System.Models
{
    public class PollModel
    {
        public int ID { get; set; }
        public DateTime TDate { get; set; }
        public DateTime? WaitDays { get; set; }
        public string Version { get; set; }

        public List<PollItemModel> PollItems { get; set; }

        public bool Deleted { get; set; }
    }
}