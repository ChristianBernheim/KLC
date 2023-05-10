using KLC.Models;
using KLC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KLC.Controllers
{

    public class PatientController : Controller
    {
        private readonly ILogger<PatientController> _logger;

        public PatientController(ILogger<PatientController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Results()
        {


            return View();
        }

        public IActionResult Statistics()
        {
            return View();
        }


        public IActionResult Test()
        {
            PatientViewModel model = new PatientViewModel();
            model.Matningar = new List<MatningNews2>();
            //gör två mätningar
            MatningNews2 matning1 = new MatningNews2();
            matning1.Andningsfrekvens = 0;
            matning1.SyreMattnad = 13;
            matning1.TillfordSyrgas = null;
            matning1.SystolisktBlodtryck = -3;
            matning1.Pulsfrekvens = 0;
            matning1.Medvetandegrad = 3;
            matning1.Temperatur = -1;
            matning1.TidForMatning = DateTime.Now;
            model.Matningar.Add(matning1);
            MatningNews2 matning2 = new MatningNews2();
            matning2.Andningsfrekvens = -3;
            matning2.SyreMattnad = -3;
            matning2.TillfordSyrgas = 0;
            matning2.SystolisktBlodtryck = -3;
            matning2.Pulsfrekvens = 0;
            matning2.Medvetandegrad = 0;
            matning2.Temperatur = -1;
            matning2.TidForMatning = DateTime.Now;
            model.Matningar.Add(matning2);
            return View("Test",model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
