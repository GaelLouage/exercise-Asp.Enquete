using Infrastructuur.Services;
using Microsoft.AspNetCore.Mvc;
using Infrastructuur.Entities;
namespace Enquete.Controllers
{
    public class VervoerController : Controller
    {

        public int[,] counters = new int[5, 4];
        private readonly IVervoerService _vervoerService;
        private List<Infrastructuur.Entities.Enquete> enquetes;
        public VervoerController(IVervoerService vervoerService)
        {
            _vervoerService = vervoerService;
        }

        public IActionResult Index()
        {

            enquetes = _vervoerService.GetEnquetes().ToList();
          
                for (int i = 0; i < enquetes.Count; i++)
                {
                    if (enquetes[i].Year < 1960)
                    {
                        SwitchVervoerTypes(enquetes[i].TypeOfTransport, 0);
                    }
                    else if (enquetes[i].Year < 1970)
                    {
                        SwitchVervoerTypes(enquetes[i].TypeOfTransport, 1);
                    }
                    else if (enquetes[i].Year < 1980)
                    {
                        SwitchVervoerTypes(enquetes[i].TypeOfTransport, 2);
                    }
                    else if (enquetes[i].Year < 1990)
                    {
                        SwitchVervoerTypes(enquetes[i].TypeOfTransport, 3);
                    }
                    else if (enquetes[i].Year >= 1990)
                    {
                        SwitchVervoerTypes(enquetes[i].TypeOfTransport, 4);
                    }
                }
                
      
            TempData["numbers"] = counters;
            return View(enquetes);
        }
        private void SwitchVervoerTypes(string vervoer, int row)
        {
            vervoer = vervoer.Replace("\"","");
            switch(vervoer)
            {
                case "E":
                    counters[row, 0]++;
                    break;
                case "F":
                    counters[row, 1]++;
                    break;
                case "OV":
                    counters[row, 2]++;
                    break;
                case "A":
                    counters[row, 3]++;
                    break;
                default:
                    // no def
                    break;
            }
        }
    }
}
