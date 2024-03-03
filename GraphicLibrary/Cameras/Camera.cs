using OpenTK.Mathematics;

namespace GraphicLibrary.Cameras
{
    /// <summary>
    /// Абстрактный класс камеры
    /// </summary>
    public abstract class Camera
    {
        /// <summary>
        /// Расстояние до ближней плоскости отсечения
        /// </summary>
        protected const float DEPTH_NEAR = 0.01f;
        /// <summary>
        /// Максимальный угол обзора
        /// </summary>
        protected const float MAX_FOV = 3 * MathHelper.PiOver4;
        /// <summary>
        /// Минимальный угол обзора
        /// </summary>
        protected const float MIN_FOV = MathHelper.PiOver4;

        /// <summary>
        /// Расстояние до дальней плоскости отсечения
        /// </summary>
        protected float _renderDistance;
        /// <summary>
        /// Угол обзора
        /// </summary>
        protected float _fov;


        // На будущее: добавить модель,
        // методы для отображения и
        // изменения положения модели
        // в пространстве вместе с камерой
        //
        //private ModelUnit model;



        public float RenderDistance
        {
            get => _renderDistance;
            set => _renderDistance = Math.Max(value, DEPTH_NEAR);
        }
        public float Fov
        {
            get => _fov;
            set => _fov = MathHelper.Clamp(value, MIN_FOV, MAX_FOV);
        }

        /// <summary>
        /// Матрица преобразования камеры
        /// </summary>
        public abstract Matrix4 ViewMatrix { get; protected set; }
        /// <summary>
        /// Матрица проекции
        /// </summary>
        public abstract Matrix4 ProjectionMatrix { get; protected set; }


        /// <summary>
        /// Соотношение сторон
        /// </summary>
        public float AspectRatio { get; set; }


        /// <summary>
        /// Координаты камеры
        /// </summary>
        public Vector3 Position { get; private set; }
        /// <summary>
        /// Направление камеры
        /// </summary>
        public Vector3 Direction { get; private set; }
        /// <summary>
        /// Вектор, на конец которого направлена камера
        /// </summary>
        public Vector3 Target { get; private set; }
        /// <summary>
        /// Вертикальный вектор камеры
        /// </summary>
        public Vector3 Up { get; private set; }
        /// <summary>
        /// Правый вектор камеры
        /// </summary>
        public Vector3 Right { get; private set; }


        /// <summary>
        /// Метод для передвижения камеры
        /// </summary>
        /// <param name="mouseX"> перемещение мыши по OX </param>
        /// <param name="mouseY"> перемещение мыши по OY </param>
        public abstract void Move(float mouseX, float mouseY);
        /// <summary>
        /// Обновление векторов и матриц
        /// </summary>
        protected abstract void Update();
    }
}
