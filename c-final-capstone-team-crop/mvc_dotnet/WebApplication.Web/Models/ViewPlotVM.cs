using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
    public class ViewPlotVM
    {
        public Plot Plot { get; set; }
        public List<Plant> RecommendedPlants { get; set; }
        public List<Plant> FullShadePlants { get; set; }
        public List<Plant> PartialSunPlants { get; set; }
        public List<Plant> FullSunPlants { get; set; }
        public List<PlantPlot> CanvasPlantInfo { get; set; }
        public List<Plant> ListAllPlants { get; set; }
        
        //public string CanvasPlantInfoString
        //{
        //    get
        //    {
        //        return JsonConvert.SerializeObject(CanvasPlantInfo);
        //    }
        //    set
        //    {
        //        CanvasPlantInfo = JsonConvert.DeserializeObject<PlantPlot[]>(value);
        //    }
        //}
    }
}
