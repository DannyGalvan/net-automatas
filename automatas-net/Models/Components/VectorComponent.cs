using Microsoft.AspNetCore.Mvc;

namespace automatas_net.Models.Components
{
    public class VectorComponent : ViewComponent
    {
        public IViewComponentResult Invoke(VectorComponentModel model)
        {
            return View(model);
        }
    }
}
