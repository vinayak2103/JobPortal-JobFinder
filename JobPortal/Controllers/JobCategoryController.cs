﻿using System;
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
    public class JobCategoryController : Controller
    {
        private JobPortalDBEntities db = new JobPortalDBEntities();

        // GET: JobCategory
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");

            }
            return View(db.JobCategoryTables.ToList());
        }

        

        // GET: JobCategory/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");

            }
            return View(new JobCategoryTable());
        }

        // POST: JobCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobCategoryTable jobCategoryTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");

            }
            if (ModelState.IsValid)
            {
                db.JobCategoryTables.Add(jobCategoryTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobCategoryTable);
        }

        // GET: JobCategory/Edit/5
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
            JobCategoryTable jobCategoryTable = db.JobCategoryTables.Find(id);
            if (jobCategoryTable == null)
            {
                return HttpNotFound();
            }
            return View(jobCategoryTable);
        }

        // POST: JobCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JobCategoryTable jobCategoryTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");

            }
            if (ModelState.IsValid)
            {
                db.Entry(jobCategoryTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobCategoryTable);
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
