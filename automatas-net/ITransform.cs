using automatas_net.Models;

namespace automatas_net
{
    public interface ITransform
    {
        List<string> TransformVector(string line, string title, string separator = ",");
        List<Matrix> TransformMatrix(List<string> matrixArray);
    }
}
