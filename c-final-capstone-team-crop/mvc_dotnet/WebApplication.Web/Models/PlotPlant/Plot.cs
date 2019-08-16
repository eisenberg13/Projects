using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
    public class Plot
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A name for your plot is required")]
        public string PlotName { get; set; }
        [Required(ErrorMessage = "A length for your plot is needed")]
        public int Length { get; set; }
        [Required(ErrorMessage = "A width for your plot is needed")]
        public int  Width { get; set; }
        [Required(ErrorMessage = "We need to know how much light your plot gets")]
        public string PlotExposure { get; set; }
        public int UserId { get; set; }
        public List<Plant> Plants { get; set; }

        public Plot()
        {
            this.Plants = new List<Plant>();
        }
    }
}
