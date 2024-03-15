using GraphicLibrary.Models.Interfaces.Common;
using GraphicLibrary.Models.Interfaces.Components;
using GraphicLibrary.Models.Unit;
using GraphicLibrary.Shaders;

namespace GraphicLibrary.ComplexModels
{
    /// <summary>
    /// Класс, описывающий характеристики комплексной модели
    /// </summary>
    public abstract class StaticModel : TransformationInfoComplexModel, IStaticComponents
    {
        /// <summary>
        /// Используемый шейдер
        /// </summary>
        public static Shader? Shader { get; set; }

        /// <summary>
        /// Список простых моделей
        /// </summary>
        public IReadOnlyList<StaticModelUnit> Models => models;



        /// <summary>
        /// Конструктор класса StaticModel
        /// </summary>
        protected StaticModel() : base() { }
        /// <summary>
        /// Конструктор класса StaticModel, принимающиий одну простую модель
        /// </summary>
        /// <param name="unit"> простая модель </param>
        protected StaticModel(ModelUnit unit) : base(unit) { }
        /// <summary>
        /// Конструктор класса StaticModel, принимающий перечисление простых объектов
        /// </summary>
        /// <param name="units"> перечисление простых обюъектов </param>
        protected StaticModel(IEnumerable<ModelUnit> units) : base(units) { }




        /// <summary>
        /// Сохранение модели в файл с расширение .obj
        /// </summary>
        /// <param name="path"> путь к файлу </param>
        public void Save(string path)
        {

        }
    }
}
