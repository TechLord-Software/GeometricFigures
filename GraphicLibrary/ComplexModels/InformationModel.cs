using GraphicLibrary.Models.Interfaces.AbstractModels;
using GraphicLibrary.Models.Interfaces.Components;
using GraphicLibrary.Models.Unit;
using GraphicLibrary.Shaders;

namespace GraphicLibrary.ComplexModels
{
    /// <summary>
    /// Класс, описывающий характеристики комплексной модели
    /// </summary>
    public abstract class InformationModel : TransformationInfoComplexModel, IInformationComponents
    {
        /// <summary>
        /// Список простых моделей
        /// </summary>
        public IReadOnlyList<InformationModelUnit> Models => models;



        /// <summary>
        /// Конструктор класса StaticModel
        /// </summary>
        protected InformationModel() : base() { }
        /// <summary>
        /// Конструктор класса StaticModel, принимающиий одну простую модель
        /// </summary>
        /// <param name="unit"> простая модель </param>
        protected InformationModel(ModelUnit unit) : base(unit) { }
        /// <summary>
        /// Конструктор класса StaticModel, принимающий перечисление простых объектов
        /// </summary>
        /// <param name="units"> перечисление простых обюъектов </param>
        protected InformationModel(IEnumerable<ModelUnit> units) : base(units) { }




        /// <summary>
        /// Сохранение модели в файл с расширение .obj
        /// </summary>
        /// <param name="path"> путь к файлу </param>
        public void Save(string path)
        {

        }
    }
}
