using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMandiola2.Controllers
{
    public class CoreController : Controller
    {
        // GET: Core
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session["usuarioLogueado"] = null;
            return Redirect("~/Login/Index");
        }

    }
}
