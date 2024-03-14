using GraphicLibrary.Materials;
using GraphicLibrary.Models.Information;
using GraphicLibrary.Models.Interfaces.Common;
using OpenTK.Mathematics;

namespace GraphicLibrary.Models.Unit
{
    /// <summary>
    /// Абстрактный класс, описывающий перемещение, вращение и масштабирование модели
    /// </summary>
    public abstract class TransformableModelUnit : StaticModelUnit, ITransformable
    {
        /// <summary>
        /// Конструктор класса TransformableModelUnit
        /// </summary>
        /// <param name="name"> имя модели </param>
        /// <param name="material"> материал модели </param>
        protected TransformableModelUnit(string name, Material material)
            : base(name, material) { }
        protected TransformableModelUnit(string name) 
            : base(name) { }
        protected TransformableModelUnit(Material material) 
            : base(material) { }
        protected TransformableModelUnit() 
            : base() { }


        /// <summary>
        /// Метод перемещения модели
        /// </summary>
        /// <param name="shifts"> вектор перемещения </param>
        public void Move(Vector3 shifts)
        {
            translationMatrix *= Matrix4.CreateTranslation(shifts);
        }
        /// <summary>
        /// Метод масштабирования модели
        /// </summary>
        /// <param name="size"> коэффициенты масштабирования </param>
        public void Scale(Size size)
        {
            scaleMatrix *= Matrix4.CreateScale(size.X, size.Y, size.Z);
        }
        /// <summary>
        /// Метод вращения модели
        /// </summary>
        /// <param name="angles"> углы вращения </param>
        public void Rotate(RotationAngles angles)
        {
            rotationMatrix *= Matrix4.CreateRotationX(angles.X);
            rotationMatrix *= Matrix4.CreateRotationY(angles.Y);
            rotationMatrix *= Matrix4.CreateRotationY(angles.Z);
        }
    }
}
