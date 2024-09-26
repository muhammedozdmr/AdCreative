using AdCreative.Business;
using AdCreative.Dto;
using AdCreative.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using X.PagedList;
using CommandResult = AdCreative.Business.CommandResult;

namespace AdCreative.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WordService _wordService;

        public HomeController(ILogger<HomeController> logger, WordService wordService)
        {
            _logger = logger;
            _wordService = wordService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7076/")
            };

            var response = await client.GetAsync("api/WordDbAPI/Index");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                List<WordListDto> words = JsonConvert.DeserializeObject<List<WordListDto>>(result);
                var listWord = words.ToPagedList(pageNumber, 10);
                return View(listWord);
            }
            else
            {
                TempData["ErrorMessage"] = await response.Content.ReadAsStringAsync();
                return View();
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(WordDto dto)
        {
            if(dto.Word == null)
            {
                ViewBag.Words = _wordService.GetAll();
            }
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7076/");
            HttpResponseMessage response = await client.PostAsJsonAsync("api/WordDbAPI/Create/", dto);
            if (response.IsSuccessStatusCode)
            {
                var successMessage = "Kelime kaydedildi.";
                TempData["SuccessMessage"] = successMessage;
                return RedirectToAction("Index");
            }
            else
            {
                var errorResponse= await response.Content.ReadAsStringAsync();
                var errorMessage = JsonConvert.DeserializeObject<AdCreative.Dto.CommandResult>(errorResponse).Message;
                TempData["ErrorMessage"] = $"Kelime kaydedilemedi : {errorMessage}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost("RandomGenerator")]
        public async Task<IActionResult> RandomGenerator()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7076/");
            HttpResponseMessage response = await client.PostAsync("api/WordDbAPI/Generate/", null);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Kelime oluþturuldu.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Kelime oluþturulamadý !";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
