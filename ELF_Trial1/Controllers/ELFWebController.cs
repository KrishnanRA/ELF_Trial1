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
        public String SubmitRegistration(String FirstName, String Number, String Password, String Email, Int32 Class, Int32 Group, String User)
        {
            string registration = "";
            if (User == "Student")
            {
                registration = objweb.StudentRegistration(FirstName, "", Email, Password, Number, 1, 1, Class, 1, 1, 1, Group);

            }
            else if (User == "Parent")
            {
                registration = objweb.ParentRegistration(FirstName, "", Email, Password, Number, 1, 1, 1);
            }
            else if (User == "Staff")
            {
                registration = objweb.StaffRegistration(FirstName, "", Email, Password, Number, 1, Class, 1, 1, 1, 1, 1);
            }


            JObject Studentparsing = JObject.Parse(registration);

            String IsLogin = (String)Studentparsing["Table"][0]["OutputStatus"];

            return IsLogin;
        }

        public JsonResult GetClasses(int BoardId)
        {
            string output = objweb.GetClasses(BoardId);
            return Json(output, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Login(String Username, String Password, String UserType)
        {
            string Result = "";
            // Check UserType Login
            if (UserType == "Student")
            {
                string Studentoutput = objweb.CheckStudentLogin(Username, Password);

                //objLoginDetails = JArray.Parse(objweb.CheckStudentLogin(Username, Password));
                //Parse the String using Newsoft Library
                JObject StudentOutput = JObject.Parse(Studentoutput);

                // Get Output Status 
                string OutputStatus = (string)StudentOutput["Table"][0]["OutputStatus"];

                if (OutputStatus == "success")
                {
                    Int64 StudentID = (Int64)StudentOutput["Table"][0]["StudentId"];
             
                    //     return Json(StudentID);"
                    //  return RedirectToAction("Dashboard", "Student");

                    // return RedirectToAction("NewRegistration");
                   Session["UserType"] = "Student";
                   Session["UserId"] = StudentID;

                    Result = "success";
                }

            }
            // Check the UserType Login
            else if (UserType == "Parent")
            {
                // Check parent user exists
                string Parentoutput = objweb.CheckParentLogin(Username, Password);
                // Parse to String form Json
                JObject ParentParse = JObject.Parse(Parentoutput);
                // Get Output Status
                string ParentOutputStatus = (string)ParentParse["Table"][0]["OutputStatus"];

                if (ParentOutputStatus == "success")
                {
                    Int64 ParentId = (Int64)ParentParse["Table"][0]["ParentId"];
                  
                    // Validating Student id is Null
                  
                    Session["UserType"] = "Parent";
                    Session["UserId"] = ParentId;

                    Result = "success";
                }
            }
            else if (UserType == "Staff")
            {

                string Staffoutput = objweb.CheckStaffLogin(Username, Password);
            }

            // Check the OutPut states is Success and return to Json
            if (Result == "success")
            {
                return Json("success");
            }
            else
            {
                return Json("Failed");
            }

        }

        public ActionResult ForgotPassword()
        {

            return View();
        }


        public JsonResult SubmitForgotPassword(string Number, string Password, string Email, string User)
        {
            // Request to update password with respect to email and phonenumber
            // Get result into Json 
            string ForgotPassword = objweb.ForgotPassword(User, Number, Email, Password);
            // Parse the Json string into JObject
            JObject ParseForgotPassword = JObject.Parse(ForgotPassword);
            // get the Output response and validate
            string Result = (string)ParseForgotPassword["Table"][0]["OutputStatus"];
            return Json(Result);
        }

    }


}
