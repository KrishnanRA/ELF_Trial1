using ELF_Trial1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ELF_Trial1.Models.Parent;
using Newtonsoft.Json.Linq;

namespace ELF_Trial1.Controllers
{
    public class ParentController : Controller
    {
        // WebService Object
        web_ws.Ielf_web_wsClient ParentWeb = new web_ws.Ielf_web_wsClient();
        // Student Dashboard Object
        //


        // GET: /Parent/

        public ActionResult Dashboard()
        {

            if (Session["UserType"] == null)
            {
                return RedirectToAction("Index", "ELFWeb");
            }

            //  Get Student Under Parent
            string _studentUnderParent = ParentWeb.GetStudentUnderParent(GlobalParentDetails.ParentId);
            // Parse the value to string
            JObject studentUnderParentparsing = JObject.Parse(_studentUnderParent);
            // Parent DashBoard Object 
            ParentDashboard _parentDashboard = new ParentDashboard();
            // List Created to Load the Student Under Parent
            List<StudentUnderParent> _ListStudentUnderParent = new List<StudentUnderParent>();
            // Get the table Count
            int _studentUnderParentCount = studentUnderParentparsing["Table"].Count();

            if (_studentUnderParentCount > 0)
            {
                // Load the Student details in List Using For loop
                for (int i = 0; i < _studentUnderParentCount; i++)
                {
                    StudentUnderParent _ParentUnderStudent = new StudentUnderParent();
                    _ParentUnderStudent.StudentId = (Int32)studentUnderParentparsing["Table"][i]["StudentId"];
                    _ParentUnderStudent.StudentName = (string)studentUnderParentparsing["Table"][i]["StudentName"];
                    _ListStudentUnderParent.Add(_ParentUnderStudent);
                }

                GlobalStudentClass.StudentId = (Int32)studentUnderParentparsing["Table"][0]["StudentId"];
                // Get Student Details
                string _getStudentDetails = ParentWeb.GetStudentDetails(GlobalStudentClass.StudentId);
                // Parse Json to String 
                JObject _StudentDetailsParse = JObject.Parse(_getStudentDetails);
                // Load the Student Details
                GlobalStudentClass.BoardName = (string)_StudentDetailsParse["Table"][0]["BoardId"];
                GlobalStudentClass.ClassName = (string)_StudentDetailsParse["Table"][0]["ClassName"];
                GlobalStudentClass.CityName = (string)_StudentDetailsParse["Table"][0]["CityName"];
                GlobalStudentClass.DistrictName = (string)_StudentDetailsParse["Table"][0]["DistrictName"];
                GlobalStudentClass.EmailAddress = (string)_StudentDetailsParse["Table"][0]["EmailAddress"];
                GlobalStudentClass.InstitutionName= (string)_StudentDetailsParse["Table"][0]["InstitutionName"];
                GlobalStudentClass.PhoneNumber = (string)_StudentDetailsParse["Table"][0]["PhoneNumber"];
                GlobalStudentClass.StateName= (string)_StudentDetailsParse["Table"][0]["StateName"];

                // Student Details from parent Dashboard module class

                StudentGeneralDetails _studentGeneralDetails = new StudentGeneralDetails();
                // Load general details in student General Class
                _studentGeneralDetails.StudentId = GlobalStudentClass.StudentId;
                _studentGeneralDetails.StudentName = GlobalStudentClass.StudentName;
                _studentGeneralDetails.BoardName = GlobalStudentClass.BoardName;
                _studentGeneralDetails.ClassName = GlobalStudentClass.ClassName;
                _studentGeneralDetails.DistrictName = GlobalStudentClass.DistrictName;
                _studentGeneralDetails.EmailAddress = GlobalStudentClass.EmailAddress;
                _studentGeneralDetails.InstitutionName = GlobalStudentClass.InstitutionName;
                _studentGeneralDetails.PhoneNumber = GlobalStudentClass.PhoneNumber;
                _studentGeneralDetails.StateName = GlobalStudentClass.StateName;

                StudentRank _StudentRankDetails = new StudentRank();
                OverallAvailableTest _StudentOverallAvailableTest = new OverallAvailableTest();
                OverallLastFiveTest _StudentOverallLastFiveTest = new OverallLastFiveTest();

                String _GetStudentDashboardDetails = ParentWeb.StudentDashboardDetails(GlobalStudentClass.StudentId);
                String _GetStudentDashboardDetails1 = ParentWeb.GetStudentDashboard(GlobalStudentClass.StudentId);

                JObject Studentparsing = JObject.Parse(_GetStudentDashboardDetails);
                JObject Studentparsing1 = JObject.Parse(_GetStudentDashboardDetails1);

                _StudentRankDetails.CityRank = (Int32)Studentparsing1["Table"][0]["CityRank"];
                _StudentRankDetails.DistrictRank = (Int32)Studentparsing1["Table"][0]["DistrictRank"];
                _StudentRankDetails.InstitutionRank = (Int32)Studentparsing1["Table"][0]["InstitutionRank"];
                _StudentRankDetails.StateRank = (Int32)Studentparsing1["Table"][0]["StateRank"];

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
                // adding five test details
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

                // Report Subjectwise performance

                int goodPerformingCount = Studentparsing["Table5"].Count();
                int averagePerformingCount = Studentparsing["Table6"].Count();
                int badPerformingCount = Studentparsing["Table7"].Count();

                List<GoodPerformingSubject> _goodPerformingSubjectList = new List<GoodPerformingSubject>();
                List<AveragePerformingSubject> _averagePerformingSubjectList = new List<AveragePerformingSubject>();
                List<BadPerformingSubject> _badPerformingSubjectList = new List<BadPerformingSubject>();

                // Add good performing into list
                #region Good Performing
                if (goodPerformingCount > 0)
                {
                    if (goodPerformingCount > 5)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            GoodPerformingSubject _goodPerformingSubject = new GoodPerformingSubject();
                            _goodPerformingSubject.SubjectId = (Int32)Studentparsing["Table5"][i]["SubjectId"];
                            _goodPerformingSubject.SubjectName = (string)Studentparsing["Table5"][i]["SubjectName"];
                            _goodPerformingSubject.LessionId = (Int32)Studentparsing["Table5"][i]["LessionId"];
                            _goodPerformingSubject.LessionName = (string)Studentparsing["Table5"][i]["LessionName"];
                            _goodPerformingSubject.Topic = (string)Studentparsing["Table5"][i]["Topic"];
                            _goodPerformingSubject.Percentage = (Int32)Studentparsing["Table5"][i]["Percentage"];
                            _goodPerformingSubjectList.Add(_goodPerformingSubject);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < goodPerformingCount; i++)
                        {
                            GoodPerformingSubject _goodPerformingSubject = new GoodPerformingSubject();
                            _goodPerformingSubject.SubjectId = (Int32)Studentparsing["Table5"][i]["SubjectId"];
                            _goodPerformingSubject.SubjectName = (string)Studentparsing["Table5"][i]["SubjectName"];
                            _goodPerformingSubject.LessionId = (Int32)Studentparsing["Table5"][i]["LessionId"];
                            _goodPerformingSubject.LessionName = (string)Studentparsing["Table5"][i]["LessionName"];
                            _goodPerformingSubject.Topic = (string)Studentparsing["Table5"][i]["Topic"];
                            _goodPerformingSubject.Percentage = (Int32)Studentparsing["Table5"][i]["Percentage"];
                            _goodPerformingSubjectList.Add(_goodPerformingSubject);
                        }
                    }
                }
                #endregion
                // Add Average performing into List
                #region Average Performing

