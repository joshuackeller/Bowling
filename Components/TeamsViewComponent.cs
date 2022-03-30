using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Bowling.Models;
using Microsoft.EntityFrameworkCore;


// See /Shared/Componenets/Teams/Defaul.cshtml to see the View
namespace Bowling.Components
{
    public class TeamsViewComponent : ViewComponent
    {
        
        private IBowlerRepository repo;

        public TeamsViewComponent(IBowlerRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedTeam = RouteData?.Values["teamName"];
          

            var teams = repo.Bowlers
                .Include(x => x.Team)
                .Select(x => x.Team)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
                

            return View(teams);
        }
    }
}
