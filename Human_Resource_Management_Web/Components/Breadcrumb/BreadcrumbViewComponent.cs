using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Human_Resource_Management_Web.Components.Breadcrumb
{
    [ViewComponent(Name = "BreadcrumbViewComponent")]
    public class BreadcrumbViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.Run(() => View());
        }
    }
}
