using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
    public class Plant
    {
        public int Id { get; set; }
        public string PlantName { get; set; }
        public string Description   { get; set; }
        public string PlantExposure { get; set; }
        public int Spread { get; set; }
        public string TypeSoldIn { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }

    }
}
