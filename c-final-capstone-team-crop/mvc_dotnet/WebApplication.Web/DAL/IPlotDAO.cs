using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
    public interface IPlotDAO
    {
        int CreatePlot(int userId, Plot plot);
        Plot GetPlotById(int id);
        bool AddPlantToPlot(int plotId, int plantId);
        List<Plot> GetUsersPlots(int userId);
    }
}
