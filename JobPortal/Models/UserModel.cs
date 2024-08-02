using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobPortal.Models
{
    public class UserModel
    {

        public UserModel()
        {
            Company = new CompanyModel();
        }

        public int UserID { get; set; }
        public int UserTypeID { get; set; }

        [Required(ErrorMessage="Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        public string ContactNo { get; set; }

        public bool AreYouProvider { get; set; }

        public CompanyModel Company { get; set; }
    }
}