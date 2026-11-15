using Microsoft.AspNetCore.Mvc;

namespace TechStore.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
