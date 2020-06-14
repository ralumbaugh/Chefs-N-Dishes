using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChefsNDishes.Models;

namespace ChefsNDishes.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult ChefRoster()
        {
            Chef[] AllChefs = dbContext.Chefs
                .Include(d => d.CreatedDishes)
                .ToArray();
            return View(AllChefs);
        }
        [HttpGet("/dishes")]
        public IActionResult Dishes()
        {
            Dish[] AllDishes = dbContext.Dishes
                .Include(c => c.Creator)
                .ToArray();
            return View(AllDishes);
        }
        [HttpGet("/new")]
        public IActionResult NewChef()
        {
            return View();
        }
        public IActionResult MakeNewChef(Chef chef)
        {
            if(DateTime.Now.CompareTo(chef.DateOfBirth)<0)
            {
                ModelState.AddModelError("DateOfBirth","Please be born in the past.");
            }
            else if(DateTime.Now.AddYears(-18).CompareTo(chef.DateOfBirth)<0)
            {
                ModelState.AddModelError("DateOfBirth","Chef must be at least 18.");
            }
            if(ModelState.IsValid)
            {
                dbContext.Add(chef);
                dbContext.SaveChanges();
                return RedirectToAction("ChefRoster");
            }
            else
            {
                return View("NewChef");
            }
        }
        [HttpGet("/dishes/new")]
        public IActionResult NewDish()
        {
            WrapperModel NewDishWrapper = new WrapperModel();
            NewDishWrapper.AllChefs = dbContext.Chefs.ToList();
            return View(NewDishWrapper);
        }
        public IActionResult MakeNewDish(WrapperModel WrappedDish)
        {
            if(dbContext.Chefs.FirstOrDefault(chef => chef.ChefId == WrappedDish.OneDish.ChefId) == null)
            {
                ModelState.AddModelError("OneDish.ChefId","There is no chef that goes by that ID");
            }
            if(ModelState.IsValid)
            {
                dbContext.Add(WrappedDish.OneDish);
                dbContext.SaveChanges();
                return RedirectToAction("Dishes");
            }
            else
            {
                WrappedDish.AllChefs = dbContext.Chefs.ToList();
                return View("NewDish", WrappedDish);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
