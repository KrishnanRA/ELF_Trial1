using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELF_Trial1.Models.Student
{
    
    public class ReportModel : StudentDashboard
    {
        public List<LessionWiseReportModel> LessonWiseReportList { get; set; }

        public List<SubjectDetails> StudentSubjectList { get; set; }
    }

        public class LessionWiseReportModel 
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int QuestionsAsked { get; set; }
        public int CorrectAnswers { get; set; }
        public int LessionId { get; set; }

        public int Percentage { get; set; }
        public string LessionName { get; set; }

        public string GetStringForParticularSubject(Int32 SubjectID)
        {

            return "";
        }
    }
    

}