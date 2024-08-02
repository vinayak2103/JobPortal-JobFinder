using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyEF.EF;

namespace JobPortal.Controllers
{
    public class JobNatureController : Controller
    {
        private JobPortalDBEntities db = new JobPortalDBEntities();

        // GET: JobNature
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");

            }
            return View(db.JobNatureTables.ToList());
        }

        // GET: JobNature/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");

            }
            return View(new JobNatureTable());
        }

        // POST: JobNature/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobNatureID,JobNature")] JobNatureTable jobNatureTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");

            }
            if (ModelState.IsValid)
            {
                db.JobNatureTables.Add(jobNatureTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobNatureTable);
        }

        // GET: JobNature/Edit/5
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");

            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobNatureTable jobNatureTable = db.JobNatureTables.Find(id);
            if (jobNatureTable == null)
            {
                return HttpNotFound();
            }
            return View(jobNatureTable);
        }

        // POST: JobNature/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobNatureID,JobNature")] JobNatureTable jobNatureTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");

            }
            if (ModelState.IsValid)
            {
                db.Entry(jobNatureTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobNatureTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
