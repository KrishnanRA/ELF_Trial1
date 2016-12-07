using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELF_Trial1.Controllers
{
    public class ParentController : Controller
    {
        //
        // GET: /Parent/

        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Notifications()
        {
            return View();
        }
        public ActionResult Report()
        {
            return View();
        }
    }
}
