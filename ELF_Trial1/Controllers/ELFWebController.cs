using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELF_Trial1.Controllers
{
    public class ELFWebController : Controller
    {
        web_ws.Ielf_web_wsClient objweb = new web_ws.Ielf_web_wsClient();
        //
        // GET: /ELFWeb/

        /// <summary>
        /// Main Login Page
        /// Navigates based on their Role
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            return View();
        }
        /// <summary>
        /// Register New User
        /// </summary>
        /// <returns></returns>
        public ActionResult NewRegistration()
        {

            return View();
        }
        public JsonResult GetClasses(int BoardId)
        {
            string output = objweb.GetClasses(BoardId);
            return Json(output, JsonRequestBehavior.AllowGet);
        }
    }
}
