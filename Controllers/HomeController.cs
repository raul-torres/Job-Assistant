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

    // HANDLING LOGIN/REGISTRATION FORM ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
    
    // HANDLING LOGIN FORM ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
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
            return View();
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
                    return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                NewUser.Password = Hasher.HashPassword(NewUser, NewUser.Password);

                dbContext.Add(NewUser);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("UserInSession", NewUser.UserId);
                return RedirectToAction("Home");
            }
            return View("Index");
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
                    if(NewJob.AppliedDate < DateTime.Today)
                    {
                        ModelState.AddModelError("AppliedDate", "Activity cannot be in the past.");
                        return View("newjob");
                    }
                    NewJob.UserId = (int)HttpContext.Session.GetInt32("UserInSession");
                    dbContext.Add(NewJob);
                    dbContext.SaveChanges();
                    return RedirectToAction("Home");
                }
                return View("newjob");
            }
    }   
}
