using GraphicLibrary.Models.Interfaces.Common;
using GraphicLibrary.Models.Unit;
using GraphicLibrary.Shaders;

namespace GraphicLibrary.ComplexModels
{
    public class Model : TransformableModel, IDrawable
    {
        /// <summary>
        /// Конструктор класса Model
        /// </summary>
        public Model() : base() { }
        /// <summary>
        /// Конструктор класса Model, принимающиий одну простую модель
        /// </summary>
        /// <param name="unit"> простая модель </param>
        public Model(ModelUnit unit) { }
        /// <summary>
        /// Конструктор класса Model, принимающий перечисление простых объектов
        /// </summary>
        /// <param name="units"> перечисление простых обюъектов </param>
        public Model(IEnumerable<ModelUnit> units) : base(units) { }



        /// <summary>
        /// Отрисовка модели (всех ее составных частей)
        /// </summary>
        public void Draw()
        {
            if (Shader == null) return;

            Shader.Activate();
            foreach (var model in models)
            {
                model.Draw();
            }
            Shader.Deactivate();
        }


        /// <summary>
        /// Загрузка модели из файла с расширением .obj
        /// </summary>
        /// <param name="path"> путь к файлу </param>
        /// <returns> загруженная модель </returns>
        public static Model Parse(string path)
        {

        }
    }
}
