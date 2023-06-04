using CharacterCounter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace CharacterCounter.Controllers
{
    public class CharacterCounterController : Controller
    {
        private readonly ILogger<CharacterCounterController> _logger;

        public CharacterCounterController(ILogger<CharacterCounterController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ResponseData GetCharacterCount(string word)
        {
            ResponseData response = new ResponseData();
            List<LetterCountDetails> data = new List<LetterCountDetails>();
            response.Data = data;

            try
            {
                if (string.IsNullOrEmpty(word))
                {
                    response.Status = Status.Error;
                    response.Message = "Please enter a word or a sentence";

                    return response;
                }

                if (!word.All(x => Char.IsLetter(x) || Char.IsWhiteSpace(x) || Char.IsDigit(x)))
                {
                    response.Status = Status.Error;
                    response.Message = "Data contain special characters";
                    return response;
                }

                foreach (char letter in word)
                {
                    string character = letter.ToString().ToLower();

                    if (string.IsNullOrWhiteSpace(character))
                    {
                        continue;
                    }

                    if (data.Any(x => x.Letter == character))
                    {
                        data.Where(x => x.Letter == character).ToList().ForEach(y => y.Count++);
                    }
                    else
                    {
                        LetterCountDetails details = new LetterCountDetails();
                        details.Letter = character;
                        details.Count = 1;

                        data.Add(details);
                    }
                }

                response.Data = data.OrderBy(x => x.Letter).ToList();
                response.Status = Status.Success;

                return response;
            }
            catch (Exception ex)
            {
                response.Status = Status.Error;
                response.Message = "Error occurred - " + ex.Message;
                return response;
            }
        }
    }
}