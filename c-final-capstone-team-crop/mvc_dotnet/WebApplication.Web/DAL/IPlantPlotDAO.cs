using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
    public interface IPlantPlotDAO
    {
        void SavePlotInfo(PlantPlot[] plantPlot, int plotId);
        List<PlantPlot> GetPlantPlotInfo(int plotId);

    }
}
