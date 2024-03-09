using GraphicLibrary.Models.Information;
using GraphicLibrary.Models.Unit;
using OpenTK.Mathematics;

namespace GraphicLibrary.Models.Interfaces
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
    }
}
