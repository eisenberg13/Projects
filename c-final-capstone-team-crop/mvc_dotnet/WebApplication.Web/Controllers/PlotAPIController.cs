using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Web.DAL;
using WebApplication.Web.Models;

namespace WebApplication.Web.Controllers
{
    [Route("api/plotapi")]
    [ApiController]
    public class PlotAPIController : ControllerBase
    {
        public static string ViewedPlot = "Auth_Plot_Id";
        private IPlotDAO plotDAO;
        private IPlantDAO plantDAO;
        private IPlantPlotDAO plantPlotDAO;

        public PlotAPIController(IPlotDAO plotDAO, IPlantDAO plantDAO, IPlantPlotDAO plantPlotDAO)
        {
            this.plotDAO = plotDAO;
            this.plantDAO = plantDAO;
            this.plantPlotDAO = plantPlotDAO;
        }
        [HttpGet]
        public ActionResult<PlantPlot[]> Get()
        {
            List<PlantPlot> p = new List<PlantPlot>()
            {
                new PlantPlot(){ left=10, plantId=200, top=10},
                new PlantPlot(){ left=15, plantId=23, top=44},
            };
            return p.ToArray();
        }

        [HttpPut]
        public ActionResult PutPlot([FromBody]PlantPlot[] plots)
        {
            int? activePlotId = HttpContext.Session.GetInt32("ViewedPlot");
            if(activePlotId != null)
            {
                plantPlotDAO.SavePlotInfo(plots, activePlotId.Value);
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }
    }
}