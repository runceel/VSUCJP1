#pragma checksum "C:\Repos\runceel\VSUCJP1\Demo3\Demo3.BlazorClient\Pages\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b8a0676cf9c5fe103835054c13a74433020531e5"
// <auto-generated/>
#pragma warning disable 1591
namespace Demo3.BlazorClient.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#line 1 "C:\Repos\runceel\VSUCJP1\Demo3\Demo3.BlazorClient\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#line 2 "C:\Repos\runceel\VSUCJP1\Demo3\Demo3.BlazorClient\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#line 3 "C:\Repos\runceel\VSUCJP1\Demo3\Demo3.BlazorClient\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#line 4 "C:\Repos\runceel\VSUCJP1\Demo3\Demo3.BlazorClient\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#line 5 "C:\Repos\runceel\VSUCJP1\Demo3\Demo3.BlazorClient\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#line 6 "C:\Repos\runceel\VSUCJP1\Demo3\Demo3.BlazorClient\_Imports.razor"
using Demo3.BlazorClient;

#line default
#line hidden
#line 7 "C:\Repos\runceel\VSUCJP1\Demo3\Demo3.BlazorClient\_Imports.razor"
using Demo3.BlazorClient.Shared;

#line default
#line hidden
#line 2 "C:\Repos\runceel\VSUCJP1\Demo3\Demo3.BlazorClient\Pages\Index.razor"
using Demo3.ClientLib;

#line default
#line hidden
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>Hello, world!</h1>\r\n\r\nWelcome to your new app.\r\n\r\n");
            __builder.OpenElement(1, "div");
            __builder.AddAttribute(2, "style", "color: red; font-size: xx-large;");
            __builder.AddContent(3, 
#line 8 "C:\Repos\runceel\VSUCJP1\Demo3\Demo3.BlazorClient\Pages\Index.razor"
                                               AlertText

#line default
#line hidden
            );
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#line 10 "C:\Repos\runceel\VSUCJP1\Demo3\Demo3.BlazorClient\Pages\Index.razor"
       
    private AlertNotifier _alertNotifier;
    private string AlertText { get; set; }
    protected override async Task OnInitializedAsync()
    {
        _alertNotifier = new AlertNotifier();
        _alertNotifier.Alert += (_, args) => AlertText = $"{DateTime.Now}: {args.Data.Value}";
        await _alertNotifier.InitializeAsync();
    }

#line default
#line hidden
    }
}
#pragma warning restore 1591