using KLC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace KLC.Controllers
{
    [Route("[controller]")]
    public class PatientController : Controller
    {

        string _baseUrl = "https://informatik13.ei.hv.se/klcapi/api/";

        [HttpGet("{id}")]
        public async Task<ActionResult> Index(int id)
        {
            //sätter patientId till session
            if(id != 0) { 
            HttpContext.Session.SetString("currentPatientId", id.ToString());
                
            }

            int currentPatientId;
            //om det finns user i session,sätt currpatid till sessionsvärde
            if (HttpContext.Session.GetString("currentPatientId") != null)
			{
                currentPatientId = int.Parse(HttpContext.Session.GetString("currentPatientId"));
                
            }
            //redir till index om inget hittas
			else
			{
                return RedirectToAction("Index", "Home");
			}
            //RedirectToAction (string actionName, string controllerName);
            
            
            PatientViewModel model = new PatientViewModel();
            model.Patient = new Patient();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                HttpResponseMessage response = await client.GetAsync("patients/"+currentPatientId);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    model.Patient = JsonSerializer.Deserialize<Patient>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }

                else
                {
                    ViewBag.Message = "Tyvärr något gick fel " + response.ReasonPhrase;

                }
                return View(model);
            }
        }
    //****************************************************************//


        private readonly ILogger<PatientController> _logger;

        public PatientController(ILogger<PatientController> logger)
        {
            _logger = logger;
        }

        //public IActionResult Index()
        //{
        //   return View();
        //}
        [HttpGet]
        public IActionResult Results()
        {
            return View();
        }

        [HttpPost("newsform")]
        public async Task<IActionResult> Results(IFormCollection collection)
        {
            //Får inte uppförslag i share tydligen...
                MatningNews2 model = new MatningNews2();
           //im going eating 
           //hörlurarna dog
           //hör
                model.Andningsfrekvens = int.Parse(collection["andningsfrekvens"]);
                model.
                model.
                model.
                model.
                model.
                model.
                model.

             using (HttpClient client = new HttpClient())
                {
                client.BaseAddress = new Uri(_baseUrl);
                HttpResponseMessage response = await client.GetAsync("patients/" + currentPatientId);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    model.Patient = JsonSerializer.Deserialize<Patient>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }

                else
                {
                    ViewBag.Message = "Tyvärr något gick fel " + response.ReasonPhrase;

                }
                return View(model);
                }

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
