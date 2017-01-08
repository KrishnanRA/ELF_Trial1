using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ELF_Trial1.Models;
using ELF_Trial1.Models.Student;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace ELF_Trial1.Controllers
{
    [SessionExpire]
    
    public class StudentController : Controller
    {
        //
        // GET: /Student/
        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        web_ws.Ielf_web_wsClient StudentWeb = new web_ws.Ielf_web_wsClient();
        public String String_GetStudentDashStudentWebboardDetails = "";
        public static Int32 GlobalTestID = 0;
        public static StudentDashboard _studentDashboar = new StudentDashboard();

        public ActionResult Dashboard()
        {
            string dashboardOutput = StudentWeb.GetStudentDetails(Convert.ToInt32(Session["UserId"].ToString()));
            JObject studentUnderParentparsing = JObject.Parse(dashboardOutput);

            // load login details in student general class
            StudentGeneralDetails _studentDetails = new StudentGeneralDetails();
            _studentDetails.BoardName = (String)studentUnderParentparsing["Table"][0]["BoardName"];
            _studentDetails.CityName = (String)studentUnderParentparsing["Table"][0]["CityName"];
            _studentDetails.ClassName = (String)studentUnderParentparsing["Table"][0]["ClassName"];
            _studentDetails.DistrictName = (String)studentUnderParentparsing["Table"][0]["DistrictName"];
            _studentDetails.EmailAddress = (String)studentUnderParentparsing["Table"][0]["EmailAddress"];
            _studentDetails.Name = (String)studentUnderParentparsing["Table"][0]["FirstName"];
            _studentDetails.PhoneNumber = (String)studentUnderParentparsing["Table"][0]["PhoneNumber"];
            _studentDetails.StateName = (String)studentUnderParentparsing["Table"][0]["StateName"];
            _studentDetails.StudentId = (Int32)studentUnderParentparsing["Table"][0]["StudentId"];
            _studentDetails.InstitutionName = (String)studentUnderParentparsing["Table"][0]["InstitutionName"];

            // Create Object for class 
            StudentDashboard _StudentDashboardDetails = new StudentDashboard();
            StudentRank _StudentRankDetails = new StudentRank();
            OverallAvailableTest _StudentOverallAvailableTest = new OverallAvailableTest();
            OverallLastFiveTest _StudentOverallLastFiveTest = new OverallLastFiveTest();
            // Get Dashboard Details with respect to student id form service
            String _GetStudentDashboardDetails = StudentWeb.StudentDashboardDetails(Convert.ToInt32(Session["UserId"].ToString()));
            // Get Student Rank Details form service with respect to student id
            String _GetStudentDashboardDetails1 = StudentWeb.GetStudentDashboard(Convert.ToInt32(Session["UserId"].ToString()));
            // Parse the student dashbord details form Json Student dashboard
            JObject Studentparsing = JObject.Parse(_GetStudentDashboardDetails);
            // parse the student rank details form Json Student dashboard Details1
            JObject Studentparsing1 = JObject.Parse(_GetStudentDashboardDetails1);
            // 
            int _studentPerformanceCount = Studentparsing["Table3"].Count();

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

            if (_LastFiveTestCountCount > 5)
            {
                _LastFiveTestCountCount = 5;
            }

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
            int _OverallAvailableTestCount = (Int32)Studentparsing["Table3"].Count();
            //Studentparsing["Table3"].Count();
            List<OverallAvailableTest> _OverallAvailableTestList = new List<OverallAvailableTest>();
            

            //If Test Count is more than 5 show only  five test
            //It is not last five test
            if (_OverallAvailableTestCount > 5)
            {
                _OverallAvailableTestCount = 5;
            }

            // adding five test details
            for (int i = 0; i < _OverallAvailableTestCount; i++)
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


            List<StudentSubjects> _SubjectDetailsList = new List<StudentSubjects>();
            for (int i = 0; i < _SubjectCount; i++)
            {
                StudentSubjects _SubjectDetails = new StudentSubjects();
                _SubjectDetails.SubjectName = (String)Studentparsing["Table4"][i]["SubjectName"];
                _SubjectDetails.SubjectID = (Int32)Studentparsing["Table4"][i]["SubjectId"];

                _SubjectDetailsList.Add(_SubjectDetails);
            }

            // Get the Rows Count in Good, Bad and Average 

            int goodPerformingCount = Studentparsing["Table5"].Count();  // Good performace
            int averagePerformingCount = Studentparsing["Table6"].Count(); // Average performance
            int badPerformingCount = Studentparsing["Table7"].Count();   // Bad performance
            //  Create Object for Good, Average, Bad list
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
            // Add Average performing list
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
            //  Add Bad Performing List
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

            _StudentDashboardDetails.OverallAvailableTest = _OverallAvailableTestList;
            _StudentDashboardDetails.OverallLastFiveTest = _LastFiveTestList;
            _StudentDashboardDetails.StudentRank = _StudentRankDetails;
            _StudentDashboardDetails.SubjectPercentage = _SubjectPercentageList;
            _StudentDashboardDetails.StudentGeneralDetails = _studentDetails;
            _StudentDashboardDetails.GoodPerformingSubject = _goodPerformingSubjectList;
            _StudentDashboardDetails.AveragePerformingSubject = _averagePerformingSubjectList;
            _StudentDashboardDetails.BadPerformingSubject = _badPerformingSubjectList;

            return View(_StudentDashboardDetails);
        }

        public ActionResult Test1()
        {
            if (Session["UserType"] == null)
            {
                return RedirectToAction("Index", "ELFWeb");
            }

            string dashboardOutput = StudentWeb.GetStudentDetails(Convert.ToInt32(Session["UserId"].ToString()));
            JObject studentUnderParentparsing = JObject.Parse(dashboardOutput);

            // load login details in student general class
            StudentGeneralDetails _studentDetails = new StudentGeneralDetails();
            _studentDetails.BoardName = (String)studentUnderParentparsing["Table"][0]["BoardName"];
            _studentDetails.CityName = (String)studentUnderParentparsing["Table"][0]["CityName"];
            _studentDetails.ClassName = (String)studentUnderParentparsing["Table"][0]["ClassName"];
            _studentDetails.DistrictName = (String)studentUnderParentparsing["Table"][0]["DistrictName"];
            _studentDetails.EmailAddress = (String)studentUnderParentparsing["Table"][0]["EmailAddress"];
            _studentDetails.Name = (String)studentUnderParentparsing["Table"][0]["FirstName"];
            _studentDetails.PhoneNumber = (String)studentUnderParentparsing["Table"][0]["PhoneNumber"];
            _studentDetails.StateName = (String)studentUnderParentparsing["Table"][0]["StateName"];
            _studentDetails.StudentId = (Int32)studentUnderParentparsing["Table"][0]["StudentId"];
            _studentDetails.InstitutionName = (String)studentUnderParentparsing["Table"][0]["InstitutionName"];
            //  string StudentDetails = studentweb.CheckStudentLogin(Username, Password);
            return View(_studentDetails);
        }
        public ActionResult TestMain()
        {
            // model student general details
            string dashboardOutput = StudentWeb.GetStudentDetails(Convert.ToInt32(Session["UserId"].ToString()));
            JObject _StudentDetailsParsing = JObject.Parse(dashboardOutput);

            StudentGeneralDetails _studentDetails = new StudentGeneralDetails();
            _studentDetails.BoardName = (String)_StudentDetailsParsing["Table"][0]["BoardName"];
            _studentDetails.CityName = (String)_StudentDetailsParsing["Table"][0]["CityName"];
            _studentDetails.ClassName = (String)_StudentDetailsParsing["Table"][0]["ClassName"];
            _studentDetails.DistrictName = (String)_StudentDetailsParsing["Table"][0]["DistrictName"];
            _studentDetails.EmailAddress = (String)_StudentDetailsParsing["Table"][0]["EmailAddress"];
            _studentDetails.Name = (String)_StudentDetailsParsing["Table"][0]["FirstName"];
            _studentDetails.PhoneNumber = (String)_StudentDetailsParsing["Table"][0]["PhoneNumber"];
            _studentDetails.StateName = (String)_StudentDetailsParsing["Table"][0]["StateName"];
            _studentDetails.StudentId = (Int32)_StudentDetailsParsing["Table"][0]["StudentId"];
            _studentDetails.InstitutionName = (String)_StudentDetailsParsing["Table"][0]["InstitutionName"];

            string studentDashboard = StudentWeb.StudentDashboardDetails(Convert.ToInt32(Session["UserId"].ToString()));
            JObject _studentDashboardParse = JObject.Parse(studentDashboard);

            int _SubjectCount = (Int32)_studentDashboardParse["Table4"].Count();
            List<SubjectDetails> _SubjectDetailsList = new List<SubjectDetails>();
            for (int i = 0; i < _SubjectCount; i++)
            {
                SubjectDetails _SubjectDetails = new SubjectDetails();
                _SubjectDetails.SubjectName = (String)_studentDashboardParse["Table4"][i]["SubjectName"];
                _SubjectDetails.SubjectID = (Int32)_studentDashboardParse["Table4"][i]["SubjectId"];

                _SubjectDetailsList.Add(_SubjectDetails);
            }
            //Passing Parameter to Webservice and receving in string
            string _GetOverallPendingTestDetails = StudentWeb.GetPendingTests(_studentDetails.StudentId, "All");

            //Received string and changed to json object
            JObject TestMainOverallParsing = JObject.Parse(_GetOverallPendingTestDetails);

            //To Identify Number of Values in JSON to Iterate
            Int32 _TestMainOverallCount = (int)TestMainOverallParsing["Table"].Count();

            //Created to pass in Main Class for Model View
            TestMain _ModelTestMain = new TestMain();

            //Created a List for Model
            List<OverallTest> _ListOverallTest = new List<OverallTest>();
            List<PublicTest> _ListPublicTest = new List<PublicTest>();


            for (int i = 0; i < _TestMainOverallCount; i++)
            {
                //Created Temprory object in List
                OverallTest _OverallTest = new OverallTest();
                _OverallTest.TestID = (Int32)TestMainOverallParsing["Table"][i]["TestId"];
                _OverallTest.StudentName = (String)TestMainOverallParsing["Table"][i]["Percentage"];
                _OverallTest.SubjectName = (String)TestMainOverallParsing["Table"][i]["SubjectName"];
                _OverallTest.Description = (String)TestMainOverallParsing["Table"][i]["Description"];
                //Adding object in List    
                _ListOverallTest.Add(_OverallTest);
            }
            _ModelTestMain.StudentGeneralDetails = _studentDetails;
            _ModelTestMain.GlobalStudentSubjectDetails = _SubjectDetailsList;
            _ModelTestMain.OverallTest = _ListOverallTest;
            return View(_ModelTestMain);
        }
        public ActionResult Test(int TestID)
        {
            if (Session["UserType"] == null)
            {
                return RedirectToAction("Index", "ELFWeb");
            }

            GlobalTestID = TestID;
            string _GetParticularTestDetails = StudentWeb.GetTestQuestions(TestID);

            JObject ParticularTestDetailsParsing = JObject.Parse(_GetParticularTestDetails);

            Int32 _ParticularTestDetailsParsingCount = (int)ParticularTestDetailsParsing["Table"].Count();

            List<GetParticularTestDetail> _GetParticularTestDetail = new List<GetParticularTestDetail>();

            QuestionList _TestQuestionList = new QuestionList();
            for (int i = 0; i < _ParticularTestDetailsParsingCount; i++)
            {
                GetParticularTestDetail _Question = new GetParticularTestDetail();
                _Question.QuestionNos = i + 1;
                _Question.QuestionId = (Int32)ParticularTestDetailsParsing["Table"][i]["QuestionId"];
                _Question.Question = (String)ParticularTestDetailsParsing["Table"][i]["Question"];
                _Question.OptionA = (String)ParticularTestDetailsParsing["Table"][i]["OptionA"];
                _Question.OptionB = (String)ParticularTestDetailsParsing["Table"][i]["OptionB"];
                _Question.OptionC = (String)ParticularTestDetailsParsing["Table"][i]["OptionC"];
                _Question.OptionD = (String)ParticularTestDetailsParsing["Table"][i]["OptionD"];
                _Question.Answer = (String)ParticularTestDetailsParsing["Table"][i]["Answer"];
                _GetParticularTestDetail.Add(_Question);
            }

            //StudentChosen
            List<StudentAnswered> _StudentChosenList = new List<StudentAnswered>();
            for (int i = 0; i < _ParticularTestDetailsParsingCount; i++)
            {
                StudentAnswered _StudentChosen = new StudentAnswered();
                _StudentChosen.QuestionID = i + 1;
                _StudentChosen.TrueQuestionID = (Int32)ParticularTestDetailsParsing["Table"][i]["QuestionId"];
                _StudentChosen.optionsSelected = null;
                _StudentChosenList.Add(_StudentChosen);
            }
            _TestQuestionList.TestID = TestID;
            _TestQuestionList.AnsweredList = _StudentChosenList;
            _TestQuestionList.QuestionLists = _GetParticularTestDetail;
            return View(_TestQuestionList);
        }
        public ActionResult TestSummary(Int32 TestId)
        {
            if (Session["UserType"] == null)
            {
                return RedirectToAction("Index", "ELFWeb");
            }
            TestReport _TestReport = new TestReport();

            String _GetTestReportOverviewDetails = StudentWeb.GetTestOverview(Convert.ToInt32(Session["UserId"].ToString()), TestId);
            String _GetTestReportDetailsDetails = StudentWeb.GetDetailedTestReport(Convert.ToInt32(Session["UserId"].ToString()), TestId);

            JObject StudentTestReportOverviewparsing = JObject.Parse(_GetTestReportOverviewDetails);
            JObject StudentTestReportDetailsparsing = JObject.Parse(_GetTestReportDetailsDetails);

            TestOverview _TestSummary = new TestOverview();
            _TestSummary.SubjectId = (Int32)StudentTestReportOverviewparsing["Table"][0]["SubjectId"];
            _TestSummary.SubjectName = (String)StudentTestReportOverviewparsing["Table"][0]["SubjectName"];
            // _TestSummary.CorrectAnswers = (string)StudentTestReportOverviewparsing["Table"][0]["CorrectAnswers"];
            _TestSummary.Percentage = (Int32)StudentTestReportOverviewparsing["Table"][0]["Percentage"];
            //_TestSummary.Description = (String)StudentTestReportOverviewparsing["Table"][0]["Description"];
            _TestSummary.TestId = (Int32)StudentTestReportOverviewparsing["Table"][0]["TestId"];

            List<TestDetails> _TestDetailsList = new List<TestDetails>();

            for (int i = 0; i < 20; i++)
            {
                TestDetails _TestDetails = new TestDetails();
                _TestDetails.Question = (String)StudentTestReportDetailsparsing["Table"][i]["Question"];
                _TestDetails.Answer = (String)StudentTestReportDetailsparsing["Table"][i]["Answer"];
                _TestDetails.AnswerStatus = (String)StudentTestReportDetailsparsing["Table"][i]["AnswerStatus"];
                _TestDetails.QNumber = i + 1;
                _TestDetailsList.Add(_TestDetails);
            }
            _TestReport.TestDetailList = _TestDetailsList;
            _TestReport.TestOverviews = _TestSummary;
            return View(_TestReport);
        }
        public JsonResult SubmitTest(StudentAnswered[] QA)
        {
           

            Int32 _StudentID = Convert.ToInt32(Session["UserId"].ToString());
            SubmitTest _SubmitTest = new SubmitTest();

            String _TestSubmitResult = _SubmitTest.SubmitTestQuestions(_StudentID, GlobalTestID, QA);
            JObject ParsingSubmitResult = JObject.Parse(_TestSubmitResult);
            String _Result = (string)ParsingSubmitResult["Table"][0]["OutputStatus"];
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Report()
        {

            {
              
                string dashboardOutput = StudentWeb.GetStudentDetails(Convert.ToInt32(Session["UserId"].ToString()));
                JObject _StudentDashboardParsing = JObject.Parse(dashboardOutput);

                StudentGeneralDetails _studentDetails = new StudentGeneralDetails();
                _studentDetails.BoardName = (String)_StudentDashboardParsing["Table"][0]["BoardName"];
                _studentDetails.CityName = (String)_StudentDashboardParsing["Table"][0]["CityName"];
                _studentDetails.ClassName = (String)_StudentDashboardParsing["Table"][0]["ClassName"];
                _studentDetails.DistrictName = (String)_StudentDashboardParsing["Table"][0]["DistrictName"];
                _studentDetails.EmailAddress = (String)_StudentDashboardParsing["Table"][0]["EmailAddress"];
                _studentDetails.Name = (String)_StudentDashboardParsing["Table"][0]["FirstName"];
                _studentDetails.PhoneNumber = (String)_StudentDashboardParsing["Table"][0]["PhoneNumber"];
                _studentDetails.StateName = (String)_StudentDashboardParsing["Table"][0]["StateName"];
                _studentDetails.StudentId = (Int32)_StudentDashboardParsing["Table"][0]["StudentId"];
                _studentDetails.InstitutionName = (String)_StudentDashboardParsing["Table"][0]["InstitutionName"];

                ReportModel ReportDetails = new ReportModel();

                LessionWiseReportModel _StudentReportDetails = new LessionWiseReportModel();

                string studentDashboard = StudentWeb.StudentDashboardDetails(Convert.ToInt32(Session["UserId"].ToString()));
                JObject _studentDashboardParse = JObject.Parse(studentDashboard);

                int _SubjectCount = (Int32)_studentDashboardParse["Table4"].Count();
                List<SubjectDetails> _SubjectDetailsList = new List<SubjectDetails>();
                for (int i = 0; i < _SubjectCount; i++)
                {
                    SubjectDetails _SubjectDetails = new SubjectDetails();
                    _SubjectDetails.SubjectName = (String)_studentDashboardParse["Table4"][i]["SubjectName"];
                    _SubjectDetails.SubjectID = (Int32)_studentDashboardParse["Table4"][i]["SubjectId"];

                    _SubjectDetailsList.Add(_SubjectDetails);
                }


                ReportDetails.StudentGeneralDetails = _studentDetails;

                string _GetStudentDashboardDetails = StudentWeb.GetLessionWiseReport(_studentDetails.StudentId, _SubjectDetailsList[0].SubjectID);

                JObject Studentparsing = JObject.Parse(_GetStudentDashboardDetails);


                int _lessionReportCount = (Int32)Studentparsing["Table"].Count();

                List<LessionWiseReportModel> _lessionWiseReportModelList = new List<LessionWiseReportModel>();

                for (int i = 0; i < _lessionReportCount; i++)
                {
                    LessionWiseReportModel _lessionWiseReportModel = new LessionWiseReportModel();
                    _lessionWiseReportModel.StudentId = (Int32)Studentparsing["Table"][i]["StudentId"];
                    _lessionWiseReportModel.SubjectId = (Int32)Studentparsing["Table"][i]["SubjectId"];
                    _lessionWiseReportModel.LessionName = (String)Studentparsing["Table"][i]["LessionName"];
                    _lessionWiseReportModel.Percentage = (Int32)Studentparsing["Table"][i]["Percentage"];
                    _lessionWiseReportModel.QuestionsAsked = (Int32)Studentparsing["Table"][i]["QuestionsAsked"];
                    _lessionWiseReportModel.CorrectAnswers = (Int32)Studentparsing["Table"][i]["CorrectAnswers"];
                    _lessionWiseReportModelList.Add(_lessionWiseReportModel);
                }



                //Completed TEst Details
                string _GetTestCompletedDetails = StudentWeb.GetWritenTestDetails(_studentDetails.StudentId);
                JObject WrittenTestparsing = JObject.Parse(_GetTestCompletedDetails);
                int _WrittenTestCount = (Int32)WrittenTestparsing["Table"].Count();
                List<CompletedTestList> _CompletedTestList = new List<CompletedTestList>();
                for (int i = 0; i < _WrittenTestCount; i++)
                {
                    CompletedTestList _CompletedTestListReportModel = new CompletedTestList();
                    _CompletedTestListReportModel.Description = (String)WrittenTestparsing["Table"][i]["Description"];
                    _CompletedTestListReportModel.MarksScored = (Int32)WrittenTestparsing["Table"][i]["MarksScored"];
                    _CompletedTestListReportModel.SubjectId = (Int32)WrittenTestparsing["Table"][i]["SubjectId"];
                    if (_CompletedTestListReportModel.SubjectId == 1)
                    {
                        _CompletedTestListReportModel.SubjectName = "Maths";
                    }
                    else if (_CompletedTestListReportModel.SubjectId == 2)
                    {
                        _CompletedTestListReportModel.SubjectName = "Science";
                    }
                    else if (_CompletedTestListReportModel.SubjectId == 3)
                    {
                        _CompletedTestListReportModel.SubjectName = "Social";
                    }
                    else if (_CompletedTestListReportModel.SubjectId == 4)
                    {
                        _CompletedTestListReportModel.SubjectName = "Maths";
                    }
                    else if (_CompletedTestListReportModel.SubjectId == 5)
                    {
                        _CompletedTestListReportModel.SubjectName = "Chemistry";
                    }
                    else if (_CompletedTestListReportModel.SubjectId == 6)
                    {
                        _CompletedTestListReportModel.SubjectName = "Physics";
                    }
                    else if (_CompletedTestListReportModel.SubjectId == 7)
                    {
                        _CompletedTestListReportModel.SubjectName = "Computer";
                    }
                    else if (_CompletedTestListReportModel.SubjectId == 8)
                    {
                        _CompletedTestListReportModel.SubjectName = "Biology";
                    }
                    _CompletedTestListReportModel.TestId = (Int32)WrittenTestparsing["Table"][i]["TestId"];


                    _CompletedTestList.Add(_CompletedTestListReportModel);
                }
                //--Completed TEst Details--//



                ReportDetails.LessonWiseReportList = _lessionWiseReportModelList;
                ReportDetails.StudentSubjectList = _SubjectDetailsList;
                ReportDetails.StudentGeneralDetails = _studentDetails;
                ReportDetails.CompletedTestList = _CompletedTestList;
                return View(ReportDetails);
            }
        }
        public ActionResult Notification()
        {
            if (Session["UserType"] == null)
            {
                return RedirectToAction("Index", "ELFWeb");
            }
            string dashboardOutput = StudentWeb.GetStudentDetails(Convert.ToInt32(Session["UserId"].ToString()));
            JObject StudentGeneralDetailsParse = JObject.Parse(dashboardOutput);

            StudentGeneralDetails _studentDetails = new StudentGeneralDetails();
            _studentDetails.BoardName = (String)StudentGeneralDetailsParse["Table"][0]["BoardName"];
            _studentDetails.CityName = (String)StudentGeneralDetailsParse["Table"][0]["CityName"];
            _studentDetails.ClassName = (String)StudentGeneralDetailsParse["Table"][0]["ClassName"];
            _studentDetails.DistrictName = (String)StudentGeneralDetailsParse["Table"][0]["DistrictName"];
            _studentDetails.EmailAddress = (String)StudentGeneralDetailsParse["Table"][0]["EmailAddress"];
            _studentDetails.Name = (String)StudentGeneralDetailsParse["Table"][0]["FirstName"];
            _studentDetails.PhoneNumber = (String)StudentGeneralDetailsParse["Table"][0]["PhoneNumber"];
            _studentDetails.StateName = (String)StudentGeneralDetailsParse["Table"][0]["StateName"];
            _studentDetails.StudentId = (Int32)StudentGeneralDetailsParse["Table"][0]["StudentId"];
            _studentDetails.InstitutionName = (String)StudentGeneralDetailsParse["Table"][0]["InstitutionName"];
            _studentDashboar.StudentGeneralDetails = _studentDetails;
            return View(_studentDashboar);
        }
        public JsonResult StudentLogin(String Username, String Password)
        {
            // string output = studentweb.CheckStudentLogin(Username, Password);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLessonReport(int SubjectID)
        {
            ReportModel ReportDetails = new ReportModel();

            LessionWiseReportModel _StudentReportDetails = new LessionWiseReportModel();

            string _GetLessonReportString = StudentWeb.GetLessionWiseReport(Convert.ToInt32(Session["UserId"].ToString()), SubjectID);
            JObject Studentparsing = JObject.Parse(_GetLessonReportString);

            int _lessionReportCount = (Int32)Studentparsing["Table"].Count();

            List<LessionWiseReportModel> _lessionWiseReportModelList = new List<LessionWiseReportModel>();

            for (int i = 0; i < _lessionReportCount; i++)
            {
                LessionWiseReportModel _lessionWiseReportModel = new LessionWiseReportModel();
                _lessionWiseReportModel.StudentId = (Int32)Studentparsing["Table"][i]["StudentId"];
                _lessionWiseReportModel.SubjectId = (Int32)Studentparsing["Table"][i]["SubjectId"];
                _lessionWiseReportModel.LessionName = (String)Studentparsing["Table"][i]["LessionName"];
                _lessionWiseReportModel.Percentage = (Int32)Studentparsing["Table"][i]["Percentage"];
                _lessionWiseReportModel.QuestionsAsked = (Int32)Studentparsing["Table"][i]["QuestionsAsked"];
                _lessionWiseReportModel.CorrectAnswers = (Int32)Studentparsing["Table"][i]["CorrectAnswers"];
                _lessionWiseReportModelList.Add(_lessionWiseReportModel);
            }
            ReportDetails.LessonWiseReportList = _lessionWiseReportModelList;
            return Json(ReportDetails, JsonRequestBehavior.AllowGet);
        }

        //   public ActionResult TopicDetails(Int32 SubjectID, Int32 LessonID)
        //    {

        //        TestReport _TestReport = new TestReport();

        //        String _GetTestReportOverviewDetails = StudentWeb.(GlobalStudentClass.StudentId, SubjectID,LessonID);
        //        //String _GetTestReportDetailsDetails = StudentWeb.GetDetailedTestReport(GlobalStudentClass.StudentId, TestId);

        //        JObject StudentTestReportOverviewparsing = JObject.Parse(_GetTestReportOverviewDetails);
        //        JObject StudentTestReportDetailsparsing = JObject.Parse(_GetTestReportDetailsDetails);

        //        TestOverview _TestSummary = new TestOverview();
        //        _TestSummary.SubjectId = (Int32)StudentTestReportOverviewparsing["Table"][0]["SubjectId"];
        //        _TestSummary.SubjectName = (String)StudentTestReportOverviewparsing["Table"][0]["SubjectName"];
        //        // _TestSummary.CorrectAnswers = (string)StudentTestReportOverviewparsing["Table"][0]["CorrectAnswers"];
        //        _TestSummary.Percentage = (Int32)StudentTestReportOverviewparsing["Table"][0]["Percentage"];
        //        //_TestSummary.Description = (String)StudentTestReportOverviewparsing["Table"][0]["Description"];
        //        _TestSummary.TestId = (Int32)StudentTestReportOverviewparsing["Table"][0]["TestId"];

        //        List<TestDetails> _TestDetailsList = new List<TestDetails>();

        //        for (int i = 0; i < 20; i++)
        //        {
        //            TestDetails _TestDetails = new TestDetails();
        //            _TestDetails.Question = (String)StudentTestReportDetailsparsing["Table"][i]["Question"];
        //            _TestDetails.Answer = (String)StudentTestReportDetailsparsing["Table"][i]["Answer"];
        //            _TestDetails.AnswerStatus = (String)StudentTestReportDetailsparsing["Table"][i]["AnswerStatus"];
        //            _TestDetails.QNumber = i + 1;
        //            _TestDetailsList.Add(_TestDetails);
        //        }
        //        _TestReport.TestDetailList = _TestDetailsList;
        //        _TestReport.TestOverviews = _TestSummary;
        //        return View(_TestReport);
        //    }
        //}

        public ActionResult FeedBack()
        {
            return View();
        }

        public JsonResult SubmitFeedBack(string Feedback)
        {
            String _Result = "";
            // Check UserType 
            if (Session["UserType"].ToString() == "Student")
            {
                string FeedBack = StudentWeb.SaveUserFeedback(Convert.ToInt32(Session["UserId"].ToString()), Feedback, Session["UserType"].ToString());
                JObject ParsingFeedback = JObject.Parse(FeedBack);
                _Result = (string)ParsingFeedback["Table"][0]["OutputStatus"];

            }
            return Json(_Result);
        }

        public ActionResult Coupon()
        {

            return View();
        }

        public JsonResult SubmitCoupon(string CoupenCode)
        {
            // Validate the Coupon
            StudentGeneralDetails _studentDetails = new StudentGeneralDetails();
            string Coupon = StudentWeb.CheckCoupenCode(Convert.ToInt32(Session["UserId"].ToString()), CoupenCode);
            JObject ParsingCoupon = JObject.Parse(Coupon);
            // Check the Json Result to validate the Coupon
            string _Result = (string)ParsingCoupon["Table"][0]["StatusCode"];
            if (_Result == "9996")     // Status Code
            {
                return Json("success");
            }
            else
            {
                return Json("failed");
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon(); // it will clear the session
                               // return Json("success");
            return RedirectToAction("index", "ELFWeb");
        }


        public ActionResult Feed()
        {
            return View();
        }

        public JsonResult SubmitFeed()
        {

            return Json("");
        }
    }
}
