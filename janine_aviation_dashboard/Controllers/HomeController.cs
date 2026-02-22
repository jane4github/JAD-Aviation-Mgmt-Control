using System.Diagnostics;
using janine_aviation.Models;
using Microsoft.AspNetCore.Mvc;

namespace janine_aviation.Controllers
{

    

    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Menu()
        {
            // Deine Liste erstellen
            var meinMenue = new List<MenuItem>
            {
                /*
                aeronautical knowledge

                     - thermo dynamics meteorology
                      - meteorology - reading weather charts, extended knowledge of weather - reading clouds - calculation of power setting in altitudes.
                     - piston engines
                     - propulsion systems
                     - flight physics and airframe
                     - flight planning and navigation
                     - Aeronautical Medical decision making
                     - Hazards in flight

                       - LTR loss of tail rotor effectivness
                       - Settling with power Rotor Vortex Ringstate
                       - Roll Over
                       - Mast Bumping esp. Hinge Rotorheads
                       - Fire in Flight - electrical fire, gasoline combustion fire
                       - Main Engine Failure   
                    - Radio Communication 
                    - Aviation Regulations - FAA / EASA

                */

            //Aviation Haupknoten - MainNode
                new MenuItem { Title = "Aviation", Url = "/" },
                new MenuItem {
                    Title = "Aviation_knowledge",
                    Url = "/Aviation_knowledge",
                    Children = new List<MenuItem>
                    {
                        new MenuItem { Title = "Overview", Url = "/aviation_ov/" },
                        new MenuItem { Title = "Aviation Knowledge", Url = "/Aviation/" }
                    }
                },

                            //Aviation Haupknoten - MainNode
                new MenuItem { Title = "Aeronautic", Url = "/" },
                new MenuItem {
                    Title = "Aeronautical_knowledge",
                    Url = "/Aeronautic",
                    Children = new List<MenuItem>
                    {
                        new MenuItem { Title = "Overview", Url = "/Aeronautic_ov/" },
                        new MenuItem { Title = "Aeronautical Knowledge", Url = "/Aeronautic/" }
                    }
                },

                //Identity Hauptknoten - Mainnode
                new MenuItem { Title = "Identity", Url = "/" },
                new MenuItem {
                    Title = "Janine Servais",
                    Url = "/Identity",
                    Children = new List<MenuItem>
                    {
                        new MenuItem { Title = "Identity", Url = "/proof_ID/" },
                        new MenuItem { Title = "Janine ID", Url = "/Copy_of_ID/" }
                    }
                },

            new MenuItem { Title = "Kontakt", Url = "/kontakt" }
            };

            // Die Liste als Model an die View übergeben
            return View(meinMenue);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}     

