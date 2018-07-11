// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Microsoft.Knowzy.WebApp.TagHelpers
{
    [HtmlTargetElement("li", Attributes = "nav-controller")]
    public class ActiveItemTagHelper : TagHelper
    {
        [HtmlAttributeName("nav-controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("nav-action")]
        public string Action { get; set; }

        [HtmlAttributeName("nav-class")]
        public string Class { get; set; } = "active";

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var currentController = (string)ViewContext.RouteData.Values["controller"];
            var currentAction = (string)ViewContext.RouteData.Values["action"];
            if (currentController == Controller && currentAction == (Action ?? currentAction))
            {
                var classes = output.Attributes.Where(attribute => attribute.Name == "class")
                    .Select(attribute => attribute.Value)
                    .ToList();
                classes.Add(Class);
                output.Attributes.Add("class", string.Join(" ", classes));
            }
        }
    }
}
