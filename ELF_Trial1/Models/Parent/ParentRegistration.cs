using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ELF_Trial1.Models.Parent
{
    public class ParentRegistration 
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String EmailAddress { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public String Password { get; set; }
        public int StateId { get; set; }
        public int ClassId { get; set; }
        public int BoardId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int InstitutionId { get; set; }
        public String PhoneNumber { get; set; }
    }

}