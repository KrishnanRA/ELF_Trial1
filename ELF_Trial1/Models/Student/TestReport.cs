using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELF_Trial1.Models.Student
{
    public class TestReport
    {
        public TestOverview TestOverviews { get; set; }
        public List<TestDetails> TestDetailList { get; set; }

        public List<CompletedTestInfo> CompletedTestInfo { get; set; }
        public List<PendingTestInfo> PendingTestInfo { get; set; }
        public List<StudentInfo> StudentInfo { get; set; }
        public List<SubjectInfo> SubjectInfo { get; set; }


    }
    public class TestOverview
    {
        public String CorrectAnswers { get; set; }
        public String Description { get; set; }
        public Int32 SubjectId { get; set; }
        public String SubjectName { get; set; }
        public Int32 TestId { get; set; }
        public Int32 TotalQuestionsAsked { get; set; }

        public Int32 Percentage { get; set; }
    }
    public class TestDetails
    {
        public Int32 QNumber { get; set; }
        public String Answer { get; set; }
        public String AnswerStatus { get; set; }
        public String CreatedDate { get; set; }
        public String Question { get; set; }
        public Int32 QuestionId { get; set; }
        public Int32 StudentId { get; set; }
        public String StudentName { get; set; }
        public Int32 SubjectId { get; set; }
        public String SubjectName { get; set; }
        public Int32 TestId { get; set; }
    }

    public class CompletedTestInfo
    {
        public Int32 Percentage { get; set; }
        public Int32 SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Int32 TestId { get; set; }
        public string TestType { get; set; }
    }

    public class PendingTestInfo
    {
        public Int32 SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Int32 TestId { get; set; }
        public string TestType { get; set; }
    }

    public class StudentInfo
    {
        public string Board { get; set; }
        public string District { get; set; }
        public string FirstName  { get; set; }
        public string Institution { get; set; }
        public string LastName { get; set; }
    }

    public class SubjectInfo
    {
        public Int32 Percentage { get; set; }
        public Int32 SubjectId { get; set; }
        public string SubjectName { get; set; }

    }

    public class SubjectWiseTopicPerformance
    {
        public List<AveragePerformingSubject> AveragePerformingSubject { get; set; }


    }



    public class AveragePerformingSubject
    {
        public Int32 LessionId { get; set; }
        public string LessionName{ get; set; }
        public Int32 Percentage { get; set; }
        public string Topic { get; set; }

    }

    public class BadPerformingSubject
    {
        public Int32 LessionId { get; set; }
        public string LessionName { get; set; }
        public Int32 Percentage { get; set; }
        public string Topic { get; set; }

    }

    public class GoodPerformingSubject
    {
        public Int32 LessionId { get; set; }
        public string LessionName { get; set; }
        public Int32 Percentage { get; set; }
        public string Topic { get; set; }

    }

}