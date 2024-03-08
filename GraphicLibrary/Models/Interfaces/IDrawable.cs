using GraphicLibrary.Shaders;

namespace GraphicLibrary.Models.Interfaces
{
    public interface IDrawable
    {
        static Shader Shader { get; set; }
        void Draw();
    }
}
