using AdCreative.Business;
using AdCreative.Dto;
using AdCreative.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using X.PagedList;

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
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44300/");
            HttpResponseMessage response = await client.GetAsync("api/WordDbAPI/Index/");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                List<WordDto> words = JsonConvert.DeserializeObject<List<WordDto>>(result);
                var listWord = words.ToPagedList(pageNumber, 10);
                return View(words);
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
            client.BaseAddress = new Uri("https://localhost:44300/");
            HttpResponseMessage response = await client.PostAsJsonAsync("api/WordDbAPI/Create/", dto);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Kelime kaydedildi.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Kelime kaydedilemedi !";
                return RedirectToAction("Index");
            }
        }

        [HttpPost("RandomGenerator")]
        public async Task<IActionResult> RandomGenerator()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44300/");
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
