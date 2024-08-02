using MyEF.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal.Models
{
    public class FilterJobModel
    {

        public FilterJobModel()
        {
            Result = new List<PostJobTable>();
        }

        public int JobCategoryID { get; set; }
        public int JobNatureID { get; set; }
        public int NoDays { get; set; }

        public List<PostJobTable> Result { get; set; }
    }
}