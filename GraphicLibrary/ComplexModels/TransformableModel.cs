using GraphicLibrary.Models.Information;
using GraphicLibrary.Models.Interfaces.Common;
using GraphicLibrary.Models.Interfaces.Components;
using GraphicLibrary.Models.Unit;
using OpenTK.Mathematics;

namespace GraphicLibrary.ComplexModels
{
    /// <summary>
    /// Абстрактный класс, реализующий методы перемещения, вращения и масштабирования, 
    /// а также доступ к подобным методам у простых моделей, входящих в объект этого типа
    /// </summary>
    public abstract class TransformableModel : StaticModel, IDynamicComponents, ITransformable
    {
        /// <summary>
        /// Список простых моделей
        /// </summary>
        public new IReadOnlyList<ITransformableModelUnit> Models => models;


        /// <summary>
        /// Конструктор класса TransformableModel
        /// </summary>
        protected TransformableModel() : base() { }
        /// <summary>
        /// Конструктор класса TransformableModel, принимающиий одну простую модель
        /// </summary>
        /// <param name="unit"> простая модель </param>
        protected TransformableModel(ModelUnit unit) : base(unit) { }
        /// <summary>
        /// Конструктор класса TransformableModel, принимающий перечисление простых объектов
        /// </summary>
        /// <param name="units"> перечисление простых обюъектов </param>
        protected TransformableModel(IEnumerable<ModelUnit> units) : base(units) { }




        /// <summary>
        /// Перемещение модели
        /// </summary>
        /// <param name="shifts"> вектор перемещения </param>
        public void Move(Vector3 shifts)
        {
            foreach (var model in models)
            {
                model.Move(shifts);
            }

            position.X += shifts.X;
            position.Y += shifts.Y;
            position.Z += shifts.Z;
        }

        /// <summary>
        /// Поворот модели
        /// </summary>
        /// <param name="angles"> углы поворота </param>
        public void Rotate(RotationAngles angles)
        {
            foreach (var model in models)
            {
                model.Rotate(angles);
            }

            rotationAngles.X += angles.X;
            rotationAngles.Y += angles.Y;
            rotationAngles.Z += angles.Z;

            size.UpdateAfterRotation(angles);
        }

        /// <summary>
        /// Масштабирование модели
        /// </summary>
        /// <param name="scale"> коэффициенты масштабирования </param>
        public void Scale(Size scale)
        {
            foreach (var model in models)
            {
                model.Scale(scale);
            }

            size.X += scale.X;
            size.Y += scale.Y;
            size.Z += scale.Z;
        }
    }
}
