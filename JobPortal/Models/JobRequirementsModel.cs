﻿using MyEF.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobPortal.Models
{
    public class JobRequirementsModel
    {
        public JobRequirementsModel()
        {
            Details = new List<JobRequirementDetailTable>();
        }

        [Required(ErrorMessage ="Required")]
        public string JobRequirementDetail { get; set; }

        [Required(ErrorMessage = "Required")]
        public int JobRequirementID { get; set; }


        public int PostJobID { get; set; }

        public List<JobRequirementDetailTable> Details { get; set; }
    }
}