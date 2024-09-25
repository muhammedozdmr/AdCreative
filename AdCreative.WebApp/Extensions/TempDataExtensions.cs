using McTours.WebApp.Helpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace McTours.WebApp.Extensions
{
    public static class TempDataExtensions
    {
        public static string GetString(this ITempDataDictionary tempData,string key)
        {
            return tempData[key] as string;
        }
        public static string GetSuccessMessage(this ITempDataDictionary tempData)
        {
            return tempData.GetString(Keys.SuccessMessage);
        }
        public static string GetErrorMessage(this ITempDataDictionary tempData)
        {
            return tempData.GetString(Keys.ErrorMessage);
        }
    }
}
