using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdCreative.Dto
{
    public class CommandResult
    {
        public bool? IsSuccess { get; set; }
        public string? Message { get; set; } = "Başarılı Giriş";
        public string? ErrorMessage { get; set; } = "Hatalı Giriş";
    }
}
