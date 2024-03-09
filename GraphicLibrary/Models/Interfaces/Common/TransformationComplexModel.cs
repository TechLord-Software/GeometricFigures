namespace GraphicLibrary.Models.Interfaces.Common
{
    public abstract class TransformationComplexModel : ComplexModel
    {
        /// <summary>
        /// Размер модели
        /// </summary>
        protected Size size;
        /// <summary>
        /// Углы поворота
        /// </summary>
        protected RotationAngles rotationAngles;
    }
}
