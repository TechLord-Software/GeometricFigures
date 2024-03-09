using System.Numerics;
using System.Xml.Linq;

namespace GraphicLibrary.Models
{
    /// <summary>
    /// Структура, задающая размер модели относительно начального размера
    /// </summary>
    public struct Size
    {
        /// <summary>
        /// Значение по умолчанию - 1x1x1
        /// </summary>
        public static readonly Size One;

        /// <summary>
        /// Размер по оси OX (расстояние от точки с наименьшей координатой X до наибольшей (относительно начального размера))
        /// </summary>
        public float X;
        /// <summary>
        /// Размер по оси OY (расстояние от точки с наименьшей координатой Y до наибольшей (относительно начального размера))
        /// </summary>
        public float Y;
        /// <summary>
        /// Размер по оси OZ (расстояние от точки с наименьшей координатой Z до наибольшей (относительно начального размера))
        /// </summary>
        public float Z;


        /// <summary>
        /// Статический конструктор
        /// </summary>
        static Size()
        {
            One = new Size(1, 1, 1);
        }


        public Size()
        {
            this = One;
        }
        /// <summary>
        /// Конструктор структуры Size
        /// </summary>
        /// <param name="x"> размер по OX </param>
        /// <param name="y"> размер по OY </param>
        /// <param name="z"> размер по OZ </param>
        public Size(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }


        /// <summary>
        /// Деконструктор структуры Size
        /// </summary>
        /// <param name="x"> компонента X </param>
        /// <param name="y"> компонента Y </param>
        /// <param name="z"> компонента Z </param>
        public void Deconstruct(out float x, out float y, out float z)
        {
            x = X;
            y = Y;
            z = Z;
        }


        /// <summary>
        /// Обновление размера после вращения
        /// </summary>
        /// <param name="angles"> углы вращения </param>
        public void UpdateAfterRotation(RotationAngles angles)
        {
            UpdateAfterRotationX(angles.NormalizedX);
            UpdateAfterRotationY(angles.NormalizedY);
            UpdateAfterRotationZ(angles.NormalizedZ);
        }
        /// <summary>
        /// Обновление размера после вращения по оси OX
        /// </summary>
        /// <param name="angle"> угол повророта </param>
        private void UpdateAfterRotationX(float angle)
        {
            var (x, y, z) = this;

            X = x;
            Y = y * MathF.Cos(angle) - z * MathF.Sin(angle);
            Z = y * MathF.Sin(angle) + z * MathF.Cos(angle);
        }
        /// <summary>
        /// Обновление размера после вращения по оси OY
        /// </summary>
        /// <param name="angle"> угол повророта </param>
        private void UpdateAfterRotationY(float angle)
        {
            var (x, y, z) = this;

            X = x * MathF.Cos(angle) + z * MathF.Sin(angle);
            Y = y;
            Z = -x * MathF.Sin(angle) + z * MathF.Cos(angle);
        }
        /// <summary>
        /// Обновление размера после вращения по оси OZ
        /// </summary>
        /// <param name="angle"> угол повророта </param>
        private void UpdateAfterRotationZ(float angle)
        {
            var (x, y, z) = this;

            X = x * MathF.Cos(angle) - y * MathF.Sin(angle);
            Y = x * MathF.Sin(angle) + y * MathF.Cos(angle);
            Z = z;
        }
    }
}
