using AdCreative.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdCreative.Business.Validator
{
    internal class WordValidator
    {
        public ValidationResult Validate(WordAdd word)
        {
            var validationResult = new ValidationResult();
            if (string.IsNullOrEmpty(word.Word))
            {
                validationResult.AddError("Kelime boş olamaz.");
            }
            if (word.CountWord > 100)
            {
                validationResult.AddError("Karakter sayısı 100 den fazla olamaz.");
            }
            return validationResult;
        }
    }
}
