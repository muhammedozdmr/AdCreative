using AdCreative.Business;
using AdCreative.Dto;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace InputCheckOnDBWithApi.Controllers
{
    [Route("api/(controller)")]
    [ApiController]
    public class WordDbAPI : Controller
    {
        private readonly WordService _wordService;

        //DI ile WordService sınıfını inject ediyoruz.
        public WordDbAPI(WordService wordService)
        {
            _wordService = wordService;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            var words = _wordService.GetAll();
            return Ok(words);
        }
        [HttpPost("Create")]
        public IActionResult Create(WordDto wordDto)
        {
            var commandResult = _wordService.Create(wordDto);
            if (commandResult.IsSuccess)
            {
                return Ok(commandResult);
            }
            else
            {
                return NotFound(commandResult);
            }
        }

        [HttpPost("Generate")]
        public IActionResult Generate()
        {
            _wordService.GenerateWord();
            return Ok();
        }
    }
}
