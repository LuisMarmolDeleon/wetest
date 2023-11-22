    using global::ConsoleApp2;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    namespace ReportLoader.Pages.WebsiteClass
    {
        [HtmlTargetElement("alert")]
        public class Helper : TagHelper
        {
            public Alert Alert { get; set; }
            public string RiskLevel { get; set; }

            public override void Process(TagHelperContext context, TagHelperOutput output)
            {
                output.TagName = "div";
                output.Attributes.SetAttribute("class", "frame-group");
                output.Content.SetHtmlContent($@"
                <div class='cross-site-scripting-reflecte-wrapper'>
                    <div class='cross-site-scripting'>
                        {Alert.name}
                    </div>
                </div>
                <div class='{RiskLevel.ToLower()}-wrapper'>
                    <b class='httpsgoogle-gruyereappspot'>{RiskLevel.ToUpper()}</b>
                </div>
                <div class='wrapper'>
                    <div class='httpsgoogle-gruyereappspot'>{Alert.instances}</div>
                </div>");
            }
        }
    }
