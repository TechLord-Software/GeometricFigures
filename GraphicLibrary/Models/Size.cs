using System.Numerics;

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
        /// Размер по оси OX
        /// </summary>
        public float X;
        /// <summary>
        /// Размер по оси OY
        /// </summary>
        public float Y;
        /// <summary>
        /// Размер по оси OZ
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
    }
}
