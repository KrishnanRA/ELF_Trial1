using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELF_Trial1.Models.Student
{
    public class TestMain
    {
        public StudentGeneralDetails StudentGeneralDetails { get; set; }
        public List<OverallTest> OverallTest { get; set; }
        public List<RecommendedTest> RecommendedTest { get; set; }
        public List<PublicTest> PublicTest { get; set; }
        public List<SubjectDetails> GlobalStudentSubjectDetails { get; set; }
    }
    public class GlobalStudentSubjectDetails
    {
       
        public List<SubjectDetails> SubjectList { get; set; }
    }
    public class OverallTest
    {
        public String Description { get; set; }
        //Status:Pending
        public String Status { get; set; }
        public Int32 StudentID { get; set; }
        public String StudentName { get; set; }

        public Int32 SubjectID { get; set; }
        public String SubjectName { get; set; }

        public Int32 TestID { get; set; }
        public Int32 Type { get; set; }
    }
    public class RecommendedTest
    {
        public String Description { get; set; }
        //Status:Pending
        public String Status { get; set; }
        public Int32 StudentID { get; set; }
        public String StudentName { get; set; }

        public Int32 SubjectID { get; set; }
        public String SubjectName { get; set; }

        public Int32 TestID { get; set; }
        public Int32 Type { get; set; }
    }
    public class PublicTest
    {
        public String Description { get; set; }
        //Status:Pending
        public String Status { get; set; }
        public Int32 StudentID { get; set; }
        public String StudentName { get; set; }

        public Int32 SubjectID { get; set; }
        public String SubjectName { get; set; }

        public Int32 TestID { get; set; }
        public Int32 Type { get; set; }
    }
}