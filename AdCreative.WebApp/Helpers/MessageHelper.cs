using Microsoft.AspNetCore.Html;
using System.Text;

namespace McTours.WebApp.Helpers
{
    public static class MessageHelper
    {
        //Html elementlerini bu şekilde render ediyor string nesnelerini
        public static HtmlString DisplayError(string message)
        {
            //Environment.NewLine Windows: \n\r Linux: \r MacOS: \n bu yüzden farklı işletim sistemlerinde sıkıntı çıkmasın diye
            //onu yazdık. Daktilodan geliyor. sayfa sonunda çark ile bir satır aşağı in \n ve satır başı yap Carriage Return \r demek
            var splittedMessages = message.Split(Environment.NewLine);
            var stringBuilder = new StringBuilder();
            stringBuilder
                .Append("<div class=\"alert alert-danger\">")
                .Append("<ul class=\"my-0\">");

            foreach(var errorMessage in splittedMessages)
            {
                //stringBuilder.Append($"<li>{errorMessage}</li>");
                stringBuilder.AppendFormat("<li>{0}</li>",errorMessage);
            }

            stringBuilder
                .Append("</ul>")
                .Append("</div>");

            return new HtmlString(stringBuilder.ToString());
        }
        public static HtmlString DisplaySuccess(string message)
        {
            var splittedMessages = message.Split(Environment.NewLine);
            var stringBuilder = new StringBuilder();
            stringBuilder
                .Append("<div class=\"alert alert-success\">")
                .Append("<ul class=\"my-0\">");

            foreach (var successMessage in splittedMessages)
            {
                stringBuilder.AppendFormat("<li>{0}</li>", successMessage);
            }

            stringBuilder
                .Append("</ul>")
                .Append("</div>");

            return new HtmlString(stringBuilder.ToString());
        }
    }
}
