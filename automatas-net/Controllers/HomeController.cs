using automatas_net.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Lombok.NET;

namespace automatas_net.Controllers
{
    [AllArgsConstructor]
    public partial class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITransform _transform;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] IFormFile file)
        {
            try
            {
                if (file.Length == 0)
                {
                    ModelState.AddModelError("File", "Por favor, seleccione un archivo valido.");
                    return View();
                }

                if (file.ContentType != "text/plain")
                {
                    ModelState.AddModelError("File", "Solo se permiten archivos de texto.");
                    return View();
                }

                string fileContent;
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    fileContent = await reader.ReadToEndAsync();
                }

                Vectors vectors = new Vectors();
                vectors.FileContent = fileContent.Trim();

                List<string> lines = vectors.FileContent.Split('\n').ToList();

                if (lines.Count >= 5)
                {
                    string qLine = lines[0].Trim();
                    vectors.Q = _transform.TransformVector(qLine, "Q");
                    string sigmaLine = lines[1].Trim();
                    vectors.Sigma = _transform.TransformVector(sigmaLine, "Sigma");
                    string iLine = lines[2].Trim();
                    vectors.I = iLine;
                    string aLine = lines[3].Trim();
                    vectors.A = _transform.TransformVector(aLine, "A");
                    string wLine = lines[4].Trim();
                    vectors.W = _transform.TransformMatrix(_transform.TransformVector(wLine, "w", ";"));

                    return View(vectors);
                }

                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred.");

                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError("An error occurred.");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
