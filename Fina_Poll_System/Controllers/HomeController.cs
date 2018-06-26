using Fina_Poll_System.Filters;
using Fina_Poll_System.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Fina_Poll_System.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "";

            return View();
        }


        BusinessLogic bl = new BusinessLogic();

        public JsonResult GetPolls(BaseFilterModel filter)
        {
            return CustomPagingJson(bl.GetPolls, filter);
        }


        public JsonResult SavePollModel(PollModel model)
        {
            var result = bl.SavePollModel(model);

            return Json(new { success = true, root = result }, JsonRequestBehavior.AllowGet);
        }


        public class CustomJsonResult : JsonResult
        {
            public override void ExecuteResult(ControllerContext context)
            {
                var response = context.HttpContext.Response;

                response.ContentEncoding = System.Text.Encoding.UTF8;

                response.ContentType = "application/json";

                JsonSerializer serializer = new JsonSerializer();

                serializer.Converters.Add(new JavaScriptDateTimeConverter());

                serializer.NullValueHandling = NullValueHandling.Include;

                var sb = new StringBuilder();

                var textWriter = new StringWriter(sb);


                serializer.Serialize(new JsonTextWriter(textWriter), Data);

                response.Write(sb.ToString());
            }
        }

        public static CustomJsonResult CustomPagingJson<TFilter, TResult>(Func<TFilter, TResult> callSite, TFilter filter) where TFilter : BaseFilterModel
        {
            var result = new CustomJsonResult();

            try
            {
                TResult data = callSite(filter);

                result.Data = new
                {
                    root = data,
                    success = true,
                    start = filter.start,
                    limit = filter.limit,
                    count = filter.count
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
