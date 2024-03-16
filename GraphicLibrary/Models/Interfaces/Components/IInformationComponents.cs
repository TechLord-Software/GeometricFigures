using GraphicLibrary.Models.Unit;

namespace GraphicLibrary.Models.Interfaces.Components
{
    public interface IInformationComponents
    {
        IReadOnlyList<InformationModelUnit> Models { get; }
    }
}
