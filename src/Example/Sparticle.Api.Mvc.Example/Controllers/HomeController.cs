using Sparticle.DTO.Example;
using Sparticle.Request.Context;
using Sparticle.Service.Example;
using Sparticle.Service.Interface.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sparticle.Api.Mvc.Example.Controllers
{
    public class HomeController : Controller
    {
        public JsonResult JsonGet(object data)
        {
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public new JsonResult Json(object data)
        {
            if (HttpContext.Request.HttpMethod.ToUpper() == "GET")
                return JsonGet(data);
            return base.Json(data);
        }

        // GET: Home
        public ActionResult Index(string name)
        {
            return Content("Welcome! " + name);
        }

        private IExampleService _exampleService = new ExampleService("Example", "Mvc");
        public ActionResult Echo(string msg, RequestContext requestContext)
        {
           var result =  _exampleService.Echo(msg, requestContext);

            return Json(result);
        }

        public ActionResult Add(AddRequest request, RequestContext requestContext)
        {
            var result = _exampleService.Add(request, requestContext);

            return Json(result);
        }

        public ActionResult Div(DivRequest request, RequestContext requestContext)
        {
            var result = _exampleService.Div(request, requestContext);

            return Json(result);
        }
    }
}