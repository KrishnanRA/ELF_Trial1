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
            //Call Get Student Under Parent WEb service
            //If(GetStudentUnderParentvalue!=null)
            //{
            //Get StudentID and Get Student Details from it
            //And Pass this data into dashboard
            //THis dashboard can be inherited from Student Dashboard Page
            //return View(Passing Student Details object);
            //}
            //Else
            //{
            //return View("First You Need to Send Request to your son");
                ////////Return Message Showing "First You Need to Send Request to your son"
            //}
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
