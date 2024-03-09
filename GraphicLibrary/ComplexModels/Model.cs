using GraphicLibrary.Models;
using GraphicLibrary.Models.Interfaces;
using GraphicLibrary.Models.Unit;
using GraphicLibrary.Shaders;
using OpenTK.Mathematics;

namespace GraphicLibrary.ComplexModels
{
    public class Model : ComplexModel, IDrawable, ITransformableModel
    {
        /// <summary>
        /// Размер модели
        /// </summary>
        private Size _size;
        /// <summary>
        /// Углы поворота
        /// </summary>
        private RotationAngles _rotationAngles;



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
        public Size Size => _size;
        public RotationAngles RotationAngles => _rotationAngles;


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

            _rotationAngles.X += angles.X;
            _rotationAngles.Y += angles.Y;
            _rotationAngles.Z += angles.Z;

            _size.UpdateAfterRotation(angles);
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

            _size.X += scale.X;
            _size.Y += scale.Y;
            _size.Z += scale.Z;
        }
    }
}
