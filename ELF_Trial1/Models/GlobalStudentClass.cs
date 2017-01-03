using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELF_Trial1.Models
{
    public static class GlobalStudentClass
    {

        static string _boardName;
        static string _className;
        static string _cityName;
        static string _districtName;
        static string _emailAddress;
        static string _name;
        static string _institutionName;
        static string _phoneNumber;
        static string _stateName;
        static int _studentId;
        static string _studentName;
        static string _UserType;
        public static string BoardName
        {
            get { return _boardName; }
            set { _boardName = value; }
        }

        public static string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }


        public static string CityName
        {
            get { return _cityName; }
            set { _cityName = value; }
        }
        public static string DistrictName
        {
            get { return _districtName; }
            set { _districtName = value; }
        }

        public static string EmailAddress
        {
            get { return _emailAddress; }
            set { _emailAddress = value; }

        }
        public static string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public static string InstitutionName
        {
            get { return _institutionName; }
            set { _institutionName = value; }

        }

        public static string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }

        }

        public static string StateName
        {
            get { return _stateName; }
            set { _stateName = value; }
        }
        public static int StudentId
        {
            get { return _studentId; }
            set { _studentId = value; }
        }

        public static string StudentName
        {
            get { return _studentName; }
            set { _studentName = value; }
        }
        public static string UserType
        {
            get { return _UserType; }
            set { _UserType = value; }
        }
    }
    public class GlobalTestClass
    {
        public Int32 StudentID { get; set; }
        public Int32 TestID { get; set; }

        public Int32 SubjectID { get; set; }

    }
    
    public class SubjectDetails {
        public Int32 SubjectID { get; set; }
        public String SubjectName { get; set; }
    }

}