using JobPortal.Models;
using MyEF.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobPortal.Controllers
{
    public class UserController : Controller
    {
        private JobPortalDBEntities db = new JobPortalDBEntities();
        // GET: User
        public ActionResult NewUser()
        {
            return View(new UserModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewUser(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var checkuser = db.UserTables.Where(u => u.Email == userModel.Email).FirstOrDefault();
                if(checkuser != null)
                {
                    ModelState.AddModelError("Email", "Email is already registered");
                    return View(userModel);
                }
                checkuser = db.UserTables.Where(u => u.UserName == userModel.UserName).FirstOrDefault();
                if (checkuser != null)
                {
                    ModelState.AddModelError("UserName", "Username is already registered");
                    return View(userModel);
                }

                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        var user = new UserTable();
                        user.UserName = userModel.UserName;
                        user.Password = userModel.Password;
                        user.ContactNo = userModel.ContactNo;
                        user.Email = userModel.Email;
                        user.UserTypeID = userModel.AreYouProvider == true ? 2 : 3;
                        db.UserTables.Add(user);
                        db.SaveChanges();

                        if (userModel.AreYouProvider)
                        {
                            var company = new CompanyTable();
                            company.UserID = user.UserID;
                            if (string.IsNullOrEmpty(userModel.Company.Email))
                            {
                                trans.Rollback();
                                ModelState.AddModelError("Company.Email", "Required");
                                return View(userModel);
                            }
                            if (string.IsNullOrEmpty(userModel.Company.CompanyName))
                            {
                                trans.Rollback();
                                ModelState.AddModelError("Company.CompanyName", "Required");
                                return View(userModel);
                            }
                            if (string.IsNullOrEmpty(userModel.Company.PhoneNo))
                            {
                                trans.Rollback();
                                ModelState.AddModelError("Company.PhoneNo", "Required");
                                return View(userModel);
                            }
                            if (string.IsNullOrEmpty(userModel.Company.Description))
                            {
                                trans.Rollback();
                                ModelState.AddModelError("Company.Description", "Required");
                                return View(userModel);
                            }

                            company.Email = userModel.Company.Email;
                            company.CompanyName = userModel.Company.CompanyName;
                            company.ContactNo = userModel.ContactNo;
                            company.PhoneNo = userModel.Company.PhoneNo;
                            company.Logo = "~/Content/assets/img/icon/job-list1.png";
                            company.Description = userModel.Company.Description;
                            db.CompanyTables.Add(company);
                            db.SaveChanges();
                        }
                        trans.Commit();
                        return RedirectToAction("Login");
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "Please Provide Valid Details");
                        trans.Rollback();
                    }
                }

            }
            return View(userModel);
        }

        public ActionResult Login()
        {
            return View(new UserLoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginModel userLogin)
        {
            if (ModelState.IsValid)
            {
                var user = db.UserTables.Where(u => u.UserName == userLogin.UserName && u.Password == userLogin.Password).FirstOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Username or Password is incorrect");
                    return View(userLogin);
                }
                Session["UserID"] = user.UserID;
                Session["UserName"] = user.UserName;
                Session["UserTypeID"] = user.UserTypeID;

                if(user.UserTypeID == 2)
                {
                    Session["CompanyID"] = user.CompanyTables.FirstOrDefault().CompanyID;
                }

                return RedirectToAction("Index", "Home");
            }
            return View(userLogin);
        }

        public ActionResult LogOut()
        {
            Session["UserID"] = string.Empty;
            Session["UserName"] = string.Empty;
            Session["CompanyID"] = string.Empty;
            Session["UserTypeID"] = string.Empty;
            return RedirectToAction("Index","Home");
        }

        public ActionResult AllUsers()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");

            }
            var users = db.UserTables.ToList();
            return View(users);
        }
    }
}