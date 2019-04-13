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
        public IActionResult Login(LogUser LogUser)
        {            
            if(ModelState.IsValid)
            {
                User UserInfo = dbContext.Users.SingleOrDefault(u => u.Email == LogUser.LoginEmail);
                if(UserInfo == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid User");
                    return View("Index");
                }

                PasswordHasher<LogUser> Hasher = new PasswordHasher<LogUser>();
                var result = Hasher.VerifyHashedPassword(LogUser, UserInfo.Password, LogUser.LoginPassword);
                
                if(!result.ToString().Equals("Success"))
                {
                    ModelState.AddModelError("LoginEmail", "Invalid User");
                    return View("Index");
                }
                
                HttpContext.Session.SetInt32("UserID", UserInfo.UserId);
                
                return RedirectToAction("Success", "User");
            } else {
                return View("Index");
            }
        }

        [HttpPost("registration")]
        public IActionResult Registration(User User)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(u => u.Email == User.Email)){
                    ModelState.AddModelError("Email", "This Email already exist");
                    return View("Index");
                }

                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                User.Password = Hasher.HashPassword(User, User.Password);

                dbContext.Add(User);
                dbContext.SaveChanges();

                User UserInfo = dbContext.Users.SingleOrDefault(u => u.Email == User.Email);
                HttpContext.Session.SetInt32("UserID", UserInfo.UserId);
                
                return RedirectToAction("Success");
            } else {
                return View("Index");
            }
        }

        [HttpGet("success")]
        public IActionResult Success()
        {
            int? UserID = HttpContext.Session.GetInt32("UserID");
            
            if(UserID.ToString().Length == 0)
            {
                return RedirectToAction("Index");
            }

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
