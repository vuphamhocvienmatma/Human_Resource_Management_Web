using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Human_Resource_Management_Web.Components.UserProfile
{
    [ViewComponent(Name = "UserProfileViewComponent")]
    public class UserProfileViewComponent : ViewComponent
    {
        private IHttpContextAccessor _HttpContextAccessor;
        public UserProfileViewComponent(IHttpContextAccessor HttpContextAccessor)
        {
            _HttpContextAccessor = HttpContextAccessor;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.Run(() => View());
        }
    }
}
