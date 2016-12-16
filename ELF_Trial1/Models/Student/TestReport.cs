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
}