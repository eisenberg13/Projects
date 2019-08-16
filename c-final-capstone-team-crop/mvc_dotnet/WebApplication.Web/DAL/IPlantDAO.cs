using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
    public interface IPlantDAO
    {
        List<Plant> GetPlantsByExposure(string exposure);
        List<Plant> GetPlantsFullShade();
        List<Plant> GetPlantsPartialSun();
        List<Plant> GetPlantsFullSun();
        List<Plant> GetAllPlants();

    }
}
