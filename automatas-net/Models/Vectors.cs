namespace automatas_net.Models
{
    public class Vectors
    {
        public string FileContent { get; set; } = string.Empty;
        public List<string> Q { get; set; } = new List<string>();
        public List<string> Sigma { get; set; } = new List<string>();
        public string I { get; set; } = string.Empty;
        public List<string> A { get; set; } = new List<string>();
        public List<Matrix> W { get; set; } = new List<Matrix>();

        public string GetValueForCell(string from, string symbol)
        {
            var transition = W.Where(x => x.From == from && x.Symbol == symbol);

            var enumerable = transition.Select(x => x.To).ToList();

            string join = enumerable.Any() ? string.Join(',', enumerable) : string.Empty;

            return join;
        }
    }
}
