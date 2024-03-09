using GraphicLibrary.Models.Unit;

namespace GraphicLibrary.Models.Interfaces.Common
{
    /// <summary>
    /// Абстрактный класс комплексной модели с полями размера и углов поворотов
    /// </summary>
    public abstract class TransformationInfoComplexModel : ComplexModel
    {
        /// <summary>
        /// Размер модели
        /// </summary>
        protected Size size;
        /// <summary>
        /// Углы поворота
        /// </summary>
        protected RotationAngles rotationAngles;


        public Size Size => size;
        public RotationAngles RotationAngles => rotationAngles;


        /// <summary>
        /// Конструктор класса TransformationInfoComplexModel
        /// </summary>
        protected TransformationInfoComplexModel() : base()
        {
            size = Size.One;
            rotationAngles = RotationAngles.Zero;
        }
        /// <summary>
        /// Конструктор класса TransformationInfoComplexModel, принимающиий одну простую модель
        /// </summary>
        /// <param name="unit"> простая модель </param>
        protected TransformationInfoComplexModel(ModelUnit unit) : base(unit)
        {
            size = Size.One;
            rotationAngles = RotationAngles.Zero;
        }
        /// <summary>
        /// Конструктор класса TransformationInfoComplexModel, принимающий перечисление простых объектов
        /// </summary>
        /// <param name="units"> перечисление простых обюъектов </param>
        protected TransformationInfoComplexModel(IEnumerable<ModelUnit> units) : base(units)
        {
            size = Size.One;
            rotationAngles = RotationAngles.Zero;
        }
    }
}
