using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebStore.TagHelpers
{
    [HtmlTargetElement(Attributes = AttributeName)]
    public class ActiveRoute : TagHelper
    {
        private const string AttributeName = "ws-is-active-route";
        private const string IgnoreAction = "ws-ignore-action";

        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("asp-all-route-data", DictionaryAttributePrefix = "asp-route-")]
        public Dictionary<string, string> RouteValues { get; set; } = new(StringComparer.OrdinalIgnoreCase);

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var ignore_action = output.Attributes.ContainsName(IgnoreAction);

            if (IsActive(ignore_action))
                MakeActive(output);

            output.Attributes.RemoveAll(AttributeName);
        }

        private bool IsActive(bool IsIgnoreAction)
        {
            var route_values = ViewContext.RouteData.Values;

            var current_controller = route_values["controller"]?.ToString();
            var current_action = route_values["Action"]?.ToString();

            const StringComparison str_comp = StringComparison.OrdinalIgnoreCase;
            if (!string.IsNullOrEmpty(Controller) && !string.Equals(current_controller, Controller, str_comp))
                return false;

            if (!IsIgnoreAction && !string.IsNullOrEmpty(Action) && !string.Equals(current_action, Action, str_comp))
                return false;

            foreach(var (key, value) in RouteValues)
                if (!route_values.ContainsKey(key) || route_values[key]?.ToString() != value)
                    return false;

            return true;
        }

        private static void MakeActive(TagHelperOutput output)
        {
            var class_attribute = output.Attributes.FirstOrDefault(attr => attr.Name == "class");

            if(class_attribute is null)
                output.Attributes.Add("class", "active");
            else
            {
                if(class_attribute.Value.ToString()?.Contains("active") ?? false)
                    return;
                output.Attributes.SetAttribute("class", class_attribute.Value + " active");
            }
        }
    }
}
