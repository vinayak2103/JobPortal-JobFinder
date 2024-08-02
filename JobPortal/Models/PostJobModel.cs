﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobPortal.Models
{
    public class PostJobModel
    {
        public int PostJobID { get; set; }
        public int UserID { get; set; }
        public int CompanyID { get; set; }
        public int JobCategoryID { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(500, ErrorMessage = "Do not enter more than 500 characters")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(2000, ErrorMessage = "Do not enter more than 2000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Required")]
        public int MinSalary { get; set; }

        [Required(ErrorMessage = "Required")]
        public int MaxSalary { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Required")]
        public int Vacany { get; set; }

        public int JobNatureID { get; set; }
        public System.DateTime PostDate { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime ApplicationLastDate { get; set; } = DateTime.Now.AddDays(15);

        public int JobStatusID { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Url)]
        public string WebURL { get; set; }
    }
}