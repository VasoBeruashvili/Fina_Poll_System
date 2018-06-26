using Fina_Poll_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Fina_Poll_System
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public PollModel GetPoll(int pollID)
        {
            DataMapper dm = new DataMapper();
            Fina_Poll_SystemEntities context = new Fina_Poll_SystemEntities();
            var poll = context.Polls.Single(p => p.ID == pollID);

            var pollModel = dm.MapData(poll);

            return pollModel;
        }
    }
}
