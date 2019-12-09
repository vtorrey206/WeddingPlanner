using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {

        private MyContext dbContext;
     
        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost("registering")] //Process of Registration this will redirect to success
        public IActionResult Registering(User newUser)
        {
            
            dbContext.Add(newUser);
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
            dbContext.SaveChanges();
            HttpContext.Session.SetInt32("UserId", newUser.UserId);
            return RedirectToAction("Dashboard");
        }


        [HttpPost("logging")]
        public IActionResult Logging(LoginUser userSubmission)
        {
            if(ModelState.IsValid)
            {
                // If inital ModelState is valid, query for a user with provided email
                var userInDb = dbContext.User.FirstOrDefault(u => u.Email == userSubmission.Email);
                // If no user exists with provided email
                if(userInDb == null)
                {
                    // Add an error to ModelState and return to View!
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Index");
                }
                
                // Initialize hasher object
                var hasher = new PasswordHasher<LoginUser>();
                
                // verify provided password against hash stored in db
                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.Password);
                
                // result can be compared to 0 for failure
                if(result == 0)
                {
                    return View("Index");
                }
                HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                return RedirectToAction("Dashboard");
            }
            return View("Index");

        }
        [HttpGet("logout")]
        public IActionResult LogOut()
        {
            return View("Index");
        }
        
        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            int? LoggedIn = HttpContext.Session.GetInt32("UserId");
            if(LoggedIn == 0 || LoggedIn == null)
            {
            return RedirectToAction("Index");
            }
            User ThisUser = dbContext.User.Include(w=> w.WeddingsCreated)
            .FirstOrDefault(u => u.UserId == (int)LoggedIn);
            if(ThisUser.UserId != LoggedIn)
            {
            return RedirectToAction("Index");
            }
            ViewBag.loggedIn = (int)LoggedIn;
            ViewBag.creator = ThisUser;
            List<Wedding> weddingWithUser = dbContext.Wedding
            .Include(w=> w.User)
            .Include(w=> w.Guest) //This is grabbing the attending list all you can do with tis is 
            .ThenInclude(g=> g.User)
            .ToList();
            ViewBag.AllWeddings = weddingWithUser;
            return View();
        }
        [HttpGet("guest")]
        public IActionResult Guest()
        {
            List<Wedding> newGuest = dbContext.Wedding
            .Include(w=> w.User)
            .Include(w=> w.Guest) //This is grabbing the attending list all you can do with tis is 
            // .ThenInclude(g=> g.UserId)
            .ToList();
            dbContext.Add(newGuest);
            dbContext.SaveChanges();
            return View("Dashboard");
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            int? LoggedIn = HttpContext.Session.GetInt32("UserId");
            if(LoggedIn == 0 || LoggedIn == null)
            {
            return RedirectToAction("Index");
            }
            User ThisUser = dbContext.User.FirstOrDefault(u => u.UserId == (int)LoggedIn);
            if(ThisUser.UserId != LoggedIn)
            {
            return RedirectToAction("Index");
            }
            ViewBag.loggedIn = LoggedIn;
            
            return View();
        }
        [HttpPost("create")]
        public IActionResult Create(Wedding thisWedding)
        {
            if(ModelState.IsValid)
            {
                dbContext.Wedding.Add(thisWedding);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View("New");
        }

        [HttpGet("info/{Id}")]
        public IActionResult Info(int id)
        {
            int? LoggedIn = HttpContext.Session.GetInt32("UserId");
            if(LoggedIn == 0 || LoggedIn == null)
            {
            return RedirectToAction("Index");
            }
            User ThisUser = dbContext.User.FirstOrDefault(u => u.UserId == (int)LoggedIn);
            if(ThisUser.UserId != LoggedIn)
            {
            return RedirectToAction("Index");
            }
            ViewBag.loggedIn = LoggedIn;
            List<Wedding> weddingWithUser = dbContext.Wedding
            .Include(w=> w.Guest)
            .ThenInclude(g=> g.User)
            .ToList();
            ViewBag.AllWeddings = weddingWithUser;
            return View();
        }
    }
}
