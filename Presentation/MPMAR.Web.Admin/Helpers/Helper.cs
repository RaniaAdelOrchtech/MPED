using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Helpers
{
    public static class Helper
    {
        /// <summary>
        /// remove propperties from model state
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static ModelStateDictionary RemoveProperties(this ModelStateDictionary modelState, params string[] properties)
        {
            foreach (string property in properties)
            {
                modelState.Remove(property);
            }

            return modelState;
        }
        /// <summary>
        /// check if the property from the text editor is empty or not
        /// </summary>
        /// <param name="htmlProperty"></param>
        /// <param name="propertyName"></param>
        /// <param name="modelState"></param>
        public static void ValidateHtml(this string htmlProperty, string propertyName, ModelStateDictionary modelState)
        {
            if (!string.IsNullOrWhiteSpace(htmlProperty))
            {
                StringBuilder stringBuilder = new StringBuilder(htmlProperty);

                string brPatern = "<p><br></p>";
                string ulPatern = "<ul><li><br></li></ul>";
                string olPatern = "<ol><li><br></li></ol>";
                string ulTag = "<ul>";
                string ulCloseTag = "</ul>";
                string olTag = "<ol>";
                string olCloseTag = "</ol>";
                string liTag = "<li>";
                string liCloseTag = "</li>";
                string pTag = "<p>";
                string pCloseTag = "</p>";
                string brTag = "<br>";
                string brCloseTag = "</br>";

                List<string> paternList = new List<string> { brPatern, ulPatern, olPatern, ulTag, ulCloseTag, olTag, olCloseTag, liTag, liCloseTag, pTag, pCloseTag, brTag, brCloseTag };

                paternList.ForEach(patern =>
                {
                    stringBuilder.Replace(patern, "");
                });

                if (stringBuilder.ToString() == "")
                {
                    modelState.AddModelError("Section.EnDescription", $"This {propertyName} field is required");
                }
            }
        }
    }
}
