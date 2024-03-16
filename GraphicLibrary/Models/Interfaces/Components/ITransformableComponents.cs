using GraphicLibrary.Models.Unit;

namespace GraphicLibrary.Models.Interfaces.Components
{
    public interface ITransformableComponents : IInformationComponents
    {
        new IReadOnlyList<TransformableModelUnit> Models { get; }
    }
}
