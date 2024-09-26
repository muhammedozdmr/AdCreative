using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdCreative.Dto
{
    public class WordViewModelDto
    {
        public WordDto? Word { get; set; }
        public List<WordListDto>? Words { get; set; }
    }
}
