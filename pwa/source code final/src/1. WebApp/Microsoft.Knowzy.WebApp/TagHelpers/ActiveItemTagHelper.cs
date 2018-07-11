// MIT License
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE

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
