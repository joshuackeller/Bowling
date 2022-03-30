using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bowling.Models;
using Microsoft.EntityFrameworkCore;

namespace Bowling.Controllers
{
    public class HomeController : Controller
    {

        private IBowlerRepository _repo { get; set; }


        public HomeController(IBowlerRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index()
        {


            return View();
        }

        public IActionResult Bowlers(string teamName)
        {
            var bowlers = _repo.Bowlers
                .Include(x => x.Team)
                .Where(x => x.Team.TeamName == teamName || teamName == null)
                .ToList();

            ViewBag.Team = teamName;


            return View(bowlers);
        }


        [HttpGet]
        public IActionResult EditBowler(int bowlerId)
        {
            var bowler = _repo.Bowlers.FirstOrDefault(x => x.BowlerID == bowlerId);

            ViewBag.Teams = _repo.Teams.ToList();

            return View(bowler);

        }
        [HttpPost]
        public IActionResult EditBowler(Bowler b)
        {
            if (ModelState.IsValid)
            {
                
                _repo.SaveBowler(b);

                var bowlers = _repo.Bowlers
                .Include(x => x.Team)
                .ToList();

                return View("Bowlers", bowlers);
            }
            else
            {
                var bowler = _repo.Bowlers.FirstOrDefault(x => x.BowlerID == b.BowlerID);

                ViewBag.Teams = _repo.Teams.ToList();

                return View(bowler);
            }
        }

        [HttpGet]
        public IActionResult AddBowler()
        {
           
            ViewBag.Teams = _repo.Teams.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult AddBowler(Bowler b)
        {
           
            if (ModelState.IsValid)
            {
                _repo.CreateBowler(b);
                var bowlers = _repo.Bowlers
                .Include(x => x.Team)
                .ToList();

                return View("Bowlers", bowlers);
            }
            else
            {
                ViewBag.NewId = _repo.Bowlers.Count() + 1;
                ViewBag.Teams = _repo.Teams.ToList();
                return View();
            }
           
        }


        [HttpGet]
        public IActionResult DeleteBowler (int bowlerId)
        {

            var bowler = _repo.Bowlers.FirstOrDefault(x => x.BowlerID == bowlerId);
           

            return View(bowler);
        }
        [HttpPost]
        public IActionResult DeleteBowler (Bowler b)
        {

            _repo.DeleteBowler(b);
            var bowlers = _repo.Bowlers
                .Include(x => x.Team)
                .ToList();

            return View("Bowlers", bowlers);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
