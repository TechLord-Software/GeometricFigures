using GraphicLibrary.Models;
using GraphicLibrary.Models.Interfaces;
using GraphicLibrary.Models.Interfaces.Common;
using GraphicLibrary.Models.Unit;
using GraphicLibrary.Shaders;
using OpenTK.Mathematics;

namespace GraphicLibrary.ComplexModels
{
    public class Model : TransformationComplexModel, IDrawable, ITransformableModel
    {
        /// <summary>
        /// Используемый шейдер
        /// </summary>
        public static Shader? Shader { get; set; }

        /// <summary>
        /// Список простых моделей
        /// </summary>
        public IReadOnlyList<ITransformableModelUnit> Models => models;

        /// <summary>
        /// Позиция модели
        /// </summary>
        public Vector3 Position => position;
        /// <summary>
        /// Размер модели
        /// </summary>
        public Size Size => size;
        /// <summary>
        /// Углы поворота модели
        /// </summary>
        public RotationAngles RotationAngles => rotationAngles;


        /// <summary>
        /// Конструктор класса Model
        /// </summary>
        public Model()
        {
            models = new List<ModelUnit>();

            position = Vector3.Zero;
            size = Size.One;
            rotationAngles = RotationAngles.Zero;
        }


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
