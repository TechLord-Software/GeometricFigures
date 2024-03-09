using OpenTK.Mathematics;

namespace GraphicLibrary.Models.Information
{
    /// <summary>
    /// Структура, хранящая углы вращения вокруг координатных осей
    /// </summary>
    public struct RotationAngles
    {
        /// <summary>
        /// Значение по умолчанию
        /// </summary>
        public static readonly RotationAngles Zero;

        /// <summary>
        /// Угол вокруг оси OX
        /// </summary>
        private float _x;
        /// <summary>
        /// Угол вокруг оси OY
        /// </summary>
        private float _y;
        /// <summary>
        /// Угол вокрус оси OZ
        /// </summary>
        private float _z;

        /// <summary>
        /// Угол вокруг оси OX [0; 2*pi);
        /// </summary>
        private float _normalizedX;
        /// <summary>
        /// Угол вокруг оси OY [0; 2*pi);
        /// </summary>
        private float _normalizedY;
        /// <summary>
        /// Угол вокруг оси OZ [0; 2*pi);
        /// </summary>
        private float _normalizedZ;

        public float X
        {
            get => _x;
            set
            {
                _x = value;
                _normalizedX = MathHelper.ClampRadians(_normalizedX + value);
            }
        }
        public float Y
        {
            get =>_y;
            set
            {
                _y = value;
                _normalizedY = MathHelper.ClampRadians(_normalizedY + value);
            }
        }
        public float Z
        {
            get => _z;
            set
            {
                _z = value;
                _normalizedZ = MathHelper.ClampRadians(_normalizedZ + value);
            }
        }

        public float NormalizedX => _normalizedX;
        public float NormalizedY => _normalizedY;
        public float NormalizedZ => _normalizedZ;
        

        /// <summary>
        /// Статический конструктор
        /// </summary>
        static RotationAngles()
        {
            Zero = new RotationAngles(0, 0, 0);
        }
        public RotationAngles()
        {
            this = Zero;
        }
        /// <summary>
        /// Конструктор стуктуры RotationAngles
        /// </summary>
        /// <param name="angleX"> угол вокруг оси OX </param>
        /// <param name="angleY"> угол вокруг оси OY </param>
        /// <param name="angleZ"> угол вокруг оси OZ </param>
        public RotationAngles(float angleX, float angleY, float angleZ)
        {
            X = angleX;
            Y = angleY;
            Z = angleZ;
        }
    }
}
