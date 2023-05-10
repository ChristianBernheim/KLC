using KLC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace KLC.Controllers
{
    [Route("[controller]")]
    public class PatientController : Controller
    {
        string _baseUrl = "https://informatik13.ei.hv.se/klcapi/api/";

        private readonly ILogger<PatientController> _logger;

        public PatientController(ILogger<PatientController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
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

            //hämta patientfrån API
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                HttpResponseMessage response = await client.GetAsync("patients/" + currentPatientId);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    model.Patient = JsonConvert.DeserializeObject<Patient>(content);
                }

                else
                {
                    ViewBag.Message = "Tyvärr något gick fel " + response.ReasonPhrase;

                }
                return View(model);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Index(int id)
        {
            //sätter patientId till session
            if (id != 0)
            {
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

            //hämta patientfrån API
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                HttpResponseMessage response = await client.GetAsync("patients/" + currentPatientId);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    model.Patient = JsonConvert.DeserializeObject<Patient>(content);
                }

                else
                {
                    ViewBag.Message = "Tyvärr något gick fel " + response.ReasonPhrase;

                }
                return View(model);
            }
        }

        [HttpGet("Results")]
        public async Task<IActionResult> Results()
        {
            PatientViewModel model = new PatientViewModel();
            model.Patient = new Patient();
            model.Matningar = new List<MatningNews2>();

            //hämta patientfrån API
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                HttpResponseMessage response = await client.GetAsync("patients/" + HttpContext.Session.GetString("currentPatientId"));

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    model.Patient = JsonConvert.DeserializeObject<Patient>(content);
                }

                else
                {
                    ViewBag.Message = "Tyvärr något gick fel " + response.ReasonPhrase;

                }

                response = await client.GetAsync("MatningNews2/getAllFromPatient/" + HttpContext.Session.GetString("currentPatientId"));

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    model.Matningar = JsonConvert.DeserializeObject<List<MatningNews2>>(content);
                    model.Action = Util.GetNEWSAction(model.Matningar[model.Matningar.Count() - 1]);
                }

                else
                {
                    ViewBag.Message = "Tyvärr något gick fel " + response.ReasonPhrase;

                }

            }

            return View(model);
        }

        [HttpPost("Results")]
        public async Task<IActionResult> Results(IFormCollection collection)
        {
            
            MatningNews2 model = new MatningNews2();
            model.PatientId = int.Parse(HttpContext.Session.GetString("currentPatientId"));
            //Kollar så vi inte använder parse på null värden.
            if (collection["andningsfrekvens"].ToString() != "") { 
            model.Andningsfrekvens = int.Parse(collection["andningsfrekvens"]);
            }
            var test = collection["syremättnad"].ToString();

            if (collection["syremättnad"].ToString() != "")
            {
                model.SyreMattnad = int.Parse(collection["syremättnad"]);
            }
            if (collection["syrgas"].ToString() != "")
            {
                model.TillfordSyrgas = int.Parse(collection["syrgas"]);
            }
           
            if (collection["systolisktblodtryck"].ToString() != "")
            {
                model.SystolisktBlodtryck = int.Parse(collection["systolisktblodtryck"]);
            }
            if (collection["pulsfrekvens"].ToString() != "")
            {
                model.Pulsfrekvens = int.Parse(collection["pulsfrekvens"]);
            }
            
            if (collection["medvetandegrad"].ToString() != "")
            {
                model.Medvetandegrad = int.Parse(collection["medvetandegrad"]);
            }
            
            if (collection["temperatur"].ToString() != "")
            {
                model.Temperatur = int.Parse(collection["temperatur"]);
            }
            
            model.TidForMatning = DateTime.Now;

             //postar till mätningar
             using (HttpClient client = new HttpClient())
             {
                client.BaseAddress = new Uri(_baseUrl);
                //turn model into json string
                var json = JsonConvert.SerializeObject(model);  
                //turn json string into stringcontent
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                //post content to endpoint
                var response = await client.PostAsync("MatningNews2", content);
                //redirects to get action
                return RedirectToAction("Results","Patient");
             }
        }

        [HttpGet("Statistics")]
        public async Task<IActionResult> Statistics()
        {
            PatientViewModel model = new PatientViewModel();
            model.Patient = new Patient();
            model.Matningar = new List<MatningNews2>();

            //hämta patientfrån API
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                HttpResponseMessage response = await client.GetAsync("patients/" + HttpContext.Session.GetString("currentPatientId"));

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    model.Patient = JsonConvert.DeserializeObject<Patient>(content);
                }

                else
                {
                    ViewBag.Message = "Tyvärr något gick fel " + response.ReasonPhrase;

                }

                response = await client.GetAsync("MatningNews2/getAllFromPatient/" + HttpContext.Session.GetString("currentPatientId"));

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    model.Matningar = JsonConvert.DeserializeObject<List<MatningNews2>>(content);
                    model.Matningar.Reverse();
                }

                else
                {
                    ViewBag.Message = "Tyvärr något gick fel " + response.ReasonPhrase;

                }

            }

            return View(model);
        }

        [HttpGet("Test")]
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
        [HttpPost("DeleteAllFromPatient")]
        [HttpPut]
        [HttpDelete]
        public async Task<IActionResult> DeleteAllFromPatient()
        {
            int currentPatientId = int.Parse(HttpContext.Session.GetString("currentPatientId"));
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                HttpResponseMessage response = await client.DeleteAsync("MatningNews2/deleteAllFromPatient/" + currentPatientId);          
            }
            return Redirect("Index");
        }
        [HttpPost("DeleteLatestFromPatient")]
        [HttpPut]
        [HttpDelete]
        public async Task<IActionResult> DeleteLatestFromPatient()
        {
            int currentPatientId = int.Parse(HttpContext.Session.GetString("currentPatientId"));
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                HttpResponseMessage response = await client.DeleteAsync("MatningNews2/deleteLatestFromPatient/" + currentPatientId);
            }

            return Redirect("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
