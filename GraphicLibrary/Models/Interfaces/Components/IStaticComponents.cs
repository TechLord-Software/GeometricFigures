using GraphicLibrary.Models.Unit;

namespace GraphicLibrary.Models.Interfaces
{
    public interface IStaticComponents
    {
        IReadOnlyList<IModelUnit> Models { get; }
    }
}
