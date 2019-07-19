using Capstone.Views;
using Microsoft.Extensions.Configuration;
using System.IO;
using Capstone.DAL;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the connection string from the appsettings.json file
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            string connectionString = configuration.GetConnectionString("Project");

            // Create DAO objects
            // These will be shared with all of our menu objects
            // Each menu will have access to all of them because they are sequential
            // Yeah it's messy, but that's how we have to do it
            IParkDAO parkDAO = new ParkSqlDAO(connectionString);
            ICampgroundDAO campgroundDAO = new CampgroundSqlDAO(connectionString);
            ISiteDAO siteDAO = new SiteSqlDAO(connectionString);
            IReservationDAO reservationDAO = new ReservationSqlDAO(connectionString);
            
            // Move to our first menu
            ViewParksMenu menu = new ViewParksMenu(parkDAO, campgroundDAO, siteDAO, reservationDAO);
            menu.Run();
        }
    }
}