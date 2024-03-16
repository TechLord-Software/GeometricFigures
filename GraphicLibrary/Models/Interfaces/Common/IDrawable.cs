
using GraphicLibrary.Scenes;
using GraphicLibrary.Shaders;

namespace GraphicLibrary.Models.Interfaces.Common
{
    /// <summary>
    /// Интерфейс, определяющий метод отрисовки
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Метод отрисовки модели
        /// </summary>
        /// <param name="shader"> шейдер </param>
        /// <param name="scene"> текущая сцена </param>
        void Draw(Shader shader, Scene scene);
    }
}
