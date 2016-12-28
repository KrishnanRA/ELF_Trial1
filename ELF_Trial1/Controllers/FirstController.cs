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
    public class FirstController : Controller
    {
        //
        // GET: /First/
        web_ws.Ielf_web_wsClient StudentWeb = new web_ws.Ielf_web_wsClient();
        public static GlobalStudentSubjectDetails GlobalStudentSubjects = new GlobalStudentSubjectDetails();
        public ActionResult Index()
        {

            StudentGeneralDetails objStudentLoginDetails = new StudentGeneralDetails();
            objStudentLoginDetails.BoardName = GlobalStudentClass.BoardName;
            objStudentLoginDetails.CityName = GlobalStudentClass.CityName;
            objStudentLoginDetails.ClassName = GlobalStudentClass.ClassName;
            objStudentLoginDetails.DistrictName = GlobalStudentClass.DistrictName;
            objStudentLoginDetails.EmailAddress = GlobalStudentClass.EmailAddress;
            objStudentLoginDetails.Name = GlobalStudentClass.Name;
            objStudentLoginDetails.PhoneNumber = GlobalStudentClass.PhoneNumber;
            objStudentLoginDetails.StateName = GlobalStudentClass.StateName;
            objStudentLoginDetails.StudentId = GlobalStudentClass.StudentId;

            objStudentLoginDetails.InstitutionName = GlobalStudentClass.InstitutionName;

            StudentDashboard _StudentDashboardDetails = new StudentDashboard();
            StudentRank _StudentRankDetails = new StudentRank();
            OverallAvailableTest _StudentOverallAvailableTest = new OverallAvailableTest();
            OverallLastFiveTest _StudentOverallLastFiveTest = new OverallLastFiveTest();

            String _GetStudentDashboardDetails = StudentWeb.StudentDashboardDetails(GlobalStudentClass.StudentId);
            String _GetStudentDashboardDetails1 = StudentWeb.GetStudentDashboard(GlobalStudentClass.StudentId);

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


        public ActionResult Rerport()
        {
            StudentGeneralDetails objStudentLoginDetails = new StudentGeneralDetails();
            objStudentLoginDetails.BoardName = GlobalStudentClass.BoardName;
            objStudentLoginDetails.CityName = GlobalStudentClass.CityName;
            objStudentLoginDetails.ClassName = GlobalStudentClass.ClassName;
            objStudentLoginDetails.DistrictName = GlobalStudentClass.DistrictName;
            objStudentLoginDetails.EmailAddress = GlobalStudentClass.EmailAddress;
            objStudentLoginDetails.Name = GlobalStudentClass.Name;
            objStudentLoginDetails.PhoneNumber = GlobalStudentClass.PhoneNumber;
            objStudentLoginDetails.StateName = GlobalStudentClass.StateName;
            objStudentLoginDetails.StudentId = GlobalStudentClass.StudentId;

            objStudentLoginDetails.InstitutionName = GlobalStudentClass.InstitutionName;
            ReportModel ReportDetails = new ReportModel();

            LessionWiseReportModel _StudentReportDetails = new LessionWiseReportModel();


            ReportDetails.StudentGeneralDetails = objStudentLoginDetails;

            string _GetStudentDashboardDetails = StudentWeb.GetLessionWiseReport(GlobalStudentClass.StudentId, GlobalStudentSubjects.SubjectList[0].SubjectID);

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
            ReportDetails.LessonWiseReportList = _lessionWiseReportModelList;
            ReportDetails.StudentSubjectList = GlobalStudentSubjects.SubjectList;
            ReportDetails.StudentGeneralDetails = objStudentLoginDetails;
            return View(ReportDetails);
        }
        
    }
}
