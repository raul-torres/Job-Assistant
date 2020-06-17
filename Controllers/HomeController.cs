using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobAssistant.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;



namespace JobAssistant.Controllers
{
    public class HomeController : Controller
    {
        private Context dbContext;
        public HomeController(Context context)
        {
            dbContext = context;
        }

    // HANDLING ROOT PAGE ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

    // HANDLING LOGIN/REGISTRATION FORM ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }
    
    // HANDLING LOGIN FORM ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }
    // HANDLING LOGOUT ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }

    // HANDLING HOME PAGE ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("home")]
        public IActionResult Home()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            List<Job> AllJobs = dbContext.Jobs
                .Where(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"))
                .Where(S => S.Status == "Application Sent")
                .ToList();
            User ThisUser = dbContext.Users
                .FirstOrDefault(Us => Us.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            if(ThisUser.HasGoal == true)
            {
                Goal ThisGoal = dbContext.Goals
                    .FirstOrDefault(Use => Use.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
                ViewBag.Goal = ThisGoal;
                if(ThisGoal.SoFar == ThisGoal.Amount)
                {
                    ViewBag.HP = 2;
                    return View(AllJobs);
                }
                ViewBag.HP = 1;
                return View(AllJobs);
            }
            ViewBag.HP = 0;
            return View(AllJobs);
        }
    
    // HANDLING ADDING JOB FORM ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("newjob")]
        public IActionResult NewJob()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
    // HANDLING RESPONDED JOBS ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("respondedjobs")]
        public IActionResult RespondedJobs()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            List<Job> AllJobs = dbContext.Jobs
                .Where(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"))
                .Where(S => S.Status == "Contact Made" || S.Status == "Interview" || S.Status == "Offer Made")
                .ToList();
            User ThisUser = dbContext.Users
                .FirstOrDefault(Us => Us.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            if(ThisUser.HasGoal == true)
            {
                Goal ThisGoal = dbContext.Goals
                    .FirstOrDefault(Use => Use.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
                ViewBag.Goal = ThisGoal;
                if(ThisGoal.SoFar == ThisGoal.Amount)
                {
                    ViewBag.HP = 2;
                    return View(AllJobs);
                }
                ViewBag.HP = 1;
                return View(AllJobs);
            }
            ViewBag.HP = 0;
            return View(AllJobs);
        }
    // HANDLING CLOSED JOBS ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("closedjobs")]
        public IActionResult ClosedJobs()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            List<Job> AllJobs = dbContext.Jobs
                .Where(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"))
                .Where(S => S.Status == "Closed Job")
                .ToList();
            User ThisUser = dbContext.Users
                .FirstOrDefault(Us => Us.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            if(ThisUser.HasGoal == true)
            {
                Goal ThisGoal = dbContext.Goals
                    .FirstOrDefault(Use => Use.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
                ViewBag.Goal = ThisGoal;
                if(ThisGoal.SoFar == ThisGoal.Amount)
                {
                    ViewBag.HP = 2;
                    return View(AllJobs);
                }
                ViewBag.HP = 1;
                return View(AllJobs);
            }
            ViewBag.HP = 0;
            return View(AllJobs);
        }
    // HANDLING THIS JOB EXPANDED ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("thisjob/{IdJob}")]
        public IActionResult ThisJob(int IdJob)
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            Job ThisJob = dbContext.Jobs 
                .FirstOrDefault(J => J.JobId == IdJob);
            if(ThisJob.UserId != (int)HttpContext.Session.GetInt32("UserInSession"))
            {
                return RedirectToAction("Home");
            }
            return View(ThisJob);
        }
    // HANDLING CLOSED THIS JOB EXPANDED ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("closed_thisjob/{IdJob}")]
        public IActionResult Closed_ThisJob(int IdJob)
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            Job ThisJob = dbContext.Jobs 
                .FirstOrDefault(J => J.JobId == IdJob);
            if(ThisJob.UserId != (int)HttpContext.Session.GetInt32("UserInSession"))
            {
                return RedirectToAction("Home");
            }
            return View(ThisJob);
        }
    
    // HANDLING EDIT FORM FOR JOB ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("edit/{IdJob}")]
        public IActionResult Edit(int IdJob)
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            Job EditJob = dbContext.Jobs 
                .FirstOrDefault(J => J.JobId == IdJob);
            if(EditJob.UserId != (int)HttpContext.Session.GetInt32("UserInSession"))
            {
                return RedirectToAction("Home");
            }
            return View(EditJob);
        }
    // HANDLING CLOSED EDIT FORM ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("closed_edit/{IdJob}")]
        public IActionResult Closed_Edit(int IdJob)
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            Job EditJob = dbContext.Jobs 
                .FirstOrDefault(J => J.JobId == IdJob);
            if(EditJob.UserId != (int)HttpContext.Session.GetInt32("UserInSession"))
            {
                return RedirectToAction("Home");
            }
            return View(EditJob);
        }
    // HANDLING ACCOUNT INFO ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("account")]
        public IActionResult Account()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            User ThisUser = dbContext.Users
                .Include(A => A.AppliedJobs)
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            return View(ThisUser);
        }

    // HANDLING NAME UPDATE ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("updatename")]
        public IActionResult UpdateName()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            return View(ThisUser);
        }
    // HANDLING EMAIL UPDATE ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("updateemail")]
        public IActionResult UpdateEmail()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            return View(ThisUser);
        }

