using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.TagHelpers
{
    [HtmlTargetElement("select", Attributes = DisabledAttributeName)]
    public class SelectTagHelper : TagHelper
    {
        private const string DisabledAttributeName = "asp-disabled";

        /// Get the value of the condition
        [HtmlAttributeName(DisabledAttributeName)]
        public bool Disabled { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (Disabled)
                output.Attributes.SetAttribute("disabled", null);
        }
    }
}
