using GraphicLibrary.ComplexModels;
using GraphicLibrary.Materials;

namespace GraphicLibrary.LightSources
{
    public interface ILightSource : IModel
    {
        PhongModel PhongParameters { get; set; }
        LightAttenuationParameters LightAttenuationParameters { get; set; }
    }
}
