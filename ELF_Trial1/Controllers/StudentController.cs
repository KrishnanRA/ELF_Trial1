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
        web_ws.Ielf_web_wsClient studentweb = new web_ws.Ielf_web_wsClient();
        public ActionResult Dashboard(int StudentID)
        {
          //  string StudentDetails = studentweb.CheckStudentLogin(Username, Password);
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
        public JsonResult StudentLogin(String Username,String Password)
        {
            string output = studentweb.CheckStudentLogin(Username, Password);
            return Json(output, JsonRequestBehavior.AllowGet);
        }
    }
}
