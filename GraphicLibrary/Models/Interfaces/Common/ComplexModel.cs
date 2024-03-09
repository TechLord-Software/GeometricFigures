using GraphicLibrary.Models.Unit;
using OpenTK.Mathematics;

namespace GraphicLibrary.Models.Interfaces.Common
{
    /// <summary>
    /// Абстрактный класс комплексной модели
    /// </summary>
    public abstract class ComplexModel
    {
        /// <summary>
        /// Составные части сложной модели
        /// </summary>
        protected List<ModelUnit> models;

        /// <summary>
        /// Координаты модели
        /// </summary>
        protected Vector3 position;
        public Vector3 Position => position;


        /// <summary>
        /// Конструктор класса ComplexModel
        /// </summary>
        protected ComplexModel()
        {
            models = new List<ModelUnit>();
            position = Vector3.Zero;
        }
        /// <summary>
        /// Конструктор класса ComplexModel, принимающиий одну простую модель
        /// </summary>
        /// <param name="unit"> простая модель </param>
        protected ComplexModel(ModelUnit unit)
        {
            models = new List<ModelUnit>() { unit };
            position = Vector3.Zero;
        }
        /// <summary>
        /// Конструктор класса ComplexModel, принимающий перечисление простых объектов
        /// </summary>
        /// <param name="units"> перечисление простых обюъектов </param>
        protected ComplexModel(IEnumerable<ModelUnit> units)
        {
            models = new List<ModelUnit>(units);
            position = Vector3.Zero;
        }



        /// <summary>
        /// Метод добавления новой простой модели
        /// </summary>
        /// <param name="unit"> простая модель </param>
        public void Add(ModelUnit unit)
        {
            models.Add(unit);
        }
        /// <summary>
        /// Метод удаления новой простой модели
        /// </summary>
        /// <param name="unit"> простая модель </param>
        public void Remove(ModelUnit unit)
        {
            models.Remove(unit);
        }
        /// <summary>
        /// Копирование простых моделей в другую комплексную модель
        /// </summary>
        /// <param name="model"> комплексная модель </param>
        public void CopyModelsTo(ComplexModel model)
        {
            foreach (ModelUnit unit in models)
            {
                model.Add((ModelUnit)unit.Clone());
            }
        }
        /// <summary>
        /// Удаление всех простых моделей
        /// </summary>
        public void ClearModels()
        {
            models.Clear();
        }
    }
}
