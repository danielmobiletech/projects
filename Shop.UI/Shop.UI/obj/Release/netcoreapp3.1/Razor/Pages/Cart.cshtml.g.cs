#pragma checksum "/Users/daniel/Projects/Shop.UI/Shop.UI/Pages/Cart.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "96b16d981f74653257be22e063261dfe0154554a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Shop.UI.Pages.Pages_Cart), @"mvc.1.0.razor-page", @"/Pages/Cart.cshtml")]
namespace Shop.UI.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Users/daniel/Projects/Shop.UI/Shop.UI/Pages/_ViewImports.cshtml"
using Shop.UI;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"96b16d981f74653257be22e063261dfe0154554a", @"/Pages/Cart.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"321df9dc533d5fcb5a39eafbde6e0d1a4f3ca80e", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Cart : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("    <div>\r\n");
#nullable restore
#line 6 "/Users/daniel/Projects/Shop.UI/Shop.UI/Pages/Cart.cshtml"
         foreach (var product in Model.Cart)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <p>");
#nullable restore
#line 8 "/Users/daniel/Projects/Shop.UI/Shop.UI/Pages/Cart.cshtml"
      Write(product.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n        <p>");
#nullable restore
#line 9 "/Users/daniel/Projects/Shop.UI/Shop.UI/Pages/Cart.cshtml"
      Write(product.Values);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </p>\r\n        <p>");
#nullable restore
#line 10 "/Users/daniel/Projects/Shop.UI/Shop.UI/Pages/Cart.cshtml"
      Write(product.StockId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n        <p>");
#nullable restore
#line 11 "/Users/daniel/Projects/Shop.UI/Shop.UI/Pages/Cart.cshtml"
      Write(product.Qty);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 12 "/Users/daniel/Projects/Shop.UI/Shop.UI/Pages/Cart.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Shop.UI.CartModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Shop.UI.CartModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Shop.UI.CartModel>)PageContext?.ViewData;
        public Shop.UI.CartModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
