using Microsoft.AspNetCore.Mvc;

namespace automatas_net.Models.Components
{
    public class MatrixComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Vectors model)
        {
            return View(model);
        }
    }
}
