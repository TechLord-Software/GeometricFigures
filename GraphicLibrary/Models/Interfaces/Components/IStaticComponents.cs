using GraphicLibrary.Models.Unit;

namespace GraphicLibrary.Models.Interfaces.Components
{
    public interface IStaticComponents
    {
        IReadOnlyList<StaticModelUnit> Models { get; }
    }
}
