using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal.Models
{
    public class JobRequirementModel
    {
        public JobRequirementModel()
        {
            Details = new List<JobRequirementDetailModel>();
        }

        public int JobRequirementID { get; set; }

        public string JobRequirementTitle { get; set; }

        public List<JobRequirementDetailModel> Details { get; set; }
    }
}