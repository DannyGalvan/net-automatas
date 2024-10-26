using automatas_net.Models;
using System.Text.RegularExpressions;
using Lombok.NET;

namespace automatas_net.Services
{
    [AllArgsConstructor]
    public partial class Transform : ITransform
    {
        private readonly ILogger<Transform> _logger;
        public List<string> TransformVector(string line, string title, string separator = ",")
        {
            var match = Regex.Match(line, @"\{([^}]+)\}");

            if (match.Success)
            {
                return match.Groups[1].Value.Split(separator).Select(el => el.Trim()).ToList();
            }

            _logger.LogError($"No se pudo encontrar el vector {title} en la línea {line}");

            return new List<string>();
        }

        public List<Matrix> TransformMatrix(List<string> matrixArray)
        {
            return (from item in matrixArray select Regex.Match(item, @"\(([^,]+),([^,]+),([^)]+)\)") into match where match.Success select new Matrix { From = match.Groups[1].Value.Trim(), Symbol = match.Groups[2].Value.Trim(), To = match.Groups[3].Value.Trim() }).ToList();
        }
    }
}
