using GraphicLibrary.Models.Unit;

namespace GraphicLibrary.Models.Interfaces.Components
{
    public interface IDynamicComponents : IStaticComponents
    {
        new IReadOnlyList<TransformableModelUnit> Models { get; }
    }
}
