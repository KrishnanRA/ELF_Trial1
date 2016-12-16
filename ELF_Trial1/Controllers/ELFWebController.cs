using HtmlAgilityPack;
using System;
using System.Linq;
using System.Web.Mvc;
using ELF_Trial1.Models;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ELF_Trial1.Models.Student;

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

        //[HttpPost]
        //public ActionResult NewRegistration()
            
        //{

        //    //string registration = objweb.StudentRegistration(FirstName,"",Email,Password, Number, 1, 1, Class, 1,1,1, Group);
        //    return View();
        //}
        public String SubmitRegistration(String FirstName, String Number, String Password, String Email, Int32 Class, Int32 Group)
        {

            string registration = objweb.StudentRegistration(FirstName, "", Email, Password, Number, 1, 1, Class, 1, 1, 1, Group);

            JObject Studentparsing = JObject.Parse(registration);

            String IsLogin= (String)Studentparsing["Table"][0]["OutputStatus"];
            
            return IsLogin;
        }

        public JsonResult GetClasses(int BoardId)
        {
            string output = objweb.GetClasses(BoardId);
            return Json(output, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Login(String Username, String Password,String UserType)
        {
            
            string output="";
           // JArray objLoginDetails;
            if (UserType == "Student")
            {
                output = objweb.CheckStudentLogin(Username, Password);

                //objLoginDetails = JArray.Parse(objweb.CheckStudentLogin(Username, Password));
            }
            else if (UserType == "Parent")
            {
                output = objweb.CheckParentLogin(Username, Password);
            }
            else if (UserType == "Staff")
            {
                output = objweb.CheckStaffLogin(Username,Password);
            }
           //Parse the String using Newsoft Library
           JObject pp = JObject.Parse(output);
            //
            string OutputStatus = (string)pp["Table"][0]["OutputStatus"];

            
            if (OutputStatus == "success")
            {                
                Int64 StudentID = (Int64)pp["Table"][0]["StudentId"];
                GlobalStudentClass.BoardName = (string)pp["Table"][0]["BoardName"];
                GlobalStudentClass.CityName = (string)pp["Table"][0]["CityName"];
                GlobalStudentClass.ClassName = (string)pp["Table"][0]["ClassName"];
                GlobalStudentClass.DistrictName = (string)pp["Table"][0]["DistrictName"];
                GlobalStudentClass.EmailAddress = (string)pp["Table"][0]["EmailAddress"];
                GlobalStudentClass.Name = (string)pp["Table"][0]["FirstName"];
                GlobalStudentClass.PhoneNumber = (string)pp["Table"][0]["PhoneNumber"];
                GlobalStudentClass.StateName = (string)pp["Table"][0]["StateName"];
                GlobalStudentClass.StudentId = (Int32) pp["Table"][0]["StudentId"];
                GlobalStudentClass.InstitutionName= (string)pp["Table"][0]["InstitutionName"];
                //     return Json(StudentID);"
                //  return RedirectToAction("Dashboard", "Student");

                // return RedirectToAction("NewRegistration");

                return Json("success");

            }
            else
            {
                return Json("Failed");
            }

        }
       
    }

    
}
