using System;
using System.Collections.Generic;

namespace ELF_Trial1.Models.Student
{
    
    public class QuestionList
    {
        public Int32 TestID { get;set;}
        public List<GetParticularTestDetail> QuestionLists { get; set; }
        
    }
    
    public class GetParticularTestDetail
    {
        public Int32 QuestionNos { get; set; }
        public String Answer {get;set;}
        public String OptionA { get; set; }
        public String OptionB { get; set; }
        public String OptionC { get; set; }
        public String OptionD { get; set; }
        public String Question { get; set; }
        public Int32 QuestionId { get; set; }
        public Int32 StudentId { get; set; }
        public Int32 SubjectId { get; set; }
        public String SubjectName { get; set; }
        public Int32 TestId { get; set; }
        public String TheoryAnalytical { get; set; }
        public String Weightage { get; set; }

        
    }
    public class StudentAnswered {
        public Int32 QuestionID{ get; set; }
        public String optionsSelected{ get; set; }

        public Int32 TrueQuestionID { get; set; }
    }

    public class StudentChosen
    {
        public Int32 QuestionID { get; set; }
        public String optionsSelected { get; set; }

        public Int32 TrueQuestionID { get; set; }
    }
    public class StudentAnsweredList
    {
        public List<StudentAnswered> ListOfAnswered { get; set; }
    }
    public class SubmitTest
    {
        web_ws.Ielf_web_wsClient SubmitTestWeb = new web_ws.Ielf_web_wsClient();
        public Int32 StudentID { get; set; }
        public Int32 TestID { get; set; }

        public StudentAnswered[] StudentAnsweredList { get; set; }

        public String SubmitTestQuestions(Int32 StudentID,Int32 TestID,StudentAnswered[] StudentAnsweredTestDetails)
        {

            
             web_ws.Answers[] AnsweredArray = new web_ws.Answers[20];
            int i = 0;
            foreach (var dd in StudentAnsweredTestDetails)
            {
                web_ws.Answers AnsweredObj = new web_ws.Answers();
                AnsweredObj.QuestionId = dd.TrueQuestionID;
                AnsweredObj.AnswerSelected = dd.optionsSelected;
                AnsweredArray[i] = AnsweredObj;
                i++;
            }
            i = 0;
            
            String Result=SubmitTestWeb.SubmitTest(StudentID,TestID, AnsweredArray);  
            return Result;
        }
    }
}