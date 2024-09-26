using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdCreative.Dto
{
    public class WordListDto
    {
        public int Id { get; set; }
        public string? Word { get; set; }
        public int? CountWord { get; set; }
        public string? UniqueId { get; set; }
    }
}
