using JobPortal.Models;
using MyEF.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobPortal.Controllers
{
    public class JobController : Controller
    {
        private JobPortalDBEntities db = new JobPortalDBEntities();
        // GET: Job
        public ActionResult PostJob()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            var job = new PostJobModel();
            ViewBag.JobCategoryID = new SelectList(
                                    db.JobCategoryTables.ToList(),
                                    "JobCategoryID",
                                    "JobCategory",
                                    "0"
                                );

            ViewBag.JobNatureID = new SelectList(
                                db.JobNatureTables.ToList(),
                                "JobNatureID",
                                "JobNature",
                                "0"
                            );

            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostJob(PostJobModel post)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }

            int userid = 0;
            int companyid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);
            post.UserID = userid;
            post.CompanyID = companyid;

            if (ModelState.IsValid)
            {
                var job = new PostJobTable();
                job.UserID = post.UserID;
                job.CompanyID = post.CompanyID;
                job.JobCategoryID = post.JobCategoryID;
                job.JobStatusID = 1;
                job.JobNatureID = post.JobNatureID;
                job.JobTitle = post.JobTitle;
                job.MinSalary = post.MinSalary;
                job.MaxSalary = post.MaxSalary;
                job.Location = post.Location;
                job.Vacancy = post.Vacany;
                job.PostDate = DateTime.Now;
                job.ApplicationLastDate = post.ApplicationLastDate;
                job.LastDate = post.ApplicationLastDate;
                job.Description = post.Description;
                job.WebURL = post.WebURL;

                db.PostJobTables.Add(job);
                db.SaveChanges();
                return RedirectToAction("CompanyJobList", "Job");
            }

            ViewBag.JobCategoryID = new SelectList(
                                    db.JobCategoryTables.ToList(),
                                    "JobCategoryID",
                                    "JobCategory",
                                    "0"
                                );

            ViewBag.JobNatureID = new SelectList(
                                db.JobNatureTables.ToList(),
                                "JobNatureID",
                                "JobNature",
                                "0"
                            );

            return View(post);
        }


        public ActionResult CompanyJobList()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }

            int userid = 0;
            int companyid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);

            var allpost = db.PostJobTables.Where(c=>c.CompanyID == companyid && c.UserID == userid).ToList();

            return View(allpost);
        }

        public ActionResult AllCompanyJobs()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }

            int userid = 0;
            int companyid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);

            var allpost = db.PostJobTables.ToList();
            if(allpost.Count() > 0)
            {
                allpost = allpost.OrderByDescending(o => o.PostJobID).ToList();
            }

            return View(allpost);
        }

        public ActionResult AddJobDetails(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }

            var details = db.JobRequirementDetailTables.Where(j=>j.PostJobID == id).ToList();
            if (details.Count() > 0)
            {
                details = details.OrderBy(r => r.JobRequirementID).ToList();
            }
            var req = new JobRequirementsModel();
            req.Details = details;
            req.PostJobID = (int)id;

            ViewBag.JobRequirementID = new SelectList(
                db.JobRequirementTables.ToList(),"JobRequirementID", "JobRequirementTitle","0");

            return View(req);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddJobDetails(JobRequirementsModel jobReq)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }

            try
            {
                var req = new JobRequirementDetailTable();
                req.JobRequirementID = jobReq.JobRequirementID;
                req.JobRequirementDetail = jobReq.JobRequirementDetail;
                req.PostJobID = jobReq.PostJobID;
                db.JobRequirementDetailTables.Add(req);
                db.SaveChanges();
                return RedirectToAction("AddJobDetails", new { id = req.PostJobID });
            }
            catch (Exception ex)
            {
                var details = db.JobRequirementDetailTables.Where(j => j.PostJobID == jobReq.PostJobID).ToList();
                if (details.Count() > 0)
                {
                    details = details.OrderBy(r => r.JobRequirementID).ToList();
                }   
                jobReq.Details = details;
                ModelState.AddModelError("JobRequirementID","Required");
            }

            ViewBag.JobRequirementID = new SelectList(
                db.JobRequirementTables.ToList(), "JobRequirementID", "JobRequirementTitle", jobReq.JobRequirementID);

            return View(jobReq);
        }

        public ActionResult DeleteRequirements(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }

            var jobpostid = db.JobRequirementDetailTables.Find(id).PostJobID;

            var req = db.JobRequirementDetailTables.Find(id);
            db.Entry(req).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

            return RedirectToAction("AddJobDetails", new { id = jobpostid });
        }

        public ActionResult DeletePostJob(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }

            var jobpost = db.PostJobTables.Find(id);
            db.Entry(jobpost).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("CompanyJobList");
        }

        public ActionResult ApproveJob(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }

            var jobpost = db.PostJobTables.Find(id);
            jobpost.JobStatusID = 2;
            db.Entry(jobpost).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("AllCompanyJobs","Job");
        }

        public ActionResult CancelJob(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }

            var jobpost = db.PostJobTables.Find(id);
            jobpost.JobStatusID= 3;
            db.Entry(jobpost).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("AllCompanyJobs","Job");
        }

        public ActionResult JobDetail(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }

            var getpostjob = db.PostJobTables.Find(id);
            var postjob = new PostJobDetailModel();
            postjob.PostJobID = getpostjob.PostJobID;
            postjob.Company = getpostjob.CompanyTable.CompanyName;
            postjob.JobCategory = getpostjob.JobCategoryTable.JobCategory;
            postjob.JobNature = getpostjob.JobNatureTable.JobNature;
            postjob.JobTitle = getpostjob.JobTitle;
            postjob.MinSalary = getpostjob.MinSalary;
            postjob.MaxSalary = getpostjob.MaxSalary;
            postjob.Location = getpostjob.Location;
            postjob.Vacany = getpostjob.Vacancy;
            postjob.PostDate = getpostjob.PostDate;
            postjob.ApplicationLastDate = getpostjob.ApplicationLastDate;
            postjob.Description = getpostjob.Description;
            postjob.WebURL = getpostjob.WebURL;

            getpostjob.JobRequirementDetailTables = getpostjob.JobRequirementDetailTables.OrderBy(d => d.JobRequirementID).ToList();

            int jobreqid = 0;
            var jobreq = new JobRequirementModel();
            foreach(var detail in getpostjob.JobRequirementDetailTables)
            {
                var jobreqdetail = new JobRequirementDetailModel();
                if (jobreqid == 0)
                {
                    jobreq.JobRequirementID = detail.JobRequirementID;
                    jobreq.JobRequirementTitle = detail.JobRequirementTable.JobRequirementTitle;
                    jobreqdetail.JobRequirementID = detail.JobRequirementID;
                    jobreqdetail.JobRequirementDetails = detail.JobRequirementDetail;
                    jobreq.Details.Add(jobreqdetail);
                    jobreqid = detail.JobRequirementID;
                }
                else if(jobreqid == detail.JobRequirementID)
                {
                    jobreqdetail.JobRequirementID = detail.JobRequirementID;
                    jobreqdetail.JobRequirementDetails = detail.JobRequirementDetail;
                    jobreq.Details.Add(jobreqdetail);
                    jobreqid = detail.JobRequirementID;
                }
                else if(jobreqid != detail.JobRequirementID)
                {
                    postjob.Requirement.Add(jobreq);

                    jobreq = new JobRequirementModel();

                    jobreq.JobRequirementID = detail.JobRequirementID;
                    jobreq.JobRequirementTitle = detail.JobRequirementTable.JobRequirementTitle;
                    jobreqdetail.JobRequirementID = detail.JobRequirementID;
                    jobreqdetail.JobRequirementDetails = detail.JobRequirementDetail;
                    jobreq.Details.Add(jobreqdetail);
                    jobreqid = detail.JobRequirementID;
                }
            }
            postjob.Requirement.Add(jobreq);
            return View(postjob);
        }

        public ActionResult FilterJob()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }

            var obj = new FilterJobModel();

            var date = DateTime.Now.Date;
            var result = db.PostJobTables.Where(r => r.ApplicationLastDate >= date && r.JobStatusID==2).ToList();
            obj.Result = result;

            ViewBag.JobCategoryID = new SelectList(
                                    db.JobCategoryTables.ToList(),
                                    "JobCategoryID",
                                    "JobCategory",
                                    "0"
                                );

            ViewBag.JobNatureID = new SelectList(
                                db.JobNatureTables.ToList(),
                                "JobNatureID",
                                "JobNature",
                                "0"
                            );


            return View(obj);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FilterJob(FilterJobModel filter)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }

            var date = DateTime.Now.Date;
            var result = db.PostJobTables.Where(r => r.ApplicationLastDate >= date && (r.JobStatusID == 2) && (r.JobCategoryID == filter.JobCategoryID && r.JobNatureID == filter.JobNatureID)).ToList();
            filter.Result = result;

            ViewBag.JobCategoryID = new SelectList(
                                    db.JobCategoryTables.ToList(),
                                    "JobCategoryID",
                                    "JobCategory",
                                    filter.JobCategoryID
                                );

            ViewBag.JobNatureID = new SelectList(
                                db.JobNatureTables.ToList(),
                                "JobNatureID",
                                "JobNature",
                                filter.JobNatureID
                            );


            return View(filter);
        }

    }
}