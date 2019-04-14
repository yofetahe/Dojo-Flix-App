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
    public class RentalController : Controller
    {
        private DotnetFlixContext dbContext;

        public RentalController(DotnetFlixContext context)
        {
            dbContext = context;
        }

        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            int? UserID = HttpContext.Session.GetInt32("UserID");            
            if(UserID.ToString().Length == 0) 
                return RedirectToAction("Index", "User");
            
            User UserInfo = dbContext.Users
                .Include(u => u.UserMovies)
                .ThenInclude(um => um.Movie)
                .FirstOrDefault(u => u.UserId == UserID);

            return View("Dashboard", UserInfo);
        }

        [HttpGet("GetMovies")]
        public IActionResult GetMovies()
        {
            int? UserID = HttpContext.Session.GetInt32("UserID");            
            if(UserID.ToString().Length == 0) 
                return RedirectToAction("Index", "User");

            List<Movie> Movies = dbContext.Movies
                .Include(m => m.UserMovies)
                .Where(m => m.Avialiablity == true)
                .ToList();
            
            Rental model = new Rental();
            model.Movies = Movies;
                
            return View("Movies", model);
        }

        [HttpGet("GetMoveCreateForm")]
        public IActionResult GetMoveCreateForm()
        {
            int? UserID = HttpContext.Session.GetInt32("UserID");            
            if(UserID.ToString().Length == 0) 
                return RedirectToAction("Index", "User");

            return View("CreateMovie");
        }

        [HttpPost("createMovie")]
        public IActionResult CreateMovie(Movie Movie)
        {
            int? UserID = HttpContext.Session.GetInt32("UserID");            
            if(UserID.ToString().Length == 0) 
                return RedirectToAction("Index", "User");

            if(ModelState.IsValid)
            {
                Movie.Avialiablity = true;
                dbContext.Add(Movie);
                dbContext.SaveChanges();
                return RedirectToAction("GetMovies");
            }
            else
            {
                return View();
            }
        }

        [HttpGet("MovieDetail/{MovieId}")]
        public IActionResult MovieDetail(int MovieId)
        {
            int? UserID = HttpContext.Session.GetInt32("UserID");            
            if(UserID.ToString().Length == 0) 
                return RedirectToAction("Index", "User");

            Movie movie = dbContext.Movies
                .Include(m => m.UserMovies)
                .FirstOrDefault(m => m.MovieId == MovieId);
            return View(movie);
        }

        [HttpGet("CheckOutMovie/{MovieId}")]
        public IActionResult CheckOutMovie(int MovieId)
        {
            int? UserId = HttpContext.Session.GetInt32("UserID");            
            if(UserId.ToString().Length == 0) 
                return RedirectToAction("Index", "User");
            
            // To validate max of 5 movies
            User user = dbContext.Users
                .Include(u => u.UserMovies)                       
                .FirstOrDefault(u => u.UserId == UserId);

            int moveCounter = 0;
            foreach(var mov in user.UserMovies)
            {
                if(mov.isReturned == false)
                {
                    moveCounter += 1;
                }
            }
            
            if(moveCounter == 5){
                Movie mov = dbContext.Movies
                    .Include(m => m.UserMovies)
                    .FirstOrDefault(m => m.MovieId == MovieId);
                return View("MovieDetail", mov);
            }

            //Change the avialiablity of the movie
            Movie movie = dbContext.Movies.FirstOrDefault(m => m.MovieId == MovieId);
            movie.Avialiablity = false;
            dbContext.Movies.Update(movie);

            //Save the Movie-User Relation
            UserMovie um = new UserMovie();
            um.MovieId = MovieId;
            um.UserId = (int)UserId;
            dbContext.UserMovies.Add(um);

            dbContext.SaveChanges();

            return RedirectToAction("GetMovies");
        }

        [HttpGet("ReturnMovie/{UserMovieId}")]
        public IActionResult ReturnMovieForm(int UserMovieId)
        {
            int? UserId = HttpContext.Session.GetInt32("UserID");            
            if(UserId.ToString().Length == 0) 
                return RedirectToAction("Index", "User");

            UserMovie userMovie = dbContext.UserMovies
                .Include(um => um.Movie)
                .FirstOrDefault(um => um.UserMovieId == UserMovieId);

            Rental model = new Rental();
            model.UserMovie = userMovie;

            return View("ReturnMovie", model);
        }

        [HttpPost("ReturnMovie/{UserMovieId}/{MovieId}")]
        public IActionResult ReturnMovie(Rating Rating, int UserMovieId, int MovieId)
        {

            int? UserId = HttpContext.Session.GetInt32("UserID");            
            if(UserId.ToString().Length == 0) 
                return RedirectToAction("Index", "User");

            if(ModelState.IsValid)
            {
                //Change the avialiablity of the movie
                Movie movie = dbContext.Movies.FirstOrDefault(m => m.MovieId == MovieId);
                movie.Avialiablity = true;
                dbContext.Movies.Update(movie);
                
                //Update the Movie-User Relation - is-return status
                UserMovie userMovie = dbContext.UserMovies                    
                    .FirstOrDefault(um => um.UserMovieId == UserMovieId);
                userMovie.RatingValue = Rating.RatingValue;
                userMovie.isReturned = true;
                dbContext.UserMovies.Update(userMovie);

                dbContext.SaveChanges();

                return RedirectToAction("Dashboard");
            }
            else
            {
                 UserMovie userMovie = dbContext.UserMovies
                    .Include(um => um.Movie)
                    .FirstOrDefault(um => um.MovieId == MovieId && um.UserId == UserId);

                Rental model = new Rental();
                model.UserMovie = userMovie;

                return View("ReturnMovie", model);
            }
        }
    }
}