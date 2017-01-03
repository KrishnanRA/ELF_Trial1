using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ELF_Trial1.Models.Student
{
    public class Registration
    {

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public String EmailAddress { get; set; }
        public String Password { get; set; }
        public String ReenterPassword { get; set; }
        public int StateId { get; set; }
        public int ClassId { get; set; }
        public int BoardId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int InstitutionId { get; set; }


    }



}