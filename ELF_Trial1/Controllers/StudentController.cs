using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELF_Trial1.Controllers
{
    public class StudentController : Controller
    {
        //
        // GET: /Student/

        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult TestMain()
        {
            return View();  
        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult TestSummary()
        {
            return View();
        }
        public ActionResult Report()
        {
            return View();
        }
        public ActionResult Notification()
        {
            return View();
        }
    }
}