                if (averagePerformingCount > 0)
                {
                    if (averagePerformingCount > 5)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            AveragePerformingSubject _AveragePerformingSubject = new AveragePerformingSubject();
                            _AveragePerformingSubject.SubjectId = (Int32)Studentparsing["Table6"][i]["SubjectId"];
                            _AveragePerformingSubject.SubjectName = (string)Studentparsing["Table6"][i]["SubjectName"];
                            _AveragePerformingSubject.LessionId = (Int32)Studentparsing["Table6"][i]["LessionId"];
                            _AveragePerformingSubject.LessionName = (string)Studentparsing["Table6"][i]["LessionName"];
                            _AveragePerformingSubject.Topic = (string)Studentparsing["Table6"][i]["Topic"];
                            _AveragePerformingSubject.Percentage = (Int32)Studentparsing["Table6"][i]["Percentage"];
                            _averagePerformingSubjectList.Add(_AveragePerformingSubject);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < averagePerformingCount; i++)
                        {
                            AveragePerformingSubject _AveragePerformingSubject = new AveragePerformingSubject();
                            _AveragePerformingSubject.SubjectId = (Int32)Studentparsing["Table6"][i]["SubjectId"];
                            _AveragePerformingSubject.SubjectName = (string)Studentparsing["Table6"][i]["SubjectName"];
                            _AveragePerformingSubject.LessionId = (Int32)Studentparsing["Table6"][i]["LessionId"];
                            _AveragePerformingSubject.LessionName = (string)Studentparsing["Table6"][i]["LessionName"];
                            _AveragePerformingSubject.Topic = (string)Studentparsing["Table6"][i]["Topic"];
                            _AveragePerformingSubject.Percentage = (Int32)Studentparsing["Table6"][i]["Percentage"];
                            _averagePerformingSubjectList.Add(_AveragePerformingSubject);
                        }
                    }
                }
                #endregion
                // Add Bad performing into list
                #region Bad Performing
                if (badPerformingCount > 0)
                {
                    if (badPerformingCount > 5)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            BadPerformingSubject _BadPerformingSubject = new BadPerformingSubject();
                            _BadPerformingSubject.SubjectId = (Int32)Studentparsing["Table7"][i]["SubjectId"];
                            _BadPerformingSubject.SubjectName = (string)Studentparsing["Table7"][i]["SubjectName"];
                            _BadPerformingSubject.LessionId = (Int32)Studentparsing["Table7"][i]["LessionId"];
                            _BadPerformingSubject.LessionName = (string)Studentparsing["Table7"][i]["LessionName"];
                            _BadPerformingSubject.Topic = (string)Studentparsing["Table7"][i]["Topic"];
                            _BadPerformingSubject.Percentage = (Int32)Studentparsing["Table7"][i]["Percentage"];
                            _badPerformingSubjectList.Add(_BadPerformingSubject);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < badPerformingCount; i++)
                        {
                            BadPerformingSubject _BadPerformingSubject = new BadPerformingSubject();
                            _BadPerformingSubject.SubjectId = (Int32)Studentparsing["Table7"][i]["SubjectId"];
                            _BadPerformingSubject.SubjectName = (string)Studentparsing["Table7"][i]["SubjectName"];
                            _BadPerformingSubject.LessionId = (Int32)Studentparsing["Table7"][i]["LessionId"];
                            _BadPerformingSubject.LessionName = (string)Studentparsing["Table7"][i]["LessionName"];
                            _BadPerformingSubject.Topic = (string)Studentparsing["Table7"][i]["Topic"];
                            _BadPerformingSubject.Percentage = (Int32)Studentparsing["Table7"][i]["Percentage"];
                            _badPerformingSubjectList.Add(_BadPerformingSubject);
                        }
                    }
                }
                #endregion


                _parentDashboard.OverallAvailableTest = _OverallAvailableTestList;
                _parentDashboard.OverallLastFiveTest = _LastFiveTestList;
                _parentDashboard.StudentRank = _StudentRankDetails;
                _parentDashboard.SubjectPercentage = _SubjectPercentageList;
                _parentDashboard.StudentGeneralDetails = _studentGeneralDetails;
                _parentDashboard.GoodPerformingSubject = _goodPerformingSubjectList;
                _parentDashboard.AveragePerformingSubject = _averagePerformingSubjectList;
                _parentDashboard.BadPerformingSubject = _badPerformingSubjectList;
                return View(_parentDashboard);

            }

            else
            {
                return RedirectToAction("index", "StudentUnderParent");
            }

        }
        public ActionResult Notifications()
        {
            return View();
        }
        public ActionResult Report()
        {
            return View();
        }

        public ActionResult FeedBack()
        {
            return View();
        }

        public JsonResult SubmitFeedBack(string Feedback)
        {
            String _Result = "";
            if (GlobalStudentClass.UserType == "Parent")
            {
                int parentId = GlobalParentDetails.ParentId;
                // Send Feedback using service
                // Get the Json string form service
                string FeedBack = ParentWeb.SaveUserFeedback(parentId, Feedback, GlobalStudentClass.UserType);
                // Parse the Json to JObject
                JObject ParsingFeedback = JObject.Parse(FeedBack);
                // store the response into Result
                _Result = (string)ParsingFeedback["Table"][0]["OutputStatus"];
            }
            return Json(_Result);
        }

        // Parent Student Request
        public JsonResult ParentStudentRequest(int Studentid)
        {
            // Request Student Dashboard Access
            // Get Request response in Json
            string StudentRequest = ParentWeb.ParentStudentRequest(GlobalParentDetails.ParentId, Studentid, 1);
            // Parse the Json into JOject
            JObject ParsingStudentRequest = JObject.Parse(StudentRequest);
            // Get the Output Status
            string _Result = (string)ParsingStudentRequest["Table"][0]["OutputStatus"];
            // Return the 
            return Json(_Result);
        }


}
}
