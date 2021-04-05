﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Human_Resource_Management_Web.Components.Header
{
    [ViewComponent(Name = "SidebarViewComponent")]
    public class SidebarViewComponent : ViewComponent
    {
        private IHttpContextAccessor _HttpContextAccessor;
        public SidebarViewComponent(IHttpContextAccessor HttpContextAccessor)
        {
            _HttpContextAccessor = HttpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.Run(() => View());
        }
    }
}
