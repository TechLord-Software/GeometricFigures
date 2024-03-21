using GraphicLibrary.Models.Interfaces.Common;
using GraphicLibrary.Models.Unit;
using GraphicLibrary.Scenes;
using GraphicLibrary.Shaders;

namespace GraphicLibrary.ComplexModels
{
    /// <summary>
    /// Класс комплексной модели
    /// </summary>
    public partial class Model : TransformableModel, IDrawable, ICloneable
    {
        /// <summary>
        /// Конструктор класса Model
        /// </summary>
        public Model() : base() { }
        /// <summary>
        /// Конструктор класса Model, принимающиий одну простую модель
        /// </summary>
        /// <param name="unit"> простая модель </param>
        public Model(ModelUnit unit) : base(unit) { }
        /// <summary>
        /// Конструктор класса Model, принимающий перечисление простых объектов
        /// </summary>
        /// <param name="units"> перечисление простых объектов </param>
        public Model(IEnumerable<ModelUnit> units) : base(units) { }



        /// <summary>
        /// Метод отрисовки модели
        /// </summary>
        /// <param name="shader"> шейдер </param>
        /// <param name="scene"> текущая сцена </param>
        public void Draw(Shader shader, Scene scene)
        {
            shader.UseLightSources(scene.LightSources);
            shader.UseCamera(scene.CurrentCamera);
            foreach (var model in models)
            {
                model.Draw(shader, scene);
            }
        }

        /// <summary>
        /// Копирование объекта
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new Model(models);
        }
    }
}
