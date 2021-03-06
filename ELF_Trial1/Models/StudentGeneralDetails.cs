﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELF_Trial1.Models
{
    public class StudentGeneralDetails
    {
        public string BoardName { get; set; }
        public string CityName { get; set; }
        public string ClassName { get; set; }
        public string DistrictName { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string StateName { get; set; }
        public int StudentId { get; set; }
        public string InstitutionName { get; set; }
        public Int32 GroupId { get; set; }
        public List<SubjectDetails> StudentSubject { get; set; }
       
    }

    public class SubjectDetails
    {
        public Int32 SubjectID { get; set; }
        public String SubjectName { get; set; }
    }

    public class ParentDetails
    {
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string StateName { get; set; }
        public int ParentId { get; set; }
    }


}