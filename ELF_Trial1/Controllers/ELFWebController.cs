using HtmlAgilityPack;
using System;
using System.Linq;
using System.Web.Mvc;
using ELF_Trial1.Models;
using System.Collections;
using System.Collections.Generic;

namespace ELF_Trial1.Controllers
{

    public class ELFWebController : Controller
    {
        
        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
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
        public JsonResult Login(String Username, String Password,String UserType)
        {
            
            string output="";
            if (UserType=="Student")
            {
                output = objweb.CheckStudentLogin(Username, Password);
            }
           
            Newtonsoft.Json.Linq.JObject pp = Newtonsoft.Json.Linq.JObject.Parse(output);
            string rssTitle = (string)pp["Table1"][0]["OutputStatus"];
           
            return Json(output, JsonRequestBehavior.AllowGet);
        }
    }
}
