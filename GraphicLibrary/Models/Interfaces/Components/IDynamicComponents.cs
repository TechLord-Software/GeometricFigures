using GraphicLibrary.Models.Unit;

namespace GraphicLibrary.Models.Interfaces.Components
{
    public interface IDynamicComponents
    {
        IReadOnlyList<ITransformableModelUnit> Models { get; }
    }
}
