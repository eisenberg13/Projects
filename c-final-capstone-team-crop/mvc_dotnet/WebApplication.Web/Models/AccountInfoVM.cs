using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
    public class AccountInfoVM
    {
        public Plot plot { get; set; }
        public List<Plot> UserPlots { get; set; }
    }
}
