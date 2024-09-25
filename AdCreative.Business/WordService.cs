using AdCreative.Business.Validator;
using AdCreative.DataAccess;
using AdCreative.Domain;
using AdCreative.Dto;
using AutoMapper;
using System.Diagnostics;
using System.Text;

namespace AdCreative.Business
{
    public class WordService
    {
        private readonly WordContext _context;
        private readonly IMapper _mapper;
        private readonly WordValidator _wordValidator = new WordValidator();
        public WordService(WordContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CommandResult Create(WordDto wordDto)
        {
            try
            {
                if (string.IsNullOrEmpty(wordDto.Word))
                {
                    return CommandResult.Failure("Kelime boş olamaz.");
                }
                else
                {
                    if (!IsWordInDatabase(wordDto.Word))
                    {
                        return CommandResult.Failure("Kelime zaten mevcut !");
                    }
                    // AutoMapper ile DTO'dan entity'ye dönüşüm
                    WordAdd entity = _mapper.Map<WordAdd>(wordDto);
                    var validationResult = _wordValidator.Validate(entity);
                    if (validationResult.HasErrors)
                    {
                        return CommandResult.Failure(validationResult.ErrorString);
                    }

                    _context.Add(entity);
                    _context.SaveChanges();
                    return CommandResult.Success("Kayıt İşlemi Başarılı !");
                }

            }
            catch (Exception ex)
            {
                return CommandResult.Error("Kayıt İşlemi Başarısız !", ex);
            }
        }

        public IEnumerable<WordDto> GetAll()
        {
            try
            {
                return _context.Words.Select(MapToDto).ToList();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return new List<WordDto>();
            }
        }
        public void GenerateWord()
        {
            for (int i = 0; i < 10; i++)
            {
                var word = GenerateRandomWord();
                if (!IsWordInDatabase(word))
                {
                    var wordDto = new WordDto()
                    {
                        Word = word,
                        CountWord = word.Length
                    };
                    var wordModel = MapToEntity(wordDto);
                    _context.Words.Add(wordModel);
                    _context.SaveChanges();
                }
                else
                {
                    i--;
                }
            }
        }

        private string GenerateRandomWord()
        {
            var random = new Random();
            int lenght = random.Next(3, 51);
            var word = new StringBuilder();
            char nextChar;
            if (random.Next(0, 2) == 0)
            {
                nextChar = (char)random.Next('a', 'z' + 1);
            }
            else
            {
                nextChar = (char)random.Next('A', 'Z' + 1);
            }
            for (int i = 0; i < lenght; i++)
            {
                if (char.IsUpper(nextChar))
                {
                    nextChar = (char)random.Next('a', 'z' + 1);
                }
                else
                {
                    nextChar = (char)random.Next('A', 'Z' + 1);
                }
                word.Append(nextChar);
            }
            return word.ToString();
        }

        private bool IsWordInDatabase(string word)
        {
            var words = _context.Words.Select(MapToDto).ToList();
            if (string.IsNullOrEmpty(word))
            {
                return false;
            }
            else
            {
                foreach (var item in words)
                {
                    if (item.Word == word)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        private static WordAdd MapToEntity(WordDto wordDto)
        {
            return new WordAdd
            {
                Word = wordDto.Word,
                CountWord = wordDto.CountWord
            };
        }
        private static WordDto MapToDto(WordAdd word)
        {
            return new WordDto
            {
                Word = word.Word,
                CountWord = word.CountWord
            };
        }
    }
}
