using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotnetFlix.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DotnetFlix.Controllers
{
    public class UserController : Controller
    {
        private DotnetFlixContext dbContext;

        public UserController(DotnetFlixContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login(LogUser form)
        {            
            if(ModelState.IsValid)
            {
                User UserInfo = dbContext.Users.SingleOrDefault(u => u.Email == form.LoginEmail);
                if(UserInfo is null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid User");
                    return View("Index");
                }

                PasswordHasher<LogUser> Hasher = new PasswordHasher<LogUser>();
                var result = Hasher.VerifyHashedPassword(form, UserInfo.Password, form.LoginPassword);
                
                if(!result.ToString().Equals("Success"))
                {
                    ModelState.AddModelError("LoginEmail", "Invalid User");
                    return View("Index");
                }
                
                HttpContext.Session.SetInt32("UserID", UserInfo.UserId);
                
                return RedirectToAction("Success", "User");
            } 
            return View("Index");
        }

        [HttpPost("registration")]
        public IActionResult Registration(RegUser form)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(u => u.Email == form.RegEmail))
                {
                    ModelState.AddModelError("Email", "This Email already exist");
                    return View("Index");
                }

                PasswordHasher<RegUser> Hasher = new PasswordHasher<RegUser>();
                form.RegPassword = Hasher.HashPassword(form, form.RegPassword);

                User newUser = new User(form);
                dbContext.Add(newUser);
                dbContext.SaveChanges();

                User UserInfo = dbContext.Users.SingleOrDefault(u => u.Email == form.RegEmail);
                HttpContext.Session.SetInt32("UserID", UserInfo.UserId);
                
                return RedirectToAction("Success");
            }
            return View("Index");
        }

        [HttpGet("success")]
        public IActionResult Success()
        {
            int? UserID = HttpContext.Session.GetInt32("UserID");
            if(UserID is null)
                return RedirectToAction("Index");

            User UserInfo = dbContext.Users.SingleOrDefault(u => u.UserId == UserID);

            return RedirectToAction("Dashboard", "Rental");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserID");
            return View("Index");
        }
    }
}
