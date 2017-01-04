using ELF_Trial1.Models;
using ELF_Trial1.Models.Student;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELF_Trial1.Controllers
{
    public class CustomerSupportController : Controller
    {
        //
        // GET: /CustomerSupport/
        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        web_ws.Ielf_web_wsClient SupportWeb = new web_ws.Ielf_web_wsClient();
        public static GlobalStudentSubjectDetails GlobalStudentSubjects = new GlobalStudentSubjectDetails();
        public static Int32 GlobalStudentID = 0;
        public ActionResult Index(int StudentId)
        {
            GlobalStudentID = StudentId;
            StudentGeneralDetails objStudentLoginDetails = new StudentGeneralDetails();
            StudentDashboard _StudentDashboardDetails = new StudentDashboard();
            if (GlobalStudentID != 0)
            {
                String _GetStudentGeneralDetails = SupportWeb.GetStudentDetails(GlobalStudentID);
                JObject StudentGeneralDetailsparsing = JObject.Parse(_GetStudentGeneralDetails);

                objStudentLoginDetails.BoardName = (string)StudentGeneralDetailsparsing["Table"][0]["BoardName"];
                objStudentLoginDetails.CityName = (string)StudentGeneralDetailsparsing["Table"][0]["CityName"];
                objStudentLoginDetails.ClassName = (string)StudentGeneralDetailsparsing["Table"][0]["ClassName"];
                objStudentLoginDetails.DistrictName = (string)StudentGeneralDetailsparsing["Table"][0]["DistrictName"];
                objStudentLoginDetails.EmailAddress = (string)StudentGeneralDetailsparsing["Table"][0]["EmailAddress"];
                objStudentLoginDetails.Name = (string)StudentGeneralDetailsparsing["Table"][0]["FirstName"];
                objStudentLoginDetails.PhoneNumber = (string)StudentGeneralDetailsparsing["Table"][0]["PhoneNumber"];
                objStudentLoginDetails.GroupId = (int)StudentGeneralDetailsparsing["Table"][0]["GroupId"];

                objStudentLoginDetails.StudentId = GlobalStudentID;

                objStudentLoginDetails.InstitutionName = GlobalStudentClass.InstitutionName;

                
                StudentRank _StudentRankDetails = new StudentRank();
                OverallAvailableTest _StudentOverallAvailableTest = new OverallAvailableTest();
                OverallLastFiveTest _StudentOverallLastFiveTest = new OverallLastFiveTest();

                String _GetStudentDashboardDetails1 = SupportWeb.GetStudentDashboard(GlobalStudentID);
                String _GetStudentDashboardDetails = SupportWeb.StudentDashboardDetails(GlobalStudentID);
                JObject Studentparsing = JObject.Parse(_GetStudentDashboardDetails);

                JObject Studentparsing1 = JObject.Parse(_GetStudentDashboardDetails1);
                try
                {
                    _StudentRankDetails.CityRank = (Int32)Studentparsing1["Table"][0]["CityRank"];
                    _StudentRankDetails.DistrictRank = (Int32)Studentparsing1["Table"][0]["DistrictRank"];
                    _StudentRankDetails.InstitutionRank = (Int32)Studentparsing1["Table"][0]["InstitutionRank"];
                    _StudentRankDetails.StateRank = (Int32)Studentparsing1["Table"][0]["StateRank"];
                }
                catch
                {
                    return RedirectToAction("CheckStudentDetails", "CustomerSupport");
                }
                int _SubjectPercentageCount = (Int32)Studentparsing["Table1"].Count();

                List<SubjectPercentage> _SubjectPercentageList = new List<SubjectPercentage>();
                for (int i = 0; i < _SubjectPercentageCount; i++)
                {
                    SubjectPercentage _Subjectpercentage = new SubjectPercentage();
                    _Subjectpercentage.SubjectId = (Int32)Studentparsing["Table1"][i]["SubjectId"];
                    _Subjectpercentage.Percentage = (Int32)Studentparsing["Table1"][i]["Percentage"];
                    _Subjectpercentage.SubjectName = (String)Studentparsing["Table1"][i]["SubjectName"];
                    _SubjectPercentageList.Add(_Subjectpercentage);
                }
                List<OverallLastFiveTest> _LastFiveTestList = new List<OverallLastFiveTest>();
                int _LastFiveTestCountCount = (Int32)Studentparsing["Table2"].Count();

                for (int i = 0; i < _LastFiveTestCountCount; i++)
                {
                    OverallLastFiveTest _LastFiveTest = new OverallLastFiveTest();
                    _LastFiveTest.SubjectID = (Int32)Studentparsing["Table2"][i]["SubjectId"];
                    _LastFiveTest.Percentage = (Int32)Studentparsing["Table2"][i]["Percentage"];
                    _LastFiveTest.SubjectName = (String)Studentparsing["Table2"][i]["SubjectName"];
                    _LastFiveTest.TestId = (Int32)Studentparsing["Table2"][i]["TestId"];
                    _LastFiveTest.TestType = (String)Studentparsing["Table2"][i]["TestType"];
                    _LastFiveTestList.Add(_LastFiveTest);
                }


                List<OverallAvailableTest> _OverallAvailableTestList = new List<OverallAvailableTest>();

                for (int i = 0; i < 5; i++)
                {
                    OverallAvailableTest _OverallAvailableTest = new OverallAvailableTest();
                    _OverallAvailableTest.SubjectID = (Int32)Studentparsing["Table3"][i]["SubjectId"];
                    _OverallAvailableTest.SubjectName = (String)Studentparsing["Table3"][i]["SubjectName"];
                    _OverallAvailableTest.TestId = (Int32)Studentparsing["Table3"][i]["TestId"];
                    _OverallAvailableTest.TestType = (String)Studentparsing["Table3"][i]["TestType"];
                    _OverallAvailableTestList.Add(_OverallAvailableTest);
                }

                //ADding Subject in Global
                int _SubjectCount = (Int32)Studentparsing["Table4"].Count();


                List<SubjectDetails> _SubjectDetailsList = new List<SubjectDetails>();
                for (int i = 0; i < _SubjectCount; i++)
                {
                    SubjectDetails _SubjectDetails = new SubjectDetails();
                    _SubjectDetails.SubjectName = (String)Studentparsing["Table4"][i]["SubjectName"];
                    _SubjectDetails.SubjectID = (Int32)Studentparsing["Table4"][i]["SubjectId"];

                    _SubjectDetailsList.Add(_SubjectDetails);
                }

                GlobalStudentSubjects.SubjectList = _SubjectDetailsList;

                _StudentDashboardDetails.OverallAvailableTest = _OverallAvailableTestList;
                _StudentDashboardDetails.OverallLastFiveTest = _LastFiveTestList;
                _StudentDashboardDetails.StudentRank = _StudentRankDetails;
                _StudentDashboardDetails.SubjectPercentage = _SubjectPercentageList;
                _StudentDashboardDetails.StudentGeneralDetails = objStudentLoginDetails;
                return View(_StudentDashboardDetails);
            }
            else
            {
                _StudentDashboardDetails = null;
                return View(_StudentDashboardDetails);
            }
        }
        public RedirectToRouteResult  SetStudentID(int Id)
        {
            GlobalStudentID = Id;
            return RedirectToAction("Index","CustomerSupport");
        }
        public ActionResult CheckStudentDetails()
        {
            
            return View();
        }
    }
}