    // HANDLING PASSWORD UPDATE ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("updatepassword")]
        public IActionResult UpdatePassword()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            return View(ThisUser);
        }
    // DELETING CLOSED JOB ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("deletejob/{IdJob}")]
        public IActionResult deletejob(int IdJob)
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            Job DELETINGJOB = dbContext.Jobs
                .FirstOrDefault(J => J.JobId == IdJob);
            if(DELETINGJOB.UserId != (int)HttpContext.Session.GetInt32("UserInSession"))
            {
                return RedirectToAction("Home");
            }
            dbContext.Remove(DELETINGJOB);
            dbContext.SaveChanges();
            return RedirectToAction("closedjobs");
        }
    // HANDLING NEW GOAL FORM ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("newgoal")]
        public IActionResult NewGoal()
        {
           int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            return View(); 
        }
    // HANDLING THIS GOAL INFO ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("thisgoal/{IdGoal}")]
        public IActionResult ThisGoal(int IdGoal)
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            Goal ThisGoal = dbContext.Goals
                .FirstOrDefault(G => G.GoalId == IdGoal);
            if(ThisGoal.UserId != (int)HttpContext.Session.GetInt32("UserInSession"))
            {
                return RedirectToAction("Home");
            }
            return View(ThisGoal); 
        }
    // HANDLING UPDATE AMOUNT FORM ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("updateamount/{IdGoal}")]
        public IActionResult UpdateAmount(int IdGoal)
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            Goal ThisGoal = dbContext.Goals
                .FirstOrDefault(G => G.GoalId == IdGoal);
            if(ThisGoal.UserId != (int)HttpContext.Session.GetInt32("UserInSession"))
            {
                return RedirectToAction("Home");
            }
            return View(ThisGoal); 
        }
    // HANDLING GOAL DELETE ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("deletegoal/{IdGoal}")]
        public IActionResult deletegoal(int IdGoal)
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            Goal ThisGoal = dbContext.Goals
                .FirstOrDefault(G => G.GoalId == IdGoal);
            if(ThisGoal.UserId != (int)HttpContext.Session.GetInt32("UserInSession"))
            {
                return RedirectToAction("Home");
            }
            dbContext.Remove(ThisGoal);
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            ThisUser.HasGoal = false;
            dbContext.SaveChanges();
            return RedirectToAction("Home");
        }
    // HANDLING COMPLETED GOAL ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("thiscompgoal/{IdGoal}")]
        public IActionResult ThisCompGoal(int IdGoal)
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            Goal ThisGoal = dbContext.Goals
                .FirstOrDefault(G => G.GoalId == IdGoal);
            if(ThisGoal.UserId != (int)HttpContext.Session.GetInt32("UserInSession"))
            {
                return RedirectToAction("Home");
            }
            return View(ThisGoal); 
        }
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~POST~~~~~REQUESTS~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~POST~~~~~REQUESTS~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~POST~~~~~REQUESTS~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


     // HANDLING REGISTRATION ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpPost("register")]
        public IActionResult register(User NewUser)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(u => u.Email == NewUser.Email))
                {
                    ModelState.AddModelError("Email", "Email is already in use.");
                    return View("Register");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                NewUser.Password = Hasher.HashPassword(NewUser, NewUser.Password);

                dbContext.Add(NewUser);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("UserInSession", NewUser.UserId);
                return RedirectToAction("Home");
            }
            return View("Register");
        }

    // HANDLING USER LOGIN ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            [HttpPost("access")]
            public IActionResult access(LoginUser AccessUser)
            {
                if(ModelState.IsValid)
                {
                    var UserInDb = dbContext.Users.FirstOrDefault(u => u.Email == AccessUser.LEmail);
                    if(UserInDb == null)
                    {
                        ModelState.AddModelError("LEmail", "Invalid email or password");
                        return View("Login");
                    }
                    var Hasher = new PasswordHasher<LoginUser>();
                    var Result = Hasher.VerifyHashedPassword(AccessUser, UserInDb.Password, AccessUser.LPassword);
                    if(Result == 0)
                    {
                        ModelState.AddModelError("LEmail", "Invalid email or password");
                        return View("Login");
                    }
                    HttpContext.Session.SetInt32("UserInSession", UserInDb.UserId);
                    return RedirectToAction("Home");
                }
                return View("Login");
            }
    // HANDLING ADDING NEW JOB ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            [HttpPost("jobadder")]
            public IActionResult jobadder(Job NewJob)
            {
                if(ModelState.IsValid)
                {
                    if(NewJob.ADate < DateTime.Today)
                    {
                        ModelState.AddModelError("AppliedDate", "Activity cannot be in the past.");
                        return View("newjob");
                    }
                    NewJob.UserId = (int)HttpContext.Session.GetInt32("UserInSession");
                    dbContext.Add(NewJob);
                    User ThisUser = dbContext.Users
                        .FirstOrDefault(X => X.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
                    if(ThisUser.HasGoal == true)
                    {
                        Goal ThisGoal = dbContext.Goals
                            .FirstOrDefault(G => G.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
                        ThisGoal.SoFar++;
                        if(ThisGoal.Amount == ThisGoal.SoFar)
                        {
                            ThisGoal.UpdatedAt = DateTime.Now;
                        }
                    }
                    dbContext.SaveChanges();
                    return RedirectToAction("Home");
                }
                return View("newjob");
            }

    // SAVING STATUS FROM HOME CHANGED ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            [HttpPost("updatestatushome/{IdJob}")]
            public IActionResult updatestatushome(Job StatusChange, int IdJob)
            {
                Job CurrentJob = dbContext.Jobs
                    .FirstOrDefault(J => J.JobId == IdJob);
                CurrentJob.Status = StatusChange.Status;
                CurrentJob.UpdatedAt = DateTime.Now;
                CurrentJob.RDate = DateTime.Now;
                dbContext.SaveChanges();
                return RedirectToAction("Home");
            }
    // SAVING STATUS FROM RESPONDED CHANGED ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            [HttpPost("updatestatusresponded/{IdJob}")]
            public IActionResult updatestatusresponded(Job StatusChange, int IdJob)
            {
                Job CurrentJob = dbContext.Jobs
                    .FirstOrDefault(J => J.JobId == IdJob);
                CurrentJob.Status = StatusChange.Status;
                CurrentJob.UpdatedAt = DateTime.Now;
                CurrentJob.RDate = DateTime.Now;
                dbContext.SaveChanges();
                return RedirectToAction("respondedjobs");
            }
    // CHANGING STATUS FROM CLOSED ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            [HttpPost("updatestatusfromclosed/{IdJob}")]
            public IActionResult updatestatusfromclosed(Job StatusChange, int IdJob)
            {
                Job CurrentJob = dbContext.Jobs
                    .FirstOrDefault(J => J.JobId == IdJob);
                CurrentJob.Status = StatusChange.Status;
                CurrentJob.ClosedNotes = "";
                CurrentJob.UpdatedAt = DateTime.Now;
                CurrentJob.RDate = DateTime.Now;
                dbContext.SaveChanges();
                return RedirectToAction("respondedjobs");
            }
    // SAVING EDITED CHANGES ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            [HttpPost("savechanges/{IdJob}")]
            public IActionResult savechanges(Job UpdatedJob, int IdJob)
            {
                Job CurrentJob = dbContext.Jobs
                    .FirstOrDefault(J => J.JobId == IdJob);
                if(ModelState.IsValid)
                {
                    CurrentJob.Position = UpdatedJob.Position;
                    CurrentJob.Company = UpdatedJob.Company;
                    CurrentJob.Website = UpdatedJob.Website;
                    CurrentJob.ADate = UpdatedJob.ADate;
                    CurrentJob.Status = UpdatedJob.Status;
                    CurrentJob.UpdatedAt = DateTime.Now;
                    dbContext.SaveChanges();
                    return Redirect("/thisjob/" + IdJob);
                }
                return View("edit", CurrentJob);
            }
    // SAVING CLOSED JOB CANGES ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            [HttpPost("saveclosedchanges/{IdJob}")]
            public IActionResult saveclosedchanges(Job UpdatedJob, int IdJob)
            {
                Job CurrentJob = dbContext.Jobs
                    .FirstOrDefault(J => J.JobId == IdJob);
                if(ModelState.IsValid)
                {
                    CurrentJob.Position = UpdatedJob.Position;
                    CurrentJob.Company = UpdatedJob.Company;
                    CurrentJob.Website = UpdatedJob.Website;
                    CurrentJob.ADate = UpdatedJob.ADate;
                    CurrentJob.Status = UpdatedJob.Status;
                    CurrentJob.ClosedNotes = UpdatedJob.ClosedNotes;
                    CurrentJob.UpdatedAt = DateTime.Now;
                    dbContext.SaveChanges();
                    return RedirectToAction("closedjobs");
                }
                return View("Closed_Edit", CurrentJob);
            }

    // SAVING UPDATE NAME CHANGS ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            [HttpPost("saveupdatename")]
            public IActionResult saveupdatename(User UpdatedUser)
            {
                ModelState.Remove("Email");
                ModelState.Remove("Password");
                if(ModelState.IsValid)
                {
                    User CurrentUser = dbContext.Users
                        .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
                    CurrentUser.Name = UpdatedUser.Name;
                    dbContext.SaveChanges();
                    return RedirectToAction("Account");
                }
                return View("updatename");
            }
    // SAVING UPDATE EMAIL CHANGS ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            [HttpPost("saveupdateemail")]
            public IActionResult saveupdateemail(User UpdatedUser)
            {
                ModelState.Remove("Name");
                ModelState.Remove("Password");
                if(ModelState.IsValid)
                {
                    User CurrentUser = dbContext.Users
                        .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
                    CurrentUser.Email = UpdatedUser.Email;
                    dbContext.SaveChanges();
                    return RedirectToAction("Account");
                }
                return View("updateemail");
            }
    // SAVING UPDATE PASSWORD CHANGS ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            [HttpPost("saveupdatepassword")]
            public IActionResult saveupdatepassword(User UpdatedUser)
            {
                if(UpdatedUser.OldPassword == null)
                {
                    ModelState.AddModelError("OldPassword", "Old password is required");
                    return View("updatepassword");
                }
                var UserInDb = dbContext.Users
                    .FirstOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
                var Hasher = new PasswordHasher<User>();
                var Result = Hasher.VerifyHashedPassword(UpdatedUser, UserInDb.Password, UpdatedUser.OldPassword);
                if(Result == 0)
                {
                    ModelState.AddModelError("OldPassword", "Invalid password");
                    return View("updatepassword");
                }
                ModelState.Remove("Name");
                ModelState.Remove("Email");
                if(ModelState.IsValid)
                {
                    UpdatedUser.Password = Hasher.HashPassword(UpdatedUser, UpdatedUser.Password);
                    UserInDb.Password = UpdatedUser.Password;
                    dbContext.SaveChanges();
                    return RedirectToAction("Account");
                }
                return View("updatepassword");
            }
    // HANDLING JOB CLOSED (HOME) ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            [HttpPost("closeh/{IdJob}")]
            public IActionResult closeh(int IdJob)
            {
                Job ThisJob = dbContext.Jobs
                    .FirstOrDefault(J => J.JobId == IdJob);
                ThisJob.ClosedNotes = HttpContext.Request.Form["CNotes"];
                ThisJob.Status = "Closed Job";
                ThisJob.UpdatedAt = DateTime.Now;
                ThisJob.RDate = DateTime.Now;
                dbContext.SaveChanges();
                return RedirectToAction("Home");
            }
    // HANDLING JOB CLOSED (RESPONDED) ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            [HttpPost("closer/{IdJob}")]
            public IActionResult closer(int IdJob)
            {
                Job ThisJob = dbContext.Jobs
                    .FirstOrDefault(J => J.JobId == IdJob);
                ThisJob.ClosedNotes = HttpContext.Request.Form["CNotes"];
                ThisJob.Status = "Closed Job";
                ThisJob.UpdatedAt = DateTime.Now;
                ThisJob.RDate = DateTime.Now;
                dbContext.SaveChanges();
                return RedirectToAction("respondedjobs");
            }
    // HANDLING NEW GOAL ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            [HttpPost("creategoal")]
            public IActionResult creategoal(Goal NewGoal)
            {
                User ThisUser = dbContext.Users
                    .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
                if(ThisUser.HasGoal == true)
                {
                    ModelState.AddModelError("Amount", "You already have a set goal");
                    return View("NewGoal");
                }
                if(ModelState.IsValid)
                {
                    ThisUser.HasGoal = true;
                    NewGoal.UserId = (int)HttpContext.Session.GetInt32("UserInSession");
                    dbContext.Add(NewGoal);
                    dbContext.SaveChanges();
                    return RedirectToAction("Home");
                }
                return View("NewGoal");
            }
    // HANDLING GOAL AMOUNT UPDATES ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            [HttpPost("saveupdateamount/{IdGoal}")]
            public IActionResult saveupdateamount(int IdGoal, Goal UpdatedGoal)
            {
                Goal ThisGoal = dbContext.Goals
                    .FirstOrDefault(G => G.GoalId == IdGoal);
                if(ModelState.IsValid)
                {
                    ThisGoal.Amount = UpdatedGoal.Amount;
                    dbContext.SaveChanges();
                    return Redirect("/ThisGoal/" + IdGoal);
                }
                return View("UpdateAmount", ThisGoal);
            }
    }   
}
