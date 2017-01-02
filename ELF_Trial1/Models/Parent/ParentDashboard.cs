using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELF_Trial1.Models.Parent
{
    public class ParentDashboard
    {

        public List<StudentUnderParent> StudentUnderParent { get; set; }

        public StudentGeneralDetails StudentGeneralDetails { get; set; }
        public StudentRank StudentRank { get; set; }
        public List<SubjectPercentage> SubjectPercentage { get; set; }   // Subject info
        public List<OverallPerformingTopic> OverallPerformingTopic { get; set; }
        public List<OverallLastFiveTest> OverallLastFiveTest { get; set; }
        public List<OverallAvailableTest> OverallAvailableTest { get; set; }

        public List<StudentSubjects> StudentSubjects { get; set; }

        public List<StudentSubjectDetails> StudentSubjectDetails { get; set;}


    }

    public class StudentUnderParent
    {
        public Int32 StudentId { get; set; }

        public string StudentName { get; set; }
    }

     public class StudentSubjectDetails
    {
        public Int32 SubjectID { get; set; }
        public string SubjectName { get; set; }
    }
    public class StudentRank
    {
        public Int32 CityRank { get; set; }
        public Int32 DistrictRank { get; set; }
        public Int32 InstitutionRank { get; set; }
        public Int32 Points { get; set; }
        public Int32 GeneraRank { get; set; }
        public Int32 StateRank { get; set; }
    }
    public class SubjectPercentage
    {
        public Int32 Percentage { get; set; }
        public Int32 SubjectId { get; set; }
        public String SubjectName { get; set; }
      
    }
    public class OverallPerformingTopic
    {
        public String PerformingType { get; set; }
        public String SubjectName { get; set; }

        public String TopicName { get; set; }

        public Int32 TopicPercentage { get; set; }
    }
    public class OverallLastFiveTest
    {
        public Int32 TestId { get; set; }

        public String TestType { get; set; }

        public Int32 SubjectID { get; set; }

        public String SubjectName { get; set; }

        public Int32 Percentage { get; set; }
    }

    public class OverallAvailableTest
    {
        public Int32 TestId { get; set; }
        public Int32 SubjectID { get; set; }
        public String SubjectName { get; set; }
        public String TestType { get; set; }
    }

    public class GlobalStudentSubjectDetails
    {

        public List<SubjectDetails> SubjectList { get; set; }
    }
}