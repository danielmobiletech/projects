#pragma checksum "/Users/daniel/Projects/Shop.UI/Shop.UI/Cart/Small.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6b784bb2c82e5b644b08b8a4b429df483bde34af"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Cart_Small), @"mvc.1.0.view", @"/Cart/Small.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b784bb2c82e5b644b08b8a4b429df483bde34af", @"/Cart/Small.cshtml")]
    public class Cart_Small : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Shop.Application.Cart.GetCart.Response>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<a class=\"navbar-item\" asp-page=\"/Cart\">\r\n    Cart ");
#nullable restore
#line 4 "/Users/daniel/Projects/Shop.UI/Shop.UI/Cart/Small.cshtml"
    Write(Model);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</a>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Shop.Application.Cart.GetCart.Response>> Html { get; private set; }
    }
}
#pragma warning restore 1591