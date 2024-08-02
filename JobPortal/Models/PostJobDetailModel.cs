using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal.Models
{
    public class PostJobDetailModel
    {

        public PostJobDetailModel()
        {
            Requirement = new List<JobRequirementModel>();
        }

        public int PostJobID { get; set; }
        public string Company { get; set; }
        public string JobCategory { get; set; }
        public string JobNature { get; set; }
        public string JobTitle { get; set; }
        public int MinSalary { get; set; }
        public int MaxSalary { get; set; }
        public string Location { get; set; }
        public int Vacany { get; set; }
        public System.DateTime PostDate { get; set; }
        public System.DateTime ApplicationLastDate { get; set; }
        public string Description { get; set; }
        public string WebURL { get; set; }

        public List<JobRequirementModel> Requirement { get; set; }

    }
}