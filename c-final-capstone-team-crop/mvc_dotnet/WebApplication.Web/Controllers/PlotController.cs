using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Web.DAL;
using WebApplication.Web.Models;
using WebApplication.Web.Providers.Auth;

namespace WebApplication.Web.Controllers
{
    [AuthorizationFilter]
    public class PlotController : Controller
    {
        public static string ViewedPlot = "Auth_Plot_Id";

        private IPlotDAO plotDAO;
        private IPlantDAO plantDAO;
        private IPlantPlotDAO plantPlotDAO;

        public PlotController(IPlotDAO plotDAO, IPlantDAO plantDAO, IPlantPlotDAO plantPlotDAO)
        {
            this.plotDAO = plotDAO;
            this.plantDAO = plantDAO;
            this.plantPlotDAO = plantPlotDAO;
        }

        [HttpGet]
        public IActionResult CreatePlot()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePlot(Plot plot)
        {

            int userId = HttpContext.Session.GetInt32(SessionAuthProvider.UserId).Value;
            if (ModelState.IsValid)
            {
                int newPlotId = plotDAO.CreatePlot(userId, plot);
                HttpContext.Session.SetInt32("Auth_Plot_Id", newPlotId);
                return RedirectToAction("UserPlotInfo", "Account");
            }
            return View(plot);

        }

        [HttpGet]
        public IActionResult ViewPlot(int id)
        {
            ViewPlotVM vm = new ViewPlotVM();
            vm.Plot = plotDAO.GetPlotById(id);
            vm.RecommendedPlants = plantDAO.GetPlantsByExposure(vm.Plot.PlotExposure);
            vm.FullSunPlants = plantDAO.GetPlantsFullSun();
            vm.PartialSunPlants = plantDAO.GetPlantsPartialSun();
            vm.FullShadePlants = plantDAO.GetPlantsFullShade();
            vm.CanvasPlantInfo = plantPlotDAO.GetPlantPlotInfo(id);
            vm.ListAllPlants = plantDAO.GetAllPlants();
            HttpContext.Session.SetInt32("ViewedPlot", id);

            return View(vm);
        }

    }
}