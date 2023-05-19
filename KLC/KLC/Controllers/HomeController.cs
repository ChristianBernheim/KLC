using KLC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;

namespace KLC.Controllers
{
    public class HomeController : Controller
    {
        string _baseUrl = "https://informatik13.ei.hv.se/klcapi/api/";

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            PatientViewModel model = new PatientViewModel();
            model.AllPatients = new List<Patient>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                HttpResponseMessage response = await client.GetAsync("patients");

                if(response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    model.AllPatients = JsonSerializer.Deserialize<List<Patient>>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                
                else
                {
                    ViewBag.Message = "Tyvärr något gick fel " + response.ReasonPhrase;

                }           
                return View(model);
            }
        }
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
       // public IActionResult Index()
       // {
       //     return View();
       // }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult guide()
        {
            return View();
        }

    }
}