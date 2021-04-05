using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Human_Resource_Management_Web.Components.SidebarFooter
{
    [ViewComponent(Name = "SidebarFooterViewComponent")]
    public class SidebarFooterViewComponent : ViewComponent
    {
        private IHttpContextAccessor _HttpContextAccessor;
        public SidebarFooterViewComponent(IHttpContextAccessor HttpContextAccessor)
        {
            _HttpContextAccessor = HttpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.Run(() => View());
        }
    }
}
